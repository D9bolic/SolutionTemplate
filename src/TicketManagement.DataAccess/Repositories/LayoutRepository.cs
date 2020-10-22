using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class LayoutRepository : ILayoutRepository
    {
        private const string IdField = "Id";
        private const string VenueIdField = "VenueId";
        private const string DescriptionField = "Description";

        private const string IdParameter = "@id";
        private const string VenueIdParameter = "@venueId";
        private const string DescriptionParameter = "@description";

        private const string SelectAllSqlExpression = @"select Id, VenueId, Description 
                                                        from Layout";

        private const string SelectByIdSqlExpression = @"select Id, VenueId, Description 
                                                         from Layout 
                                                         where Id = @id";

        private const string DeleteSqlExpression = @"delete from Layout 
                                                     where Id = @id";

        private const string InsertSqlExpression = @"insert into Layout (VenueId, Description) 
                                                     output inserted.Id 
                                                     values(@venueId, @description)";

        private const string UpdateSqlExpression = @"update Layout 
                                                     set VenueId = @venueId, Description = @description 
                                                     where Id = @id";

        private readonly string _connectionString;

        public LayoutRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<LayoutDomain> CreateAsync(LayoutDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(InsertSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(VenueIdParameter, item?.VenueId),
                            new SqlParameter(DescriptionParameter, item?.Description),
                        });

                    var addedItemId = await command.ExecuteScalarAsync();
                    var addedItem = await GetByIdAsync((int)addedItemId);
                    return addedItem;
                }
            }
        }

        public async Task DeleteAsync(int itemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(DeleteSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, itemId);
                    command.Parameters.Add(id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<LayoutDomain>> GetAllAsync()
        {
            // variable for return
            var layouts = new List<LayoutDomain>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectAllSqlExpression, connection))
                {
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        layouts.Add(new LayoutDomain
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField)),
                            VenueId = dataReader.GetInt32(dataReader.GetOrdinal(VenueIdField)),
                            Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField)),
                        });
                    }
                }
            }

            return layouts;
        }

        public async Task<LayoutDomain> GetByIdAsync(int itemId)
        {
            // variable for return
            var layout = new LayoutDomain();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectByIdSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, itemId);
                    command.Parameters.Add(id);
                    var dataReader = await command.ExecuteReaderAsync();
                    dataReader.Read();

                    layout.Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField));
                    layout.VenueId = dataReader.GetInt32(dataReader.GetOrdinal(VenueIdField));
                    layout.Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField));
                }
            }

            return layout;
        }

        public async Task<LayoutDomain> UpdateAsync(LayoutDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(UpdateSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(VenueIdParameter, item?.VenueId),
                            new SqlParameter(DescriptionParameter, item?.Description),
                            new SqlParameter(IdParameter, item?.Id),
                        });

                    await command.ExecuteNonQueryAsync();
                    var updatedItem = await GetByIdAsync((int)item?.Id);
                    return updatedItem;
                }
            }
        }
    }
}
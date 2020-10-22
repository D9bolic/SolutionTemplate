using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class AreaRepository : IAreaRepository
    {
        private const string IdField = "Id";
        private const string LayoutIdField = "LayoutId";
        private const string DescriptionField = "Description";
        private const string CoordXField = "CoordX";
        private const string CoordYField = "CoordY";

        private const string IdParameter = "@id";
        private const string LayoutIdParameter = "@layoutId";
        private const string DescriptionParameter = "@description";
        private const string CoordXParameter = "@coordX";
        private const string CoordYParameter = "@coordY";

        private const string SelectAllSqlExpression = @"select Id, LayoutId, Description, CoordX, CoordY 
                                                        from Area";

        private const string SelectByIdSqlExpression = @"select Id, LayoutId, Description, CoordX, CoordY 
                                                         from Area 
                                                         where Id = @id";

        private const string DeleteSqlExpression = @"delete from Area 
                                                     where Id = @id";

        private const string InsertSqlExpression = @"insert into Area (LayoutId, Description, CoordX, CoordY) 
                                                   output inserted.Id 
                                                   values(@layoutId, @description, @coordX, @coordY)";

        private const string UpdateSqlExpression = @"update Area 
                                                     set LayoutId = @layoutId, Description = @description, CoordX = @coordX, CoordY = @coordY 
                                                     where Id = @id";

        private readonly string _connectionString;

        public AreaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<AreaDomain> CreateAsync(AreaDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(InsertSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(LayoutIdParameter, item?.LayoutId),
                            new SqlParameter(DescriptionParameter, item?.Description),
                            new SqlParameter(CoordXParameter, item?.CoordX),
                            new SqlParameter(CoordYParameter, item?.CoordY),
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

        public async Task<List<AreaDomain>> GetAllAsync()
        {
            // variable for return
            var areas = new List<AreaDomain>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectAllSqlExpression, connection))
                {
                    var dataReader = await command.ExecuteReaderAsync();

                    while (dataReader.Read())
                    {
                        areas.Add(new AreaDomain
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField)),
                            LayoutId = dataReader.GetInt32(dataReader.GetOrdinal(LayoutIdField)),
                            Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField)),
                            CoordX = dataReader.GetInt32(dataReader.GetOrdinal(CoordXField)),
                            CoordY = dataReader.GetInt32(dataReader.GetOrdinal(CoordYField)),
                        });
                    }
                }
            }

            return areas;
        }

        public async Task<AreaDomain> GetByIdAsync(int itemId)
        {
            // variable for return
            var area = new AreaDomain();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectByIdSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, itemId);
                    command.Parameters.Add(id);

                    var dataReader = await command.ExecuteReaderAsync();
                    dataReader.Read();

                    area.Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField));
                    area.LayoutId = dataReader.GetInt32(dataReader.GetOrdinal(LayoutIdField));
                    area.Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField));
                    area.CoordX = dataReader.GetInt32(dataReader.GetOrdinal(CoordXField));
                    area.CoordY = dataReader.GetInt32(dataReader.GetOrdinal(CoordYField));
                }
            }

            return area;
        }

        public async Task<AreaDomain> UpdateAsync(AreaDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(UpdateSqlExpression, connection))
                {
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(LayoutIdParameter, item?.LayoutId),
                            new SqlParameter(DescriptionParameter, item?.Description),
                            new SqlParameter(CoordXParameter, item?.CoordX),
                            new SqlParameter(CoordYParameter, item?.CoordY),
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
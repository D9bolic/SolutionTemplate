using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.DataAccess.Repositories
{
    internal class VenueRepository : IVenueRepository
    {
        private const string IdField = "Id";
        private const string DescriptionField = "Description";
        private const string AddressField = "Address";
        private const string PhoneField = "Phone";

        private const string IdParameter = "@id";
        private const string DescriptionParameter = "@description";
        private const string AddressParameter = "@address";
        private const string PhoneParameter = "@phone";

        private const string NullString = "null";

        private const string DeleteSqlExpression = @"delete from Venue 
                                                     where Id = @id";

        private const string SelectByIdSqlExpression = @"select Id, Description, Address, Phone 
                                                         from Venue 
                                                         where Id = @id";

        private const string SelectAllSqlExpression = @"select Id, Description, Address, Phone 
                                                        from Venue";

        private const string InsertSqlExpression = @"insert into Venue (Description, Address, Phone)
                                                     output inserted.Id
                                                     values(@description, @address, @phone)";

        private const string UpdateSqlExpression = @"update Venue 
                                                     set Description = @description, Address = @address, Phone = @phone 
                                                     where Id = @id";

        private readonly string _connectionString;

        public VenueRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<VenueDomain> CreateAsync(VenueDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(InsertSqlExpression, connection))
                {
                    var phoneForParameter = string.IsNullOrEmpty(item?.Phone) ? NullString : item?.Phone;
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(DescriptionParameter, item?.Description),
                            new SqlParameter(AddressParameter, item?.Address),
                            new SqlParameter(PhoneParameter, phoneForParameter),
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

        public async Task<List<VenueDomain>> GetAllAsync()
        {
            // variable for return
            var venues = new List<VenueDomain>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectAllSqlExpression, connection))
                {
                    var dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var phoneFromReader = dataReader.GetString(dataReader.GetOrdinal(PhoneField));

                        venues.Add(new VenueDomain
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField)),
                            Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField)),
                            Address = dataReader.GetString(dataReader.GetOrdinal(AddressField)),
                            Phone = phoneFromReader == NullString ? null : phoneFromReader,
                        });
                    }
                }
            }

            return venues;
        }

        public async Task<VenueDomain> GetByIdAsync(int itemId)
        {
            // variable for return
            var venue = new VenueDomain();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(SelectByIdSqlExpression, connection))
                {
                    var id = new SqlParameter(IdParameter, itemId);
                    command.Parameters.Add(id);

                    var dataReader = await command.ExecuteReaderAsync();
                    dataReader.Read();

                    var phoneFromReader = dataReader.GetString(dataReader.GetOrdinal(PhoneField));

                    venue.Id = dataReader.GetInt32(dataReader.GetOrdinal(IdField));
                    venue.Description = dataReader.GetString(dataReader.GetOrdinal(DescriptionField));
                    venue.Address = dataReader.GetString(dataReader.GetOrdinal(AddressField));
                    venue.Phone = phoneFromReader == NullString ? null : phoneFromReader;
                }
            }

            return venue;
        }

        public async Task<VenueDomain> UpdateAsync(VenueDomain item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(UpdateSqlExpression, connection))
                {
                    var phoneForParameter = string.IsNullOrEmpty(item?.Phone) ? NullString : item?.Phone;
                    command.Parameters.AddRange(
                        new[]
                        {
                            new SqlParameter(DescriptionParameter, item?.Description),
                            new SqlParameter(AddressParameter, item?.Address),
                            new SqlParameter(PhoneParameter, phoneForParameter),
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
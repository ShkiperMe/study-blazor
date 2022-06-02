using System;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

using Fridge.Server.Data;
using Fridge.Shared;


namespace Fridge.Server.Services.RefrigeratorService
{
    public class RefrigeratorServiceDapper : IRefrigeratorService
    {

        string connectionString = null;

        public RefrigeratorServiceDapper(string conn)
        {
            connectionString = conn;
        }

        public async Task<ServiceResponse<List<Refrigerator>>> GetRefrigeratorsAsync()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var response = new ServiceResponse<List<Refrigerator>>
                {
                    Data = db.Query<Refrigerator>("SELECT * FROM Refrigerators WHERE Deleted = 0").ToList()
                };
                return response;
            }
        }
            

        public async Task<ServiceResponse<List<Indicator>>> GetIndicatorsAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var response = new ServiceResponse<List<Indicator>>
                {
                    Data = db.Query<Indicator>("SELECT * FROM Indicators WHERE FridgeId = @id ORDER BY DateCreated DESC", new { id }).ToList()
                };

                return response;
            }
        }

        public async Task<ServiceResponse<Refrigerator>> GetRefrigeratorAsync(int id)
        {
            var response = new ServiceResponse<Refrigerator>();
            Refrigerator refrigerator = null;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                refrigerator = db.Query<Refrigerator>("SELECT * FROM Refrigerators WHERE Id = @id AND Deleted = 0", new { id }).FirstOrDefault();
            }

            if (refrigerator == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this refrigerator does not exist.";
            }
            else
            {
                response.Data = refrigerator;
            }

            return response;
        }

        public async Task<ServiceResponse<Refrigerator>> CreateRefrigerator(Refrigerator refrigerator)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Refrigerators (Name, Deleted) VALUES(@Name, @Deleted)";
                db.Execute(sqlQuery, refrigerator);
            }
            return new ServiceResponse<Refrigerator> { Data = refrigerator };
        }

        public async Task<ServiceResponse<Indicator>> CreateRefrigeratorIndicator(Indicator indicator)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Indicators (DateCreated, Value, FridgeId) VALUES(@DateCreated, @Value, @FridgeId)";
                db.Execute(sqlQuery, indicator);
            }
            return new ServiceResponse<Indicator> { Data = indicator };
        }

        public async Task<ServiceResponse<Refrigerator>> UpdateRefrigerator(Refrigerator refrigerator)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Refrigerators SET Name = @Name WHERE Id = @Id";
                db.Execute(sqlQuery, refrigerator);
            }
            return new ServiceResponse<Refrigerator> { Data = refrigerator };
        }



        public async Task<ServiceResponse<bool>> DeleteRefrigerator(int refrigeratorId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Refrigerators WHERE Id = @id";
                db.Execute(sqlQuery, new { refrigeratorId });
            }
            return new ServiceResponse<bool> { Data = true };
        }
    }
}

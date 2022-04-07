using GestionAcademica.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class CareerService : Service
    {
        private static readonly string insertCareer = "udpInsertCareer";
        private static readonly string updateCareer = "udpModifyCareer";
        private static readonly string deleteCareer = "udpDeleteCareer";
        private static readonly string listCareers = "udpFindCareer";
        private SqlCommand command = null;

        public CareerService() { }
        public async  Task<bool> InsertCareer(Career career)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertCareer, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Code", career.Code));
                command.Parameters.Add(new SqlParameter("@CareerName", career.CareerName));
                command.Parameters.Add(new SqlParameter("@DegreeName", career.DegreeName));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        public async Task<bool> UpdateCareer(Career career)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateCareer, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Career", career.ID));
                command.Parameters.Add(new SqlParameter("@Code", career.Code));
                command.Parameters.Add(new SqlParameter("@CareerName", career.CareerName));
                command.Parameters.Add(new SqlParameter("@DegreeName", career.DegreeName));

                var rowsAffected = await command .ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch
            {

            }
            Disconnect();
            return response;
        }
        public async Task<bool> DeleteCareer(int id)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteCareer, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Career", id));

                var affectedRows = await command.ExecuteNonQueryAsync();

                if (affectedRows > 0) response = true;
            }
            catch
            {
            }
            Disconnect();
            return response;
        }
        public async Task<List<Career>> ListCareer()
        {
            List<Career> response = new List<Career>();
            try
            {
                Connect();

                command = new SqlCommand(listCareers, connection);
                command.CommandType = CommandType.StoredProcedure;

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new Career
                    {
                        ID = reader.GetInt32("Pk_Career"),
                        Code = reader.GetString("Code"),
                        CareerName = reader.GetString("CareerName"),
                        DegreeName = reader.GetString("DegreeName")
                    });
                }
                reader.Close();

                Disconnect();
            }
            catch
            {
                Disconnect();
                return null;
            }
            
            return response;
        }
        public async Task<Career> FindCareerById(int id)
        {
            
            try
            {
                Career response = null;

                Connect();

                command = new SqlCommand(listCareers, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Career", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Career
                    {
                        ID = reader.GetInt32("Pk_Career"),
                        Code = reader.GetString("Code"),
                        CareerName = reader.GetString("CareerName"),
                        DegreeName = reader.GetString("DegreeName")
                    });
                }
                reader.Close();

                Disconnect();
                return response;
            }
            catch
            {
                Disconnect();
                return null;
            }
        }
    }
}

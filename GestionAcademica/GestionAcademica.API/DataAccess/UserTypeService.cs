using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class UserTypeService : Service
    {
        private static readonly string listUsers = "udpFindUserType";
        private SqlCommand command = null;

        public UserTypeService() { }

        #region Read
        public async Task<List<UserType>> ListUserType()
        {
            List<UserType> response = new List<UserType>();
            try
            {
                Connect();
                command = new SqlCommand(listUsers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new UserType
                    {
                        ID = reader.GetInt32("Pk_UserType"),
                        TypeDescription = reader.GetString("TypeDescription")
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
        public async Task<UserType> FindUserType(int id)
        {
            try
            {
                UserType response = null;
                Connect();

                command = new SqlCommand(listUsers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_UserType", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = new UserType
                    {
                        ID = reader.GetInt32("Pk_UserType"),
                        TypeDescription = reader.GetString("TypeDescription")
                    };
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
        #endregion       
    }
}

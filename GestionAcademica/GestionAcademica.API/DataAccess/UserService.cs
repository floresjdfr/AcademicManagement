using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class UserService : Service
    {
        private static readonly string insertUser = "udpInsertUser";
        private static readonly string updateUser = "udpModifyUser";
        private static readonly string deleteUser = "udpDeleteUser";
        private static readonly string listUsers = "udpFindUser";
        private SqlCommand command = null;

        public UserService() { }

        #region Create
        public async Task<bool> InsertUser(User User)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertUser, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_UserType", User.UserType.ID));
                command.Parameters.Add(new SqlParameter("@ID", User.UserID));
                command.Parameters.Add(new SqlParameter("@Password", User.Password));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<User>> ListUser()
        {
            List<User> response = new List<User>();
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
                    response.Add(new User
                    {

                        ID = reader.GetInt32("Pk_User"),
                        UserID = reader.GetString("ID"),
                        Password = reader.GetString("Password"),
                        UserType = new UserType
                        {
                            ID = reader.GetInt32("Pk_UserType"),
                            TypeDescription = reader.GetString("TypeDescription")
                        }
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
        public async Task<User> FindUser(User user)
        {
            try
            {
                User response = null;

                Connect();

                command = new SqlCommand(listUsers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_User", user.ID));
                command.Parameters.Add(new SqlParameter("@ID", user.UserID));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new User
                    {
                        ID = reader.GetInt32("Pk_User"),
                        UserID = reader.GetString("ID"),
                        Password = reader.GetString("Password"),
                        UserType = new UserType
                        {
                            ID = reader.GetInt32("Pk_UserType"),
                            TypeDescription = reader.GetString("TypeDescription")
                        }
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
        #endregion

        #region Update
        public async Task<bool> UpdateUser(int UserId, User User)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateUser, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_User", UserId));
                if (User.UserType != null) command.Parameters.Add(new SqlParameter("@Fk_UserType", User.UserType.ID));
                else command.Parameters.Add(new SqlParameter("@Password", User.Password));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch
            {

            }
            Disconnect();
            return response;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteUser(int id)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteUser, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_User", id));

                var affectedRows = await command.ExecuteNonQueryAsync();

                if (affectedRows > 0) response = true;
            }
            catch
            {
            }
            Disconnect();
            return response;
        }
        #endregion

    }
}

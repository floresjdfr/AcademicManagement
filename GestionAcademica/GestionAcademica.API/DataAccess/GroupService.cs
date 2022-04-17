using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class GroupService : Service
    {
        private static readonly string insertGroup = "udpInsertGroup";
        private static readonly string updateGroup = "udpModifyGroup";
        private static readonly string deleteGroup = "udpDeleteGroup";
        private static readonly string listGroups = "udpFindGroup";
        private SqlCommand command = null;

        public GroupService() { }

        #region Create
        public async Task<bool> InsertGroup(Group group)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertGroup, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_GroupState", group.GroupState.ID));
                command.Parameters.Add(new SqlParameter("@Year", group.Year));
                command.Parameters.Add(new SqlParameter("@Number", group.Number));
                command.Parameters.Add(new SqlParameter("@StartDate", group.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", group.EndDate));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<Group>> ListGroup()
        {
            List<Group> response = new List<Group>();
            try
            {
                Connect();

                command = new SqlCommand(listGroups, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new Group
                    {
                        GroupState = new GroupState
                        {
                            ID = reader.GetInt32("Pk_GroupState"),
                            StateDescription = reader.GetString("StateDescription")
                        },
                        ID = reader.GetInt32("Pk_Group"),
                        Year = reader.GetInt32("Year"),
                        Number = reader.GetInt32("Number"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
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
        public async Task<Group> FindGroup(Group group)
        {
            try
            {
                Group response = null;

                Connect();

                command = new SqlCommand(listGroups, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Group", group.ID));
                if (group.GroupState != null) command.Parameters.Add(new SqlParameter("@Fk_GroupState", group.GroupState.ID));
                command.Parameters.Add(new SqlParameter("@Year", group.Year));
                command.Parameters.Add(new SqlParameter("@Number", group.Number));
                command.Parameters.Add(new SqlParameter("@StartDate", group.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", group.EndDate));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Group
                    {
                        GroupState = new GroupState
                        {
                            ID = reader.GetInt32("Pk_GroupState"),
                            StateDescription = reader.GetString("StateDescription")
                        },
                        ID = reader.GetInt32("Pk_Group"),
                        Year = reader.GetInt32("Year"),
                        Number = reader.GetInt32("Number"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
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
        public async Task<bool> UpdateGroup(int GroupId, Group group)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateGroup, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Group", GroupId));
                command.Parameters.Add(new SqlParameter("@Fk_GroupState", group.GroupState.ID));
                command.Parameters.Add(new SqlParameter("@Year", group.Year));
                command.Parameters.Add(new SqlParameter("@Number", group.Number));
                command.Parameters.Add(new SqlParameter("@StartDate", group.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", group.EndDate));
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
        public async Task<bool> DeleteGroup(int id)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteGroup, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Group", id));

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
}

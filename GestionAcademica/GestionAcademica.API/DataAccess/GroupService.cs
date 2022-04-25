using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public enum GroupUpdateType
    {
        NumberAndSchedule = 1,
        Teacher = 2,
        Cycle = 3
    }
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
                command.Parameters.Add(new SqlParameter("@Number", group.Number));
                command.Parameters.Add(new SqlParameter("@Schedule", group.Schedule));

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
                        Cycle = !reader.IsDBNull("Pk_Cycle") ? new Cycle
                        {
                            CycleState = !reader.IsDBNull("Pk_CycleState") ? new CycleState
                            {
                                ID = reader.GetInt32("Pk_CycleState"),
                                StateDescription = reader.GetString("StateDescription")
                            } : null,
                            ID = reader.GetInt32("Pk_Cycle"),
                            Year = reader.GetInt32("Year"),
                            Number = reader.GetInt32("Number"),
                            StartDate = reader.GetDateTime("StartDate"),
                            EndDate = reader.GetDateTime("EndDate"),
                        } : null,
                        Teacher = !reader.IsDBNull("Pk_Teacher") ? new Teacher
                        {
                            ID = reader.GetInt32("Pk_Teacher"),
                            IdIdentidad = reader.GetString("ID"),
                            Name = reader.GetString("Name"),
                            PhoneNumber = reader.GetString("PhoneNumber"),
                            Email = reader.GetString("Email")
                        } : null,
                        Number = reader.GetString("Number"),
                        Schedule = reader.GetString("Schedule"),
                        ID = reader.GetInt32("Pk_Group")
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
                if (group.Teacher != null) command.Parameters.Add(new SqlParameter("@Fk_Teacher", group.Teacher.ID));
                if (group.Cycle != null) command.Parameters.Add(new SqlParameter("@Fk_Cycle", group.Cycle.ID));
                command.Parameters.Add(new SqlParameter("@Number", group.Number));
                command.Parameters.Add(new SqlParameter("@Schedule", group.Schedule));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Group
                    {
                        Cycle = !reader.IsDBNull("Pk_Cycle") ? new Cycle
                        {
                            CycleState = !reader.IsDBNull("Pk_CycleState") ? new CycleState
                            {
                                ID = reader.GetInt32("Pk_CycleState"),
                                StateDescription = reader.GetString("StateDescription")
                            } : null,
                            ID = reader.GetInt32("Pk_Cycle"),
                            Year = reader.GetInt32("Year"),
                            Number = reader.GetInt32("CycleNumber"),
                            StartDate = reader.GetDateTime("StartDate"),
                            EndDate = reader.GetDateTime("EndDate"),
                        } : null,
                        Teacher = !reader.IsDBNull("Pk_Teacher") ? new Teacher
                        {
                            ID = reader.GetInt32("Pk_Teacher"),
                            IdIdentidad = reader.GetString("ID"),
                            Name = reader.GetString("Name"),
                            PhoneNumber = reader.GetString("PhoneNumber"),
                            Email = reader.GetString("Email")
                        } : null,
                        Number = reader.GetString("GroupNumber"),
                        Schedule = reader.GetString("Schedule"),
                        ID = reader.GetInt32("Pk_Group")
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
                if (group.Teacher != null) command.Parameters.Add(new SqlParameter("@Fk_Teacher", group.Teacher.ID));
                if (group.Cycle != null) command.Parameters.Add(new SqlParameter("@Fk_Cycle", group.Cycle.ID));
                command.Parameters.Add(new SqlParameter("@Number", group.Number));
                command.Parameters.Add(new SqlParameter("@Schedule", group.Schedule));
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


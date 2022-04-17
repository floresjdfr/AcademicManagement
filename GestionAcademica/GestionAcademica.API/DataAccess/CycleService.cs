using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class CycleService : Service
    {
        private static readonly string insertCycle = "udpInsertCycle";
        private static readonly string updateCycle = "udpModifyCycle";
        private static readonly string deleteCycle = "udpDeleteCycle";
        private static readonly string listCycles = "udpFindCycle";
        private SqlCommand command = null;

        public CycleService() { }

        #region Create
        public async Task<bool> InsertCycle(Cycle cycle)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertCycle, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_CycleState", cycle.CycleState.ID));
                command.Parameters.Add(new SqlParameter("@Year", cycle.Year));
                command.Parameters.Add(new SqlParameter("@Number", cycle.Number));
                command.Parameters.Add(new SqlParameter("@StartDate", cycle.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", cycle.EndDate));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<Cycle>> ListCycle()
        {
            List<Cycle> response = new List<Cycle>();
            try
            {
                Connect();

                command = new SqlCommand(listCycles, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new Cycle
                    {
                        CycleState = new CycleState
                        {
                            ID = reader.GetInt32("Pk_CycleState"),
                            StateDescription = reader.GetString("StateDescription")
                        },
                        ID = reader.GetInt32("Pk_Cycle"),
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
        public async Task<Cycle> FindCycle(Cycle cycle)
        {
            try
            {
                Cycle response = null;

                Connect();

                command = new SqlCommand(listCycles, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Cycle", cycle.ID));
                if (cycle.CycleState != null) command.Parameters.Add(new SqlParameter("@Fk_CycleState", cycle.CycleState.ID));
                command.Parameters.Add(new SqlParameter("@Year", cycle.Year));
                command.Parameters.Add(new SqlParameter("@Number", cycle.Number));
                command.Parameters.Add(new SqlParameter("@StartDate", cycle.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", cycle.EndDate));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Cycle
                    {
                        CycleState = new CycleState
                        {
                            ID = reader.GetInt32("Pk_CycleState"),
                            StateDescription = reader.GetString("StateDescription")
                        },
                        ID = reader.GetInt32("Pk_Cycle"),
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
        public async Task<bool> UpdateCycle(int CycleId, Cycle cycle)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateCycle, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Cycle", CycleId));
                command.Parameters.Add(new SqlParameter("@Fk_CycleState", cycle.CycleState.ID));
                command.Parameters.Add(new SqlParameter("@Year", cycle.Year));
                command.Parameters.Add(new SqlParameter("@Number", cycle.Number));
                command.Parameters.Add(new SqlParameter("@StartDate", cycle.StartDate));
                command.Parameters.Add(new SqlParameter("@EndDate", cycle.EndDate));
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
        public async Task<bool> DeleteCycle(int id)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteCycle, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Cycle", id));

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

using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class CycleStateService : Service
    {
        private static readonly string listUsers = "udpFindCycleState";
        private SqlCommand command = null;

        public CycleStateService() { }

        #region Read
        public async Task<List<CycleState>> ListCycleState()
        {
            List<CycleState> response = new List<CycleState>();
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
                    response.Add(new CycleState
                    {
                        ID = reader.GetInt32("Pk_CycleState"),
                        StateDescription = reader.GetString("StateDescription")
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
        public async Task<CycleState> FindCycleState(int id)
        {
            try
            {
                CycleState response = null;
                Connect();

                command = new SqlCommand(listUsers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_CycleState", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = new CycleState
                    {
                        ID = reader.GetInt32("Pk_CycleState"),
                        StateDescription = reader.GetString("StateDescription")
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

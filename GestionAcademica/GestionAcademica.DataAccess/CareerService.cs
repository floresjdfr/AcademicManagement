using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GestionAcademica.DataAccess
{
    public class CareerService : Service
    {
        private static readonly string insertCareer = "udpInsertCareer";
        private static readonly string listCareers = "udpFindCareer";
        private SqlCommand command = null;

        public CareerService() { }
        public Career InsertCareer(Career career)
        {
            Career careerAdded = null;
            try
            {
                Connect();

                command = new SqlCommand(insertCareer, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Code", career.Code));
                command.Parameters.Add(new SqlParameter("@CareerName", career.CareerName));
                command.Parameters.Add(new SqlParameter("@DegreeName", career.DegreeName));

                var reader = command.ExecuteReader();
                
                reader.Read();
                var addedElementID = Convert.ToInt32(reader.GetDecimal(0));
                reader.Close();
                
                careerAdded = FindCareer(addedElementID);

                Disconnect();
            }
            catch
            {
                Disconnect();
            }
            return careerAdded;
        }

        public List<Career> ListCareer()
        {
            List<Career> list = new List<Career>();
            try
            {
                Connect();

                command = new SqlCommand(listCareers, connection);
                command.CommandType = CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Career
                    {
                        PK = reader.GetInt32("Pk_Career"),
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
            }
            return list;
        }
        public Career FindCareer(int id)
        {
            Career career = null;
            try
            {
                Connect();

                command = new SqlCommand(listCareers, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Career", id));
                
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    career = new Career
                    {
                        PK = reader.GetInt32("Pk_Career"),
                        Code = reader.GetString("Code"),
                        CareerName = reader.GetString("CareerName"),
                        DegreeName = reader.GetString("DegreeName")
                    };
                }
                reader.Close();

                Disconnect();
            }
            catch
            {
                Disconnect();
            }
            return career;
        }
    }

}

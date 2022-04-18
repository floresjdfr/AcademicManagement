using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class StudentService : Service
    {
        private static readonly string insertStudent = "udpInsertStudent";
        private static readonly string updateStudent = "udpModifyStudent";
        private static readonly string deleteStudent = "udpDeleteStudent";
        private static readonly string listStudents = "udpFindStudent";
        private SqlCommand command = null;

        public StudentService() { }

        #region Create
        public async Task<bool> InsertStudent(Student Student)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertStudent, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@ID", Student.IdStudent));
                command.Parameters.Add(new SqlParameter("@Name", Student.Name));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", Student.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@Email", Student.Email));
                command.Parameters.Add(new SqlParameter("@DateOfBirth", Student.DateOfBirth));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<Student>> ListStudent()
        {
            List<Student> response = new List<Student>();
            try
            {
                Connect();

                command = new SqlCommand(listStudents, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new Student
                    {
                        ID = reader.GetInt32("Pk_Student"),
                        IdStudent = reader.GetString("ID"),
                        Name = reader.GetString("Name"),
                        PhoneNumber = reader.GetString("PhoneNumber"),
                        Email = reader.GetString("Email")
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
        public async Task<Student> FindStudent(int id)
        {
            try
            {
                Student response = null;

                Connect();

                command = new SqlCommand(listStudents, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Student", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Student
                    {
                        ID = reader.GetInt32("Pk_Student"),
                        Name = reader.GetString("Name"),
                        PhoneNumber = reader.GetString("PhoneNumber"),
                        Email = reader.GetString("Email")
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
        public async Task<bool> UpdateStudent(int StudentId, Student Student)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateStudent, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Student", StudentId));
                command.Parameters.Add(new SqlParameter("@Name", Student.Name));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", Student.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@Email", Student.Email));
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
        public async Task<bool> DeleteStudent(int id)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteStudent, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Student", id));

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
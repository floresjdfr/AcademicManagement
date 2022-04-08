using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class CourseService : Service
    {
        private static readonly string insertCourse = "udpInsertCourse";
        private static readonly string updateCourse = "udpModifyCourse";
        private static readonly string listCourses = "udpFindCourse"; 
        private static readonly string deleteCourse = "udpDeleteCourse";
        private SqlCommand command = null;

        public CourseService() { }


        #region Create
        public async Task<bool> InsertCourse(Course Course)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertCourse, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Code", Course.Code));
                command.Parameters.Add(new SqlParameter("@Name", Course.Name));
                command.Parameters.Add(new SqlParameter("@Credits", Course.Credits));
                command.Parameters.Add(new SqlParameter("@WeeklyHours", Course.WeeklyHours));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion
        
        #region Read
        public async Task<List<Course>> ListCourses()
        {
            List<Course> response = new List<Course>();
            try
            {
                Connect();

                command = new SqlCommand(listCourses, connection);
                command.CommandType = CommandType.StoredProcedure;

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new Course
                    {
                        ID = reader.GetInt32("Pk_Course"),
                        Code = reader.GetString("Code"),
                        Name = reader.GetString("Name"),
                        Credits = reader.GetInt32("Credits"),
                        WeeklyHours = reader.GetInt32("WeeklyHours")
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
        public async Task<Course> FindCourseById(int id)
        {

            try
            {
                Course response = null;

                Connect();

                command = new SqlCommand(listCourses, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Course", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Course
                    {
                        ID = reader.GetInt32("Pk_Course"),
                        Code = reader.GetString("Code"),
                        Name = reader.GetString("Name"),
                        Credits = reader.GetInt32("Credits"),
                        WeeklyHours = reader.GetInt32("WeeklyHours")
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
        public async Task<bool> UpdateCourse(int courseId, Course Course)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateCourse, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Course", courseId));
                command.Parameters.Add(new SqlParameter("@Code", Course.Code));
                command.Parameters.Add(new SqlParameter("@Name", Course.Name));
                command.Parameters.Add(new SqlParameter("@Credits", Course.Credits));
                command.Parameters.Add(new SqlParameter("@WeeklyHours", Course.WeeklyHours));

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
        public async Task<bool> DeleteCourse(int courseId)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteCourse, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Course", courseId));

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

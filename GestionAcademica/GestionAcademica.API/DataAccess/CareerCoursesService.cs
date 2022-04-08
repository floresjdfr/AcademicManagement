using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class CareerCoursesService : Service
    {
        private static readonly string addCourseToCareer = "udpAddCourseToCareer";
        private static readonly string getCoursesByCareer = "udpGetCoursesByCareer";
        private static readonly string deleteCourseFromCareer = "udpDeleteCourseFromCareer";
        private SqlCommand command = null;

        public CareerCoursesService() { }


        #region Create
        public async Task<bool> AddCourseToCareer(CareerCourses careerCourse)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(addCourseToCareer, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Course", careerCourse.Course.ID));
                command.Parameters.Add(new SqlParameter("@Pk_Career", careerCourse.Career.ID));
                command.Parameters.Add(new SqlParameter("@Year", careerCourse.Year));
                command.Parameters.Add(new SqlParameter("@Cycle", careerCourse.Cycle));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<CareerCourses>> GetCoursesByCareer(int careerId)
        {
            List<CareerCourses> response = new List<CareerCourses>();
            try
            {
                Connect();

                command = new SqlCommand(getCoursesByCareer, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_Career", careerId));

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Course tmpCourse = new Course
                    {
                        ID = reader.GetInt32("Pk_Course"),
                        Code = reader.GetString("Code"),
                        Name = reader.GetString("Name"),
                        Credits = reader.GetInt32("Credits"),
                        WeeklyHours = reader.GetInt32("WeeklyHours")
                    };
                    Career tmpCareer = new Career
                    {
                        ID = reader.GetInt32("Fk_Career")
                    };
                    CareerCourses tmpCareerCourse = new CareerCourses
                    {
                        ID = reader.GetInt32("Pk_CareerCourses"),
                        Course = tmpCourse,
                        Career = tmpCareer,
                        Year = reader.GetInt32("Year"),
                        Cycle = reader.GetInt32("Cycle")
                    };
                    response.Add(tmpCareerCourse);
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
        #endregion

        #region Delete
        public async Task<bool> DeleteCourseFromCareer(int carreerCoursesId)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteCourseFromCareer, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Pk_CareerCourses", carreerCoursesId));

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

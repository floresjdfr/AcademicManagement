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
        private static readonly string getCoursesByCareer = "udpFindCoursesByCareer";
        private static readonly string findCareerCourse = "udpFindCareerCourse";
        private static readonly string deleteCourseFromCareer = "udpDeleteCourseFromCareer";
        private static readonly string updateCareerCourse = "udpUpdateCourseAndCareer";
        private SqlCommand command = null;

        public CareerCoursesService() { }


        #region Create
        public async Task<bool> AddCourseToCareer(CareerCourses careerCourse)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(addCourseToCareer, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Career", careerCourse.Career.ID));
                command.Parameters.Add(new SqlParameter("@Code", careerCourse.Course.Code));
                command.Parameters.Add(new SqlParameter("@Name", careerCourse.Course.Name));
                command.Parameters.Add(new SqlParameter("@Credits", careerCourse.Course.Credits));
                command.Parameters.Add(new SqlParameter("@WeeklyHours", careerCourse.Course.WeeklyHours));
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
        public async Task<CareerCourses> FindCareerCourse(int careerCourseId)
        {
            CareerCourses response = null;
            try
            {
                Connect();

                command = new SqlCommand(findCareerCourse, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_CareerCourse", careerCourseId));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
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
                        ID = reader.GetInt32("Fk_Career"),
                        Code = reader.GetString("Code"),
                        CareerName = reader.GetString("CareerName"),
                        DegreeName = reader.GetString("DegreeName")
                    };
                    CareerCourses tmpCareerCourse = new CareerCourses
                    {
                        ID = reader.GetInt32("Pk_CareerCourses"),
                        Course = tmpCourse,
                        Career = tmpCareer,
                        Year = reader.GetInt32("Year"),
                        Cycle = reader.GetInt32("Cycle")
                    };
                    response = tmpCareerCourse;
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
        public async Task<List<CareerCourses>> GetCoursesByCareer(int careerId)
        {
            List<CareerCourses> response = new List<CareerCourses>();
            try
            {
                Connect();

                command = new SqlCommand(getCoursesByCareer, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
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
        public async Task<bool> UpdateCareerCourse(int careerCourseId, CareerCourses careerCourses)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateCareerCourse, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_CareerCourses", careerCourseId));
                command.Parameters.Add(new SqlParameter("@Code", careerCourses.Course.Code));
                command.Parameters.Add(new SqlParameter("@Name", careerCourses.Course.Name));
                command.Parameters.Add(new SqlParameter("@Credits", careerCourses.Course.Credits));
                command.Parameters.Add(new SqlParameter("@WeeklyHours", careerCourses.Course.WeeklyHours));
                command.Parameters.Add(new SqlParameter("@Year", careerCourses.Year));
                command.Parameters.Add(new SqlParameter("@Cycle", careerCourses.Cycle));

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
        public async Task<bool> DeleteCourseFromCareer(int carreerCoursesId)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteCourseFromCareer, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
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

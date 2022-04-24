using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class CourseGroupsService : Service
    {
        private static readonly string addGroupToCourse = "udpAddGroupToCourse";
        private static readonly string findGroupsByCourse = "udpFindGroupsByCourse";
        //private static readonly string findCareerCourse = "udpFindCareerCourse";
        //private static readonly string findCourseGroupsByCareerAndCycle = "udpFindCourseGroupsByCareerAndCicle";
        //private static readonly string deleteCourseFromCareer = "udpDeleteCourseFromCareer";
        //private static readonly string updateCareerCourse = "udpUpdateCourseAndCareer";
        private SqlCommand command = null;

        public CourseGroupsService() { }


        #region Create
        public async Task<bool> AddGroupToCourse(CourseGroups courseGroups)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(addGroupToCourse, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_Course", courseGroups.Course.ID));
                command.Parameters.Add(new SqlParameter("@Fk_Teacher", courseGroups.Group.Teacher.ID));
                command.Parameters.Add(new SqlParameter("@Number", courseGroups.Group.Number));
                command.Parameters.Add(new SqlParameter("@Schedule", courseGroups.Group.Schedule));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 1) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        //public async Task<CourseGroups> FindCareerCourse(int courseGroupsId)
        //{
        //    CourseGroups response = null;
        //    try
        //    {
        //        Connect();

        //        command = new SqlCommand(findCareerCourse, connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        command.Parameters.Add(new SqlParameter("@Pk_CareerCourse", courseGroupsId));

        //        var reader = await command.ExecuteReaderAsync();
        //        if (reader.Read())
        //        {
        //            Course tmpCourse = new Course
        //            {
        //                ID = reader.GetInt32("Pk_Course"),
        //                Code = reader.GetString("Code"),
        //                Name = reader.GetString("Name"),
        //                Credits = reader.GetInt32("Credits"),
        //                WeeklyHours = reader.GetInt32("WeeklyHours")
        //            };
        //            Career tmpCareer = new Career
        //            {
        //                ID = reader.GetInt32("Fk_Career"),
        //                Code = reader.GetString("Code"),
        //                CareerName = reader.GetString("CareerName"),
        //                DegreeName = reader.GetString("DegreeName")
        //            };
        //            CourseGroups tmpCareerCourse = new CourseGroups
        //            {
        //                ID = reader.GetInt32("Pk_CourseGroups"),
        //                Course = tmpCourse,
        //                Career = tmpCareer,
        //                Year = reader.GetInt32("Year"),
        //                Cycle = reader.GetInt32("Cycle")
        //            };
        //            response = tmpCareerCourse;
        //        }
        //        reader.Close();

        //        Disconnect();
        //        return response;
        //    }
        //    catch
        //    {
        //        Disconnect();
        //        return null;
        //    }
        //}
        //public async Task<List<CourseGroups>> FindCourseGroupsByCareerAndCycle(int careerId, int cycle)
        //{
        //    List<CourseGroups> response = new List<CourseGroups>();
        //    try
        //    {
        //        Connect();

        //        command = new SqlCommand(findCourseGroupsByCareerAndCycle, connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        command.Parameters.Add(new SqlParameter("@Fk_Career", careerId));
        //        command.Parameters.Add(new SqlParameter("@Cycle", cycle));

        //        var reader = await command.ExecuteReaderAsync();
        //        while (reader.Read())
        //        {
        //            Course tmpCourse = new Course
        //            {
        //                ID = reader.GetInt32("Pk_Course"),
        //                Code = reader.GetString("Code"),
        //                Name = reader.GetString("Name"),
        //                Credits = reader.GetInt32("Credits"),
        //                WeeklyHours = reader.GetInt32("WeeklyHours")
        //            };
        //            Career tmpCareer = new Career
        //            {
        //                ID = reader.GetInt32("Fk_Career"),
        //                Code = reader.GetString("Code"),
        //                CareerName = reader.GetString("CareerName"),
        //                DegreeName = reader.GetString("DegreeName")
        //            };
        //            CourseGroups tmpCareerCourse = new CourseGroups
        //            {
        //                ID = reader.GetInt32("Pk_CourseGroups"),
        //                Course = tmpCourse,
        //                Career = tmpCareer,
        //                Year = reader.GetInt32("Year"),
        //                Cycle = reader.GetInt32("Cycle")
        //            };
        //            response.Add(tmpCareerCourse);
        //        }
        //        reader.Close();

        //        Disconnect();
        //        return response;
        //    }
        //    catch
        //    {
        //        Disconnect();
        //        return null;
        //    }
        //}
        public async Task<List<CourseGroups>> FindGroupsByCourse(int courseId)
        {
            List<CourseGroups> response = new List<CourseGroups>();
            try
            {
                Connect();

                command = new SqlCommand(findGroupsByCourse, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_Course", courseId));

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
                    Group tmpGroup = new Group
                    {
                        ID = reader.GetInt32("Pk_Group"),
                        Number = reader.GetString("Number"),
                        Schedule = reader.GetString("Schedule")
                    };
                    if (!reader.IsDBNull("Fk_Teacher"))
                    {
                        tmpGroup.Teacher = new Teacher
                        {
                            ID = reader.GetInt32("Pk_Teacher"),
                            IdIdentidad = reader.GetString("ID"),
                            Name = reader.GetString("TeacherName"),
                            PhoneNumber = reader.GetString("PhoneNumber"),
                            Email = reader.GetString("Email")
                        };
                    }
                    if (!reader.IsDBNull("Fk_Cycle"))
                    {
                        tmpGroup.Cycle = new Cycle
                        {
                            CycleState = new CycleState
                            {
                                ID = reader.GetInt32("Pk_CycleState"),
                                StateDescription = reader.GetString("StateDescription")
                            },
                            ID = reader.GetInt32("Pk_Cycle"),
                            Year = reader.GetInt32("Year"),
                            Number = reader.GetInt32("CycleNumber"),
                            StartDate = reader.GetDateTime("StartDate"),
                            EndDate = reader.GetDateTime("EndDate"),
                        };
                    }
                    CourseGroups tmpCareerCourse = new CourseGroups
                    {
                        ID = reader.GetInt32("Pk_CourseGroups"),
                        Course = tmpCourse,
                        Group = tmpGroup
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
        //public async Task<bool> UpdateCareerCourse(int courseGroupsId, CourseGroups CourseGroups)
        //{
        //    bool response = false;
        //    try
        //    {
        //        Connect();

        //        command = new SqlCommand(updateCareerCourse, connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        command.Parameters.Add(new SqlParameter("@Pk_CourseGroups", courseGroupsId));
        //        command.Parameters.Add(new SqlParameter("@Code", CourseGroups.Course.Code));
        //        command.Parameters.Add(new SqlParameter("@Name", CourseGroups.Course.Name));
        //        command.Parameters.Add(new SqlParameter("@Credits", CourseGroups.Course.Credits));
        //        command.Parameters.Add(new SqlParameter("@WeeklyHours", CourseGroups.Course.WeeklyHours));
        //        command.Parameters.Add(new SqlParameter("@Year", CourseGroups.Year));
        //        command.Parameters.Add(new SqlParameter("@Cycle", CourseGroups.Cycle));

        //        var rowsAffected = await command.ExecuteNonQueryAsync();

        //        if (rowsAffected > 0) response = true;
        //    }
        //    catch
        //    {

        //    }
        //    Disconnect();
        //    return response;
        //}
        #endregion

        #region Delete
        //public async Task<bool> DeleteCourseFromCareer(int carreerCoursesId)
        //{
        //    bool response = false;
        //    try
        //    {
        //        Connect();

        //        command = new SqlCommand(deleteCourseFromCareer, connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        command.Parameters.Add(new SqlParameter("@Pk_CourseGroups", carreerCoursesId));

        //        var affectedRows = await command.ExecuteNonQueryAsync();

        //        if (affectedRows > 0) response = true;
        //    }
        //    catch
        //    {
        //    }
        //    Disconnect();
        //    return response;
        //}
        #endregion

    }
}

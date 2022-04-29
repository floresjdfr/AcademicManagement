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
        private static readonly string findCourseGroupByID = "udpFindCourseGroupByID";
        private static readonly string findCourseGroupByCurrentCycle = "udpFindCourseGroupsCurrentCycle";
        private static readonly string deleteGroupFromCourse = "udpDeleteGroupFromCourse";
        private SqlCommand command = null;

        public CourseGroupsService() { }


        #region Helpers
        public static CourseGroups ReadCourseGroupsListFromDB(SqlDataReader reader, bool course = false, bool group = false, bool teacher = false, bool cycle = false, bool cycleState = false)
        {
            var newCourseGroup = new CourseGroups()
            {
                ID = reader.GetInt32("Pk_CourseGroups")
            };

            if (course && !reader.IsDBNull("Pk_Course"))
            {
                newCourseGroup.Course = new Course
                {
                    ID = reader.GetInt32("Pk_Course"),
                    Code = reader.GetString("Code"),
                    Name = reader.GetString("Name"),
                    Credits = reader.GetInt32("Credits"),
                    WeeklyHours = reader.GetInt32("WeeklyHours")
                };
            }
            if (group && !reader.IsDBNull("Pk_Group"))
            {
                newCourseGroup.Group = new Group
                {
                    ID = reader.GetInt32("Pk_Group"),
                    Number = reader.GetString("Number"),
                    Schedule = reader.GetString("Schedule")
                };
                if (teacher && !reader.IsDBNull("Fk_Teacher"))
                {
                    newCourseGroup.Group.Teacher = new Teacher
                    {
                        ID = reader.GetInt32("Pk_Teacher"),
                        IdIdentidad = reader.GetString("ID"),
                        Name = reader.GetString("TeacherName"),
                        PhoneNumber = reader.GetString("PhoneNumber"),
                        Email = reader.GetString("Email")
                    };
                }
                if (cycle && !reader.IsDBNull("Fk_Cycle"))
                {
                    newCourseGroup.Group.Cycle = new Cycle
                    {
                        CycleState = cycleState && reader.IsDBNull("Fk_CycleState") ? new CycleState
                        {
                            ID = reader.GetInt32("Pk_CycleState"),
                            StateDescription = reader.GetString("StateDescription")
                        } : null,
                        ID = reader.GetInt32("Pk_Cycle"),
                        Year = reader.GetInt32("Year"),
                        Number = reader.GetInt32("CycleNumber"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
                    };
                }
            }
            return newCourseGroup;
        }
        #endregion


        #region Create
        public async Task<bool> AddGroupToCourse(CourseGroups courseGroups)
        {
            bool response = false;

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

            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<CourseGroups>> FindCourseGroupsByCurrentCycle()
        {
            List<CourseGroups> response = new List<CourseGroups>();
            try
            {
                Connect();

                command = new SqlCommand(findCourseGroupByCurrentCycle, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

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
        public async Task<CourseGroups> FindCourseGroupsByID(int courseGroupId)
        {
            CourseGroups response = null;
            try
            {
                Connect();

                command = new SqlCommand(findCourseGroupByID, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_CourseGroups", courseGroupId));

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
        public async Task<bool> DeleteCourseFromCareer(int courseGroupId)
        {
            bool response = false;
            try
            {
                Connect();
                command = new SqlCommand(deleteGroupFromCourse, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_CourseGroups", courseGroupId));

                var affectedRows = await command.ExecuteNonQueryAsync();

                if (affectedRows > 1) response = true;
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

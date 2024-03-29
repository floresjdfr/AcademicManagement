﻿using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class TeacherService : Service
    {
        private static readonly string insertTeacher = "udpInsertTeacher";
        private static readonly string updateTeacher = "udpModifyTeacher";
        private static readonly string deleteTeacher = "udpDeleteTeacher";
        private static readonly string listTeachers = "udpFindTeacher";
        private static readonly string findTeacherGroups = "udpFindTeacherGroups";
        private SqlCommand command = null;

        public TeacherService() { }

        #region Create
        public async Task<bool> InsertTeacher(Teacher Teacher)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertTeacher, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@ID", Teacher.IdIdentidad));
                command.Parameters.Add(new SqlParameter("@Name", Teacher.Name));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", Teacher.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@Email", Teacher.Email));
                command.Parameters.Add(new SqlParameter("@Fk_User", Teacher.User.ID));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<Teacher>> ListTeacher()
        {
            List<Teacher> response = new List<Teacher>();
            try
            {
                Connect();

                command = new SqlCommand(listTeachers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new Teacher
                    {
                        ID = reader.GetInt32("Pk_Teacher"),
                        IdIdentidad = reader.GetString("ID"),
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
        public async Task<Teacher> FindTeacher(int id)
        {
            try
            {
                Teacher response = null;

                Connect();

                command = new SqlCommand(listTeachers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Teacher", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Teacher
                    {
                        ID = reader.GetInt32("Pk_Teacher"),
                        IdIdentidad = reader.GetString("ID"),
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
        public async Task<Teacher> FindTeacherByUser(int id)
        {
            try
            {
                Teacher response = null;

                Connect();

                command = new SqlCommand(listTeachers, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_User", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    response = (new Teacher
                    {
                        ID = reader.GetInt32("Pk_Teacher"),
                        IdIdentidad = reader.GetString("ID"),
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
        public async Task<List<CourseGroups>> FindTeacherGroups(int teacherID, int cycleStateID = 1)
        {
            try
            {
                List<CourseGroups> response = new List<CourseGroups>();

                Connect();

                command = new SqlCommand(findTeacherGroups, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@pk_teacher", teacherID));
                command.Parameters.Add(new SqlParameter("@pk_cycleState", cycleStateID));

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
        public async Task<bool> UpdateTeacher(int TeacherId, Teacher Teacher)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateTeacher, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Teacher", TeacherId));
                command.Parameters.Add(new SqlParameter("@ID", Teacher.IdIdentidad));
                command.Parameters.Add(new SqlParameter("@Name", Teacher.Name));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", Teacher.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@Email", Teacher.Email));
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
        public async Task<bool> DeleteTeacher(int id)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(deleteTeacher, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Teacher", id));

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

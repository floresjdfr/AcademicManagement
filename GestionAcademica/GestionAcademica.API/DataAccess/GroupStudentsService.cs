using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class GroupStudentsService : Service
    {
        private static readonly string insertGroupStudents = "udpInsertGroupStudent";
        private static readonly string findGroupStudents = "udpFindGroupStudents";
        private static readonly string updateGroupStudentsScore = "udpUpdateGroupStudentsScore";
        private static readonly string deleteGroupStudents = "udpDeleteGroupStudent";
        private SqlCommand command = null;

        public GroupStudentsService() { }


        #region Create
        public async Task<bool> InsertGroupStudents(GroupStudents groupStudent)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(insertGroupStudents, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_Group", groupStudent.Group.ID));
                command.Parameters.Add(new SqlParameter("@Fk_Student", groupStudent.Student.ID));

                var rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 1) response = true;
            }
            catch { }
            Disconnect();
            return response;
        }
        #endregion

        #region Read
        public async Task<List<GroupStudents>> FindGroupStudents(int? Pk_GroupStudents = null, int? Fk_Student = null, int? Fk_Group = null, int? Fk_CycleState = null)
        {
            List<GroupStudents> response = new List<GroupStudents>();
            try
            {
                Connect();

                command = new SqlCommand(findGroupStudents, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_GroupStudents", Pk_GroupStudents));
                command.Parameters.Add(new SqlParameter("@Fk_Student", Fk_Student));
                command.Parameters.Add(new SqlParameter("@Fk_Group", Fk_Group));
                command.Parameters.Add(new SqlParameter("@FK_CycleState", Fk_CycleState));

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Student tmpStudent = new Student
                    {
                        ID = reader.GetInt32("Pk_Student"),
                        IdStudent = reader.GetString("StudentID"),
                        Name = reader.GetString("StudentName"),
                        PhoneNumber = reader.GetString("StudentPhoneNumber"),
                        Email = reader.GetString("StudentEmail"),
                        DateOfBirth = reader.GetDateTime("DateOfBirth")
                    };
                    Group tmpGroup = new Group
                    {
                        ID = reader.GetInt32("Pk_Group"),
                        Number = reader.GetString("GroupNumber"),
                        Schedule = reader.GetString("Schedule")
                    };
                    if (!reader.IsDBNull("Pk_Teacher"))
                    {
                        tmpGroup.Teacher = new Teacher
                        {
                            ID = reader.GetInt32("Pk_Teacher"),
                            IdIdentidad = reader.GetString("TeacherID"),
                            Name = reader.GetString("TeacherName"),
                            PhoneNumber = reader.GetString("TeacherPhoneNumber"),
                            Email = reader.GetString("TeacherEmail")
                        };
                    }
                    if (!reader.IsDBNull("Pk_Cycle"))
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
                    GroupStudents tmpGroupStudent = new GroupStudents
                    {
                        ID = reader.GetInt32("Pk_GroupStudents"),
                        Score = !reader.IsDBNull("Score") ? reader.GetDouble("Score") : null,
                        Group = tmpGroup,
                        Student = tmpStudent
                    };
                    response.Add(tmpGroupStudent);
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
        public async Task<bool> UpdateGroupStudentScore(int groupStudentId, GroupStudents groupStudent)
        {
            bool response = false;
            try
            {
                Connect();

                command = new SqlCommand(updateGroupStudentsScore, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_GroupStudents", groupStudentId));
                command.Parameters.Add(new SqlParameter("@Score", groupStudent.Score));

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
        public async Task<bool> DeleteGroupStudent(int groupStudentID)
        {
            bool response = false;
            try
            {
                Connect();
                command = new SqlCommand(deleteGroupStudents, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_GroupStudents", groupStudentID));

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

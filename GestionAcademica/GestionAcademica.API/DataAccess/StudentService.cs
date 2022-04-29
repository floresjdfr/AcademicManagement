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
        private static readonly string findStudentCycles = "udpFindStudentCycles";
        private static readonly string findStudentAcademicHistory = "udpFindStudentAcademicHistory";


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

                command.Parameters.Add(new SqlParameter("@Fk_Career", Student.Career.ID));
                command.Parameters.Add(new SqlParameter("@Fk_User", Student.User.ID));

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
                    var newStudent = new Student();
                    if (!reader.IsDBNull("Fk_Carreer"))
                        newStudent.Career = new Career
                        {
                            ID = reader.GetInt32("Pk_Career"),
                            Code = reader.GetString("Code"),
                            CareerName = reader.GetString("CareerName"),
                            DegreeName = reader.GetString("DegreeName")
                        };
                    if (!reader.IsDBNull("Fk_User"))
                    {
                        newStudent.User = new User
                        {
                            ID = reader.GetInt32("Pk_User"),
                            UserID = reader.GetString("ID"),
                            Password = reader.GetString("Password"),
                            UserType = new UserType
                            {
                                ID = reader.GetInt32("Pk_UserType"),
                                TypeDescription = reader.GetString("TypeDescription")
                            }
                        };
                    }
                    newStudent.ID = reader.GetInt32("Pk_Student");
                    newStudent.IdStudent = reader.GetString("ID");
                    newStudent.Name = reader.GetString("Name");
                    newStudent.PhoneNumber = reader.GetString("PhoneNumber");
                    newStudent.Email = reader.GetString("Email");
                    newStudent.DateOfBirth = reader.GetDateTime("DateOfBirth");
                    response.Add(newStudent);
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
                    var studentFound = new Student();
                    if (!reader.IsDBNull("Fk_Carreer"))
                        studentFound.Career = new Career
                        {
                            ID = reader.GetInt32("Pk_Career"),
                            Code = reader.GetString("Code"),
                            CareerName = reader.GetString("CareerName"),
                            DegreeName = reader.GetString("DegreeName")
                        };
                    if (!reader.IsDBNull("Fk_User"))
                    {
                        studentFound.User = new User
                        {
                            ID = reader.GetInt32("Pk_User"),
                            UserID = reader.GetString("ID"),
                            Password = reader.GetString("Password"),
                            UserType = new UserType
                            {
                                ID = reader.GetInt32("Pk_UserType"),
                                TypeDescription = reader.GetString("TypeDescription")
                            }
                        };
                    }
                    studentFound.ID = reader.GetInt32("Pk_Student");
                    studentFound.IdStudent = reader.GetString("ID");
                    studentFound.Name = reader.GetString("Name");
                    studentFound.PhoneNumber = reader.GetString("PhoneNumber");
                    studentFound.Email = reader.GetString("Email");
                    studentFound.DateOfBirth = reader.GetDateTime("DateOfBirth");
                    response = studentFound;
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

        public async Task<Student> FindStudentByUser(int id)
        {
            try
            {
                Student response = null;

                Connect();

                command = new SqlCommand(listStudents, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Fk_User", id));

                var reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    var studentFound = new Student();
                    if (!reader.IsDBNull("Fk_Carreer"))
                        studentFound.Career = new Career
                        {
                            ID = reader.GetInt32("Pk_Career"),
                            Code = reader.GetString("Code"),
                            CareerName = reader.GetString("CareerName"),
                            DegreeName = reader.GetString("DegreeName")
                        };
                    if (!reader.IsDBNull("Fk_User"))
                    {
                        studentFound.User = new User
                        {
                            ID = reader.GetInt32("Pk_User"),
                            UserID = reader.GetString("ID"),
                            Password = reader.GetString("Password"),
                            UserType = new UserType
                            {
                                ID = reader.GetInt32("Pk_UserType"),
                                TypeDescription = reader.GetString("TypeDescription")
                            }
                        };
                    }
                    studentFound.ID = reader.GetInt32("Pk_Student");
                    studentFound.IdStudent = reader.GetString("ID");
                    studentFound.Name = reader.GetString("Name");
                    studentFound.PhoneNumber = reader.GetString("PhoneNumber");
                    studentFound.Email = reader.GetString("Email");
                    studentFound.DateOfBirth = reader.GetDateTime("DateOfBirth");
                    response = studentFound;
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

        public async Task<List<Cycle>> FindStudentCycles(Student student)
        {
            List<Cycle> response = new List<Cycle>();
            try
            {
                Connect();

                command = new SqlCommand(findStudentCycles, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Student", student.ID));

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    response.Add(new Cycle
                    {
                        CycleState = new CycleState
                        {
                            ID = reader.GetInt32("Pk_CycleState"),
                            StateDescription = reader.GetString("StateDescription")
                        },
                        ID = reader.GetInt32("Pk_Cycle"),
                        Year = reader.GetInt32("Year"),
                        Number = reader.GetInt32("Number"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
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
        public async Task<List<GroupStudents>> FindStudentAcademicHistory(int studentID, int? cycleID)
        {
            List<GroupStudents> response = new List<GroupStudents>();
            try
            {
                Connect();

                command = new SqlCommand(findStudentAcademicHistory, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Pk_Student", studentID));
                command.Parameters.Add(new SqlParameter("@Pk_Cycle", cycleID));

                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Teacher teacher = new Teacher
                    {
                        ID = reader.GetInt32("Pk_Teacher"),
                        IdIdentidad = reader.GetString("ID"),
                        Name = reader.GetString("Name"),
                        PhoneNumber = reader.GetString("PhoneNumber"),
                        Email = reader.GetString("Email")
                    };
                    Cycle cycle = new Cycle
                    {
                        ID = reader.GetInt32("Pk_Cycle"),
                        Year = reader.GetInt32("Year"),
                        Number = reader.GetInt32("CycleNumber"),
                        StartDate = reader.GetDateTime("StartDate"),
                        EndDate = reader.GetDateTime("EndDate"),
                    };
                    Course course = new Course
                    {
                        ID = reader.GetInt32("Pk_Course"),
                        Code = reader.GetString("Code"),
                        Name = reader.GetString("CourseName"),
                        Credits = reader.GetInt32("Credits"),
                        WeeklyHours = reader.GetInt32("WeeklyHours")
                    };
                    Group group = new Group
                    {
                        Number = reader.GetString("Number"),
                        Schedule = reader.GetString("Schedule"),
                        ID = reader.GetInt32("Pk_Group"),
                        Course = course,
                        Cycle = cycle,
                        Teacher = teacher
                    };
                    GroupStudents groupStudent = new GroupStudents
                    {
                        ID = reader.GetInt32("Pk_GroupStudents"),
                        Score = !reader.IsDBNull("Score") ? Convert.ToDouble(reader.GetDecimal("Score")) : null,
                        Group = group
                    };
                    response.Add(groupStudent);
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
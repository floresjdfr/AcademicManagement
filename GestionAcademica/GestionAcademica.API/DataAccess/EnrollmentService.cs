using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.DataAccess
{
    public class EnrollmentService : Service
    {
        private readonly string findAvaiableGroupsToEnroll = "udpFindAvaibleGroupsToEnroll";
        private readonly string udpFindStudentCourseGroupsOfCycleState = "udpFindStudentCourseGroups";
        private SqlCommand sqlCommand = null;

        public EnrollmentService() { }

        public async Task<List<CourseGroups>> FindAvailableGroupsToEnroll(int studentID)
        {
            List<CourseGroups> response = new List<CourseGroups>();

            Connect();

            sqlCommand = new SqlCommand(findAvaiableGroupsToEnroll, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.Add(new SqlParameter("@studentID", studentID));

            var reader = await sqlCommand.ExecuteReaderAsync();

            while (reader.Read())
            {
                response.Add(CourseGroupsService.ReadCourseGroupsListFromDB(reader, course: true, group: true, teacher: true));
            }
            Disconnect();
            return response;
        }

        public async Task<List<CourseGroups>> FindStudentGroupsByCycleState(int studentID, int cycleState = 1)
        {
            List<CourseGroups> response = new List<CourseGroups>();

            Connect();

            sqlCommand = new SqlCommand(udpFindStudentCourseGroupsOfCycleState, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.Add(new SqlParameter("@Fk_Student", studentID));
            sqlCommand.Parameters.Add(new SqlParameter("@Fk_CycleState", cycleState));

            var reader = await sqlCommand.ExecuteReaderAsync();

            while (reader.Read())
            {
                response.Add(CourseGroupsService.ReadCourseGroupsListFromDB(reader, course: true, group: true, teacher: true));
            }

            Disconnect();
            return response;
        }
    }
}

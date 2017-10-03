using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class ProjectSqlDAL
    {
        private string connectionString;
        private const string getProjectSql = "select * from project";
        private const string assignEmployeeToProjectSql = @"insert into project_employee  values (@project_id, @employee_id);";
        private const string removeEmployeeFromProjectSql = @"delete project_employee WHERE project_id = @project_id AND employee_id = @employee_id";
        private const string createProjectSql = @"INSERT INTO project VALUES (@name, @from_date, @to_date)";



        // Single Parameter Constructor
        public ProjectSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> project = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(getProjectSql, conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        project.Add(CreateProjectFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return project;
        }

        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(assignEmployeeToProjectSql, conn);
                    command.Parameters.AddWithValue( "@project_id", projectId);
                    command.Parameters.AddWithValue("@employee_id", employeeId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(removeEmployeeFromProjectSql, conn);
                    command.Parameters.AddWithValue("@project_id", projectId);
                    command.Parameters.AddWithValue("@employee_id", employeeId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool CreateProject(Project newProject)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(createProjectSql, conn);
                    command.Parameters.AddWithValue("@name", newProject.Name);
                    command.Parameters.AddWithValue("@from_date", newProject.StartDate);
                    command.Parameters.AddWithValue("@to_date", newProject.EndDate);
                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected > 0);

                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        private Project CreateProjectFromRow(SqlDataReader results)
        {
            Project proj = new Project();
            proj.ProjectId = Convert.ToInt32(results["project_id"]);
            proj.Name = Convert.ToString(results["name"]);
            proj.StartDate  = Convert.ToDateTime(results["from_date"]);
            proj.EndDate = Convert.ToDateTime(results["to_date"]);

            return proj;
        }
    }
}

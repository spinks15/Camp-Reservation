using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class EmployeeSqlDAL
    {
        private string connectionString;
        private const string getEmployeeSql = "select * from employee";
        private const string searchEmployeeSql = @"SELECT * from employee WHERE first_name LIKE @firstname AND last_name LIKE @lastname";
        private const string projectEmployeeSql = "SELECT * from employee LEFT JOIN project_employee ON employee.employee_id = project_employee.employee_id WHERE project_id IS NULL";
        

        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(getEmployeeSql, conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        employees.Add(CreateEmployeeFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return employees;
        }

        public List<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(searchEmployeeSql, conn);
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@lastname", lastname);

                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        employees.Add(CreateEmployeeFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return employees;
        }

        public List<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(projectEmployeeSql, conn);
                    SqlDataReader results = command.ExecuteReader();
                 
                    while (results.Read())
                    {
                        employees.Add(CreateEmployeeFromRow(results));
                    }
                  
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return employees;
        }

        private Employee CreateEmployeeFromRow(SqlDataReader results)
        {
            Employee emp = new Employee();
            emp.EmployeeId = Convert.ToInt32(results["employee_id"]);
            emp.DepartmentId = Convert.ToInt32(results["department_id"]);
            emp.JobTitle = Convert.ToString(results["job_title"]);
            emp.FirstName = Convert.ToString(results["first_name"]);
            emp.LastName = Convert.ToString(results["last_name"]);
            emp.BirthDate = Convert.ToDateTime(results["birth_date"]);
            emp.Gender = Convert.ToString(results["gender"]);
            emp.HireDate = Convert.ToDateTime(results["hire_date"]);
                 
            return emp;
        }
    }
}

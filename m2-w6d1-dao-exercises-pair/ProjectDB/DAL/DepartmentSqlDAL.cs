using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class DepartmentSqlDAL
    {

        private const string getDepartmentsSql = "select * from department";
        private string connectionString;

        // Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(getDepartmentsSql, conn);
                    SqlDataReader results = command.ExecuteReader();

                    while(results.Read())
                    {
                        departments.Add(CreateDepartmentFromRow(results));
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return departments;
        }

        public bool CreateDepartment(Department newDepartment)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDepartment(Department updatedDepartment)
        {
            throw new NotImplementedException();
        }

        private Department CreateDepartmentFromRow(SqlDataReader results)
        {
            Department dept = new Department();
            dept.Id = Convert.ToInt32(results["department_id"]);
            dept.Name = Convert.ToString(results["name"]);
            return dept;
        }

    }
}

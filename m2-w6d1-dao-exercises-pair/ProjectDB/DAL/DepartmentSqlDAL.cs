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
<<<<<<< HEAD
        private const string getDepartmentsSql = "select * from department";
        private const string createDepartmentSql = @"insert into department VALUES (@name);";
        private const string updateDepartmentSql = @"update department set name=@newname where (department_id=@olddeptnumber);";

        
=======

        private const string getDepartmentsSql = "select * from department";
>>>>>>> f352e0e3bc069be361bcacae1573423e0ce7ebdb
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
<<<<<<< HEAD
                    SqlCommand command = new SqlCommand(getDepartmentsSql, conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        departments.Add(CreateDepartmentFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
=======

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

>>>>>>> f352e0e3bc069be361bcacae1573423e0ce7ebdb
            return departments;
        }

        public bool CreateDepartment(Department newDepartment)
        {

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(createDepartmentSql, conn);
                    command.Parameters.AddWithValue("@name", newDepartment.Name);

                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected > 0);

                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool UpdateDepartment(Department updatedDepartment)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(updateDepartmentSql, conn);
                    command.Parameters.AddWithValue("@newname", updatedDepartment.Name);
                    command.Parameters.AddWithValue("@olddeptnumber", updatedDepartment.Id );
                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected > 0);

                }
            }
            catch (SqlException)
            {
              throw;
            }
        }

        private Department CreateDepartmentFromRow(SqlDataReader results)
        {
            Department dept = new Department();
            dept.Id = Convert.ToInt32(results["department_id"]);
            dept.Name = Convert.ToString(results["name"]);
            return dept;
        }
<<<<<<< HEAD
=======

>>>>>>> f352e0e3bc069be361bcacae1573423e0ce7ebdb
    }
}

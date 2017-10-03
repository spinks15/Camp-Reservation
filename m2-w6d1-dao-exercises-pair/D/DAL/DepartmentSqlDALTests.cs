using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ProjectDB.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;


namespace ProjectDB.DAL.Tests
{
    [TestClass]
    public class DepartmentSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = projects; Integrated Security = True";
        private int deptId;


        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("INSERT INTO Department VALUES ('Test Department'); SELECT CAST (SCOPE_IDENTITY() as int);", conn);
                deptId = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetDepartmentTests()
        {
            DepartmentSqlDAL deptDal = new DepartmentSqlDAL(connectionString);
            List<Department> dept = deptDal.GetDepartments();
            Assert.AreEqual(5, dept.Count);
            Assert.AreEqual(deptId, dept[4].Id);
        }

        [TestMethod]
        public void CreateDepartmentTests()
        {
            DepartmentSqlDAL deptDal = new DepartmentSqlDAL(connectionString);
            Department dept = new Department();
            dept.Id = 6;
            dept.Name = "Logistics";
            bool createWorked = deptDal.CreateDepartment(dept);
            Assert.AreEqual(true, createWorked);
        }
        [TestMethod]
        public void UpdateDepartmentTests()
        {
            DepartmentSqlDAL deptDal = new DepartmentSqlDAL(connectionString);
            Department dept = new Department();
            dept.Id = 4;
            dept.Name = "Logistics";
            bool updateWorked = deptDal.UpdateDepartment(dept);
            Assert.AreEqual(true, updateWorked);
        }
    }
}

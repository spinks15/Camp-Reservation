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
    public class EmployeeSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = projects; Integrated Security = True";
        private int empId;


        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("INSERT INTO employee VALUES (1, 'firstname', 'lastname', 'title', '2000-08-12', 'M', '2013-02-10'); SELECT CAST (SCOPE_IDENTITY() as int);", conn);
                empId = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllEmployeeTests()
        {
            EmployeeSqlDAL empDal = new EmployeeSqlDAL(connectionString);
            List<Employee> emp = empDal.GetAllEmployees();
            Assert.AreEqual(13, emp.Count);
            Assert.AreEqual("firstname", emp[12].FirstName);
           
        }
        [TestMethod]
        public void SearchTest()
        {
            EmployeeSqlDAL empDal = new EmployeeSqlDAL(connectionString);
            List<Employee> emp = empDal.Search("Chris","Christie");
            Assert.AreEqual(1, emp.Count);
            Assert.AreEqual("Chris", emp[0].FirstName);

        }
        [TestMethod]
        public void GetAllEmployeeWithoutProjectsTests()
        {
            EmployeeSqlDAL empDal = new EmployeeSqlDAL(connectionString);
            List<Employee> emp = empDal.GetAllEmployees();
            Assert.AreEqual("firstname", emp[emp.Count-1].FirstName);
            Assert.AreEqual("lastname", emp[emp.Count - 1].LastName);

        }
    }
}

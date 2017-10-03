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
    public class ProjectSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = projects; Integrated Security = True";
        private int projId;


        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("INSERT INTO project VALUES ('projectname', '2000-08-12', '2013-02-10'); SELECT CAST (SCOPE_IDENTITY() as int);", conn);
                projId = (int)cmd.ExecuteScalar();
                //cmd = new SqlCommand(@"INSERT INTO project_employee VALUES (@projId, 1);", conn);
                //cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void CreatProjects()
        {
            ProjectSqlDAL projDal = new ProjectSqlDAL(connectionString);
            Project proj = new Project();
            proj.StartDate = new DateTime(2000, 10, 12);
            proj.EndDate = new DateTime(2001, 11, 15);
            proj.Name = "Logistics";
            bool createWorked = projDal.CreateProject(proj);
            Assert.AreEqual(true, createWorked);
        }

        [TestMethod]
        public void GetAllProjects()
        {
            ProjectSqlDAL projDal = new ProjectSqlDAL(connectionString);
            List<Project> proj = projDal.GetAllProjects();
            Assert.AreEqual(7, proj.Count);
            Assert.AreEqual("projectname", proj[6].Name);
        }
        [TestMethod]
        public void AssignProjects()
        {
            ProjectSqlDAL projDal = new ProjectSqlDAL(connectionString);
            bool proj = projDal.AssignEmployeeToProject(5, 3);
            Assert.AreEqual(true, proj);
           
        }
        [TestMethod]
        public void RemoveProjects()
        {
            ProjectSqlDAL projDal = new ProjectSqlDAL(connectionString);
            bool proj = projDal.RemoveEmployeeFromProject(1, 3);
            Assert.AreEqual(true, proj);
        }
    }
}

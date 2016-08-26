using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorDemo.Utilities;
using VisitorDemo.Models;
using VisitorDemo.DataLayer;
namespace VisitorDemo.Tests
{
    /// <summary>
    /// Summary description for TestPagedList
    /// </summary>
    [TestClass]
    public class TestPagedList
    {
        public TestPagedList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestNoofPagesInTheListOfStrings()
        {

            List<string> someStringList = new List<string> { "Tom", "Dick", "Harry", "Melanie", "Tracey", "Jack", "Alan", "George" };
         
            var pagedStringList = new PagedList<string>(someStringList,5);
            Assert.AreEqual(pagedStringList.NoPages,2);
            var page2List = pagedStringList.GetListFromPage(2);
            Assert.AreEqual(page2List.Count,3);

        }

        [TestMethod]
        public void TestNoofPagesInTheListOfCustomers()
        {

            BONorthWind bnw = new BONorthWind();
            var customers = bnw.GetCustomers();
            int pageSize = 10;
            int noOfPagesExpected = customers.Count / pageSize+1;
            var customerPaged = new PagedList<Customer>(customers, pageSize);
            Assert.AreEqual(noOfPagesExpected, customerPaged.NoPages);
            List<Customer> customeratPageN = customerPaged.GetListFromPage(customerPaged.NoPages);
            Assert.AreEqual(customeratPageN.Count, (customers.Count - (customerPaged.NoPages - 1) * pageSize));
        }

    }
}

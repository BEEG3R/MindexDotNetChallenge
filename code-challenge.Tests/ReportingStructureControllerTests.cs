using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace code_challenge.Tests.Integration {
    [TestClass]
    public class ReportingStructureControllerTests {
        private static HttpClient httpClient;
        private static TestServer testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context) {
            testServer = new TestServer(WebHost.CreateDefaultBuilder()
                                               .UseStartup<TestServerStartup>()
                                               .UseEnvironment("Development"));
            httpClient = testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest() {
            httpClient.Dispose();
            testServer.Dispose();
        }

        /// <summary>
        /// Test that covers the case where the employee to query the report
        /// structure for is the root of the tree structure.
        /// </summary>
        [TestMethod]
        public void GetReportStructure_Returns_Expected_Value_John_Lennon() {
            // Setup - expected direct reports and John Lennon EmployeeId
            const int expected = 4;
            const string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Act
            Task<HttpResponseMessage> getRequest = httpClient.GetAsync($"api/reporting/{employeeId}");

            HttpResponseMessage response = getRequest.Result;

            // Assert - verify we got status code 200 (expected)
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ReportingStructure report = response.DeserializeContent<ReportingStructure>();
            int actual = report.NumberOfReports;
            // verify our actual report count matches the expected value.
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test that covers the average case. The employee to query the report
        /// structure for reports to an employee above them, but also has direct
        /// reports of their own.
        /// </summary>
        [TestMethod]
        public void GetReportStructure_Returns_Expected_Value_Ringo_Starr()
        {
            // Setup - expected direct reports and Ringo Starr EmployeeId
            const int expected = 2;
            const string employeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f";

            // Act
            Task<HttpResponseMessage> getRequest = httpClient.GetAsync($"api/reporting/{employeeId}");

            HttpResponseMessage response = getRequest.Result;

            // Assert - verify we got status code 200 (expected)
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ReportingStructure report = response.DeserializeContent<ReportingStructure>();
            int actual = report.NumberOfReports;
            // verify our actual report count matches the expected value.
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test that covers the case where the employee to query the report
        /// structure for has no direct reports. (This employee would be considered
        /// a leaf node in the tree structure)
        /// </summary>
        [TestMethod]
        public void GetReportStructure_Returns_Expected_Value_Paul_McCartney()
        {
            // Setup - expected direct reports and Paul McCartney EmployeeId
            const int expected = 0;
            const string employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";

            Task<HttpResponseMessage> getRequest = httpClient.GetAsync($"api/reporting/{employeeId}");

            HttpResponseMessage response = getRequest.Result;

            // Assert - verify we got status code 200 (expected)
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ReportingStructure report = response.DeserializeContent<ReportingStructure>();
            int actual = report.NumberOfReports;
            // verify our actual report count matches the expected value.
            Assert.AreEqual(expected, actual);
        }
    }
}

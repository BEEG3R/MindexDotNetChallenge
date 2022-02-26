using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace code_challenge.Tests.Integration {
    [TestClass]
    public class CompensationControllerTests {
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
        /// Test that covers the case when someone passes in an invalid id.
        /// </summary>
        [TestMethod]
        public void GetEmployeeCompensation_Returns_Not_Found_With_Invalid_Id() {
            // Setup
            const HttpStatusCode expected = HttpStatusCode.NotFound;
            const string employeeId = "Invalid ID";

            // Act
            Task<HttpResponseMessage> getRequest = httpClient.GetAsync($"api/compensation/{employeeId}");

            HttpResponseMessage response = getRequest.Result;

            // Assert - verify we got status code 404 (expected) 
            Assert.AreEqual(expected, response.StatusCode);
        }

        /// <summary>
        /// Test that covers the "happy path" - when a compensation record exists AND
        /// the consumer of the API provides the correct EmployeeId to find that
        /// existing compensation record.
        /// </summary>
        [TestMethod]
        public void GetEmployeeCompensation_Returns_Object_When_Found() {
            // Setup - need to create a compensation record for an existing employee before querying for it.
            const HttpStatusCode expected = HttpStatusCode.OK;
            const string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            // Call our "happy path" test case to create a compensation record
            CreateEmployeeCompensation_Returns_Object_Upon_Success();

            // Act
            Task<HttpResponseMessage> getRequest = httpClient.GetAsync($"api/compensation/{employeeId}");

            HttpResponseMessage response = getRequest.Result;

            // Assert - verify we got status code 200 (expected) 
            Assert.AreEqual(expected, response.StatusCode);
            Compensation compensation = response.DeserializeContent<Compensation>();
            // Verify the deserialized object is the one we expected.
            Assert.AreEqual(employeeId, compensation.Employee.EmployeeId);
        }

        /// <summary>
        /// Test that covers the case when a consumer of the API is trying to create
        /// a new compensation record but doesn't pass any data in via the request body.
        /// </summary>
        [TestMethod]
        public void CreateEmployeeCompensation_Returns_Bad_Request_Without_Compensation() {
            // Setup
            const HttpStatusCode expected = HttpStatusCode.BadRequest;

            // Act
            Task<HttpResponseMessage> postRequest = httpClient.PostAsync("api/compensation",
                                                                         new StringContent(
                                                                             string.Empty, Encoding.UTF8,
                                                                             "application/json"));
            HttpResponseMessage response = postRequest.Result;

            // Assert
            Assert.AreEqual(expected, response.StatusCode);
        }

        /// <summary>
        /// Test that covers the case when a consumer of the API is trying to create
        /// a new compensation record and provides all of the necessary information.
        /// </summary>
        [TestMethod]
        public void CreateEmployeeCompensation_Returns_Object_Upon_Success() {
            // Setup
            const HttpStatusCode expected = HttpStatusCode.Created;
            Employee johnLennon = new Employee {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                FirstName = "John",
                LastName = "Lennon",
                Position = "Development Manager",
                Department = "Engineering"
            };
            Compensation compensation = new Compensation {
                Employee = johnLennon, 
                Salary = 1000000, 
                EffectiveDate = DateTime.Now
            };
            string json = JsonConvert.SerializeObject(compensation);

            // Act
            Task<HttpResponseMessage> postRequest = httpClient.PostAsync("api/compensation",
                                                                         new StringContent(
                                                                             json,
                                                                             Encoding.UTF8, "application/json"));
            HttpResponseMessage response = postRequest.Result;

            // Assert
            Assert.AreEqual(expected, response.StatusCode);
            Compensation deserialized = response.DeserializeContent<Compensation>();
            // Verify the deserialized object matches the one we passed in through the POST body.
            Assert.AreEqual(compensation.Salary, deserialized.Salary);
            Assert.AreEqual(compensation.Employee.EmployeeId, deserialized.Employee.EmployeeId);
        }
    }
}

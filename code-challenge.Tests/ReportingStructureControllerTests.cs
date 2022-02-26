﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace code_challenge.Tests.Integration
{
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

        public ReportingStructureControllerTests() {

        }

        [TestMethod]
        public void GetReportStructure_Returns_Expected_Value() {

        }
    }
}
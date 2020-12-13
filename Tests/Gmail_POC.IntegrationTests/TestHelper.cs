using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Gmail_POC.IntegrationTests
{

    public static class TestHelper
    {
        public static Uri GetTestBaseUrl()
        {
            var iConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build(); 

            string baseUrl = iConfig.GetValue<string>("baseApiUrl");

            return new Uri(baseUrl);
        }
    }
}

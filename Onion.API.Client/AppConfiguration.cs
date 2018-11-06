using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Onion.API.Client
{
    public class AppConfiguration
    {

        private readonly string _webApiUrl;
        public string WebApiUrl { get => _webApiUrl; }

        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            var appSetting = root.GetSection("ApplicationSettings:WebApiUrl");
            _webApiUrl = appSetting.Value;
        }

        
    }
}

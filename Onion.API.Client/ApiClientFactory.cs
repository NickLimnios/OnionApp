using Onion.API.Client;
using System;
using System.Threading;

namespace Onion.API.Factory
{
    public static class ApiClientFactory
    {
        private static Uri apiUri;

        private static Lazy<ApiClient> restClient = new Lazy<ApiClient>(
          () => new ApiClient(apiUri),
          LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            AppConfiguration appConfig = new AppConfiguration();
            apiUri = new Uri(appConfig.WebApiUrl);
        }

        public static ApiClient Instance
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}

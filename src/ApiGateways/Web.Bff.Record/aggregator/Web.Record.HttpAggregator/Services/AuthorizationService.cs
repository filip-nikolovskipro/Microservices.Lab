using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Record.HttpAggregator.Config;

namespace Web.Record.HttpAggregator.Services
{
	public class AuthorizationService : IAuthorizationService
	{
		private readonly HttpClient _apiClient;
		private readonly ILogger<AuthorizationService> _logger;
		private readonly UrlsConfig _urls;

		public AuthorizationService(HttpClient apiClient, ILogger<AuthorizationService> logger, IOptions<UrlsConfig> urls)
		{
			_apiClient = apiClient;
			_logger = logger;
			_urls = urls.Value;
		}

		public async Task<IEnumerable<int>> GvetAllowedIds()
		{
			var data = await _apiClient.GetStringAsync(_urls.Authorization + UrlsConfig.AuthorizationOperations.GetAllowedIds());
			var allowedIds = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<IEnumerable<int>>(data) : null;

			return allowedIds;
		}
	}
}

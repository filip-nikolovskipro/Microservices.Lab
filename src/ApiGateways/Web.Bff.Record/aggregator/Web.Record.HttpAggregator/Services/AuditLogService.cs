using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Record.HttpAggregator.Config;
using Web.Record.HttpAggregator.Models;

namespace Web.Record.HttpAggregator.Services
{
	public class AuditLogService : IAuditLogService
	{
		private readonly HttpClient _apiClient;
		private readonly ILogger<AuditLogService> _logger;
		private readonly UrlsConfig _urls;

		public AuditLogService(HttpClient apiClient, ILogger<AuditLogService> logger, IOptions<UrlsConfig> urls)
		{
			_apiClient = apiClient;
			_logger = logger;
			_urls = urls.Value;
		}

		public async Task<IEnumerable<Log>> GetAuditLogs(IEnumerable<int> userIds)
		{
			var data = await _apiClient.GetStringAsync(_urls.AuditLog + UrlsConfig.AuditLogOperations.GetAuditLogs(userIds));
			var logs = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<IEnumerable<Log>>(data) : null;

			return logs;
		}
	}
}

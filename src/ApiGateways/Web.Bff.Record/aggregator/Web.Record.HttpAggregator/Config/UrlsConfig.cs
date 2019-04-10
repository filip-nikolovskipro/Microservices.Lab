using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Record.HttpAggregator.Config
{
	public class UrlsConfig
	{
		public class AuthorizationOperations
		{
			public static string GetAllowedIds() => $"/api/UserAccess/GetAllowedIds";
		}

		public class AuditLogOperations
		{
			public static string GetAuditLogs(IEnumerable<int> userIds) => $"/api/AuditLog/GetAuditLogs?userIds={string.Join(",", userIds)}";
		}

		public string Authorization { get; set; }
		public string AuditLog { get; set; }
	}
}

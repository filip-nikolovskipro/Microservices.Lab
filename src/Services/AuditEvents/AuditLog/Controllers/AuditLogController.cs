using AuditLog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuditLog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuditLogController : Controller
	{

		List<Log> logs = new List<Log>()
			{
				new Log { Id = 1, UserId = 1, TenantId = 1, Message = "Log Message 1" },
				new Log { Id = 2, UserId = 1, TenantId = 1, Message = "Log Message 2" },
				new Log { Id = 3, UserId = 1, TenantId = 1, Message = "Log Message 3" },
				new Log { Id = 4, UserId = 1, TenantId = 1, Message = "Log Message 4" },
				new Log { Id = 5, UserId = 1, TenantId = 1, Message = "Log Message 5" },
				new Log { Id = 6, UserId = 1, TenantId = 1, Message = "Log Message 6" },
				new Log { Id = 7, UserId = 1, TenantId = 1, Message = "Log Message 7" },
				new Log { Id = 8, UserId = 2, TenantId = 1, Message = "Log Message 8" },
				new Log { Id = 9, UserId = 2, TenantId = 1, Message = "Log Message 9" },
				new Log { Id = 10, UserId = 3, TenantId = 1, Message = "Log Message 10" },
				new Log { Id = 11, UserId = 4, TenantId = 1, Message = "Log Message 11" },
				new Log { Id = 12, UserId = 5, TenantId = 1, Message = "Log Message 12" },
				new Log { Id = 13, UserId = 6, TenantId = 1, Message = "Log Message 13" },
				new Log { Id = 14, UserId = 7, TenantId = 1, Message = "Log Message 14" },
				new Log { Id = 15, UserId = 8, TenantId = 1, Message = "Log Message 15" },
				new Log { Id = 16, UserId = 9, TenantId = 1, Message = "Log Message 16" },
				new Log { Id = 17, UserId = 11, TenantId = 1, Message = "Log Message 17" },
				new Log { Id = 18, UserId = 12, TenantId = 1, Message = "Log Message 18" },
				new Log { Id = 19, UserId = 10, TenantId = 2, Message = "Log Message 19" },
				new Log { Id = 20, UserId = 10, TenantId = 2, Message = "Log Message 20" },
				new Log { Id = 21, UserId = 10, TenantId = 2, Message = "Log Message 21" },
				new Log { Id = 22, UserId = 10, TenantId = 2, Message = "Log Message 22" }
			};

		// GET api/AuditLog/GetAuditLogs?userIds=1,2
		[HttpGet]
		[Route("GetAuditLogs")]
		public IEnumerable<Log> GetAuditLogs(string userIds)
		{
			var separated = userIds.Split(new char[] { ',' });
			List<int> parsed = separated.Select(s => int.Parse(s)).ToList();

			return logs.Where(x => parsed.Contains(x.UserId));
		}

		// GET api/AuditLog/GetAuditLogsForAdmin?tenantId=1
		[HttpGet]
		[Route("GetAuditLogsForAdmin")]
		public IEnumerable<Log> GetAuditLogsForAdmin(int tenantId)
		{
			return logs.Where(x => x.TenantId == tenantId);
		}

	}
}
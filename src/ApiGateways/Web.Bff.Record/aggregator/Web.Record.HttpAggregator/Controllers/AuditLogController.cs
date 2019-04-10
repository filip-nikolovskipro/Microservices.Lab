using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Record.HttpAggregator.Models;
using Web.Record.HttpAggregator.Services;

namespace Web.Record.HttpAggregator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuditLogController : Controller
    {
		private readonly IAuditLogService _auditLogService;
		private readonly IAuthorizationService _authorizationService;

		public AuditLogController(IAuditLogService auditLogService, IAuthorizationService authorizationService)
		{
			_auditLogService = auditLogService;
			_authorizationService = authorizationService;
		}


		// GET api/AuditLog/GetAuditLogs
		[HttpGet]
		[Route("GetAuditLogs")]
		public async Task<IEnumerable<Log>> GetAuditLogs()
		{
			var allowedUserIds = await _authorizationService.GvetAllowedIds();
			var logs =await _auditLogService.GetAuditLogs(allowedUserIds);

			return logs;
		}
	}
}
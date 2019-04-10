using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Record.HttpAggregator.Models;

namespace Web.Record.HttpAggregator.Services
{
	public interface IAuditLogService
	{
		Task<IEnumerable<Log>> GetAuditLogs(IEnumerable<int> userIds);
	}
}

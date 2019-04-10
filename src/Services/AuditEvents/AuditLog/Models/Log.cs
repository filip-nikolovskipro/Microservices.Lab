using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditLog.Models
{
	public class Log
	{
		public int Id { get; set; }

		public int TenantId { get; set; }

		public int UserId { get; set; }

		public string Username { get; set; }

		public string Message { get; set; }
	}
}

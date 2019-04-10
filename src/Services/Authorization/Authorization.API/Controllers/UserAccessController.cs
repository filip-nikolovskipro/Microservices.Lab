using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserAccessController : Controller
    {
		[HttpGet]
		[Route("GetAllowedIds")]
		public ActionResult<IEnumerable<int>> GetAllowedIds()
        {
			var allowedIds = new List<int> { 1, 2, 3, 4, 5 };

			return allowedIds;
        }
    }
}
using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Calemas.Erp.Api.Controllers
{
	
    [Route("api/[controller]")]
    public class HealthController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return string.Format("is live at now {0}", DateTime.Now.ToTimeZone());
        }
    }
}

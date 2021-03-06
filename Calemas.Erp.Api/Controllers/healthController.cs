using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SmartSecretary.Api.Controllers
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

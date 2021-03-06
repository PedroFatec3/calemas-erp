using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Common.Domain.Model;
using Common.API;


namespace Calemas.Erp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CurrentUserController : Controller
    {

        private readonly CurrentUser _user;
		private readonly ILogger _logger;

        public CurrentUserController(CurrentUser user, ILoggerFactory logger)
        {
            this._user = user;
			this._logger = logger.CreateLogger<CurrentUserController>();
			this._logger.LogInformation("AccountController init success");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new HttpResult<dynamic>(this._logger);
            try
            {
                var claims = this._user.GetClaims();
                if (claims.IsAny())
                {
                    return result.ReturnCustomResponse(claims.Select(_ => new
                    {
                        ClamsType = _.Key,
                        Value = _.Value
                    }));
                }

                return result.ReturnCustomResponse(new { warning = "No Claims found!" });

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex, "Calemas.Erp - CurrentUser");
            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using ITERS_Tutorio_v1.Helper;
using ITERS_Tutorio_v1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITERS_Tutorio_v1.Controllers
{
    [Route("api/[controller]")]
    public class SigninController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        private readonly IStringLocalizer<SigninController> _localizer;
        private readonly IConfiguration _configuration;

        public SigninController(ITERSTutoriov10Context db, IStringLocalizer<SigninController> localizer, IConfiguration configuration)
        {
            _db = db;
            _localizer = localizer;
            _configuration = configuration;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ReturnKey Post([FromBody]Signin signin)
        {
            var rtnKey = new ReturnKey();

            if (signin?.UserName != null && signin.Password != null)
            {
                var salt = (from i in _db.TbSubscriptions
                            where string.Equals(i.Email, signin.UserName, StringComparison.CurrentCultureIgnoreCase)
                            select i.Salt).FirstOrDefault();

                var subscription = (from i in _db.TbSubscriptions
                                    where i.SubscriptionStatusId == 2 && i.Email == signin.UserName && i.Password == Security.EncryptPassword(signin.Password, salt)
                                    select i).FirstOrDefault();

                if (subscription != null)
                {
                    var token = (from i in _db.TbSubscriptionTokens
                                 where i.Email == subscription.Email && i.ExpiresOn > DateTime.Now
                                 select i).FirstOrDefault();
                    if (token == null)
                    {
                        double.TryParse(_configuration["Setting:AuthTokenExpiry"], out var authTokenExpiry);

                        token = new TbSubscriptionTokens
                        {
                            Email = subscription.Email,
                            AuthToken = Guid.NewGuid().ToString(),
                            IssuedOn = DateTime.Now,
                            ExpiresOn = DateTime.Now.AddSeconds(authTokenExpiry)
                        };

                        _db.TbSubscriptionTokens.Add(token);
                        _db.SaveChanges();
                    }

                    rtnKey.UID = subscription.UniqueId.ToString();
                    rtnKey.AuthToken = token.AuthToken;
                    rtnKey.Message = "success!!!";
                    rtnKey.Role = subscription.GroupId;
                }
                else {
                    rtnKey.Message = _localizer["NotValid"];
                }
            }
            else {
                rtnKey.Message = _localizer["NotValid"];
            }
            return rtnKey;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebUsers.Managers;
using WebUsers.Models;

namespace WebUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersManager _usersManager;

        public UsersController(UsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get([FromQuery(Name = "limit")] int limit = 50, [FromQuery(Name = "page")] int page = 1)
        {
            if (limit > 50)
                limit = 50;

            return _usersManager.GetUsers(limit, page);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<User> Get(string id)
        {

            var user = _usersManager.GetUser(id);
            if (user != null)
                return user;
            else
                return NotFound();
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] JObject value)
        {
            string uuid = value.Value<string>("uuid");
            string idValue = value.Value<string>("idValue");
            string gender = value.Value<string>("gender");
            string name = value.Value<string>("name");
            string email = value.Value<string>("email");
            DateTime birthDate = value.Value<DateTime>("birthDate");
            string userName = value.Value<string>("userName");

            string state = value.Value<JObject>("location").Value<string>("state");
            string street = value.Value<JObject>("location").Value<string>("street");
            string city = value.Value<JObject>("location").Value<string>("city");
            string postalCode = value.Value<JObject>("location").Value<string>("postCode");

            _usersManager.InsertUser(uuid, idValue, gender, name, email, birthDate, userName, state, street, city, postalCode);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] JObject value)
        {
            string gender = value.Value<string>("gender");
            string name = value.Value<string>("name");
            string email = value.Value<string>("email");
            DateTime birthDate = value.Value<DateTime>("birthDate");
            string userName = value.Value<string>("userName");
            string state = value.Value<JObject>("location").Value<string>("state");
            string street = value.Value<JObject>("location").Value<string>("street");
            string city = value.Value<JObject>("location").Value<string>("city");
            string postalCode = value.Value<JObject>("location").Value<string>("postCode");

            _usersManager.UpdateUser(id, gender, name, email, birthDate, userName, state, street, city, postalCode);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _usersManager.DeleteUser(id);
        }
    }
}

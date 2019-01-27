using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebUsers.Models;

namespace WebUsers.Services
{
    public class ExternalUserDataService
    {
        private readonly WebUserContext _context;

        public ExternalUserDataService(WebUserContext context)
        {
            _context = context;
        }

        public void AddTestData()
        {
            List<User> users = getListUsersAsync();
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        private List<User> getListUsersAsync()
        {

            string path = "https://randomuser.me/api/?results=500";
            HttpClient client = new HttpClient();
            List<User> users = new List<User>();

            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var jsonObj = JsonConvert.DeserializeObject<JObject>(jsonString);

                var results = jsonObj.GetValue("results").ToList();
                int count = 0;
                foreach (JToken result in results)
                {
                    string idValue = result.Value<JToken>("id").Value<string>("value");
                    string gender = result.Value<string>("gender");
                    string name = string.Format("{0} {1} {2}",
                        result.Value<JObject>("name").GetValue("title").ToString(),
                        result.Value<JObject>("name").GetValue("first").ToString(),
                        result.Value<JObject>("name").GetValue("last").ToString());
                    string email = result.Value<string>("email");
                    DateTime birthDate = result.Value<JToken>("dob").Value<DateTime>("date");
                    string uuid = result.Value<JObject>("login").GetValue("uuid").ToString();
                    string userName = result.Value<JObject>("login").GetValue("username").ToString();

                    string idLocation = Guid.NewGuid().ToString();
                    string state = result.Value<JObject>("location").GetValue("state").ToString();
                    string street = result.Value<JObject>("location").GetValue("street").ToString(); ;
                    string city = result.Value<JObject>("location").GetValue("city").ToString(); ;
                    string postCode = result.Value<JObject>("location").GetValue("postcode").ToString(); ;

                    var newLocation = new Location
                    {
                        Uuid = uuid,
                        State = state,
                        Street = street,
                        City = city,
                        PostCode = postCode
                    };

                    var newUser = new User
                    {
                        IdValue = idValue,
                        Gender = gender,
                        Name = name,
                        Email = email,
                        BirthDate = birthDate,
                        Uuid = uuid,
                        UserName = userName,
                        Location = newLocation
                    };

                    users.Add(newUser);

                    count++;
                }
            }

            return users;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebUsers.Managers;
using WebUsers.Models;
using WebUsers.Services;
using Xunit;

namespace WebUser_Test.Managers
{
    public class UsersManager_test
    {
        private readonly UsersManager userManager;
        private readonly ExternalUserDataService externalUserDataService;

        public UsersManager_test()
        {
            DbContextOptions<WebUserContext> options;
            var builder = new DbContextOptionsBuilder<WebUserContext>();
            builder.UseInMemoryDatabase(databaseName: "users");
            options = builder.Options;

            WebUserContext context = new WebUserContext(options);

            UserService userService = new UserService(context);

            userManager = new UsersManager(userService);

            externalUserDataService = new ExternalUserDataService(context);

            externalUserDataService.AddTestData();
        }

        [Fact]
        public void Test_SortUsers()
        {
            IEnumerable<User> users = userManager.GetUsers(500, 1);

            long beforUser = long.MinValue;
            using (var seqUsers = users.GetEnumerator())
            {
                while (seqUsers.MoveNext())
                {
                    long currentDate = ((DateTimeOffset)seqUsers.Current.BirthDate).ToUnixTimeSeconds();
                    Assert.True(currentDate > beforUser, "Sort error test");
                    beforUser = currentDate;
                }
            }
        }

    }
}

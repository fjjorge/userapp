using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUsers.Models;

namespace WebUsers.Services
{
    public class UserService
    {
        private readonly WebUserContext _context;

        public UserService(WebUserContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {

            var users = (from u in _context.Users
                         join loc in _context.Locations on u.Uuid equals loc.Uuid
                         select new User
                         {
                             IdValue = u.IdValue,
                             Gender = u.Gender,
                             Name = u.Name,
                             Email = u.Email,
                             BirthDate = u.BirthDate,
                             Uuid = u.Uuid,
                             UserName = u.UserName,
                             Location = loc
                         })
                         .ToList();

            return users;
        }

        public User GetUser(string IdValue)
        {
            var user = (from u in _context.Users
                        join loc in _context.Locations on u.Uuid equals loc.Uuid
                        where u.IdValue == IdValue
                        select new User
                        {
                            IdValue = u.IdValue,
                            Gender = u.Gender,
                            Name = u.Name,
                            Email = u.Email,
                            BirthDate = u.BirthDate,
                            Uuid = u.Uuid,
                            UserName = u.UserName,
                            Location = loc
                        }).FirstOrDefault();

            return user;
        }

        public User InsertUser(string Uuid, string idValue, string gender, string name, string email, DateTime birthDate, string userName, string state, string street, string city, string postalCode)
        {
            var userInsert = new User();

            userInsert.Uuid = Uuid;
            userInsert.IdValue = idValue;
            userInsert.Gender = gender;
            userInsert.Name = name;
            userInsert.Email = email;
            userInsert.BirthDate = birthDate;
            userInsert.UserName = userName;


            var locationInsert = new Location();

            locationInsert.Uuid = Uuid;
            locationInsert.State = state;
            locationInsert.Street = street;
            locationInsert.City = city;
            locationInsert.PostCode = postalCode;

            _context.Users.Add(userInsert);
            _context.Locations.Add(locationInsert);

            _context.SaveChanges();

            return GetUser(idValue);

        }

        public User UpdateUser(string idValue, string gender, string name, string email, DateTime birthDate, string userName, string state, string street, string city, string postalCode)
        {
            var userUpdate = (from u in _context.Users
                              where u.IdValue == idValue
                              select u).FirstOrDefault();

            if(userUpdate != null)
            {
                userUpdate.Gender = gender;
                userUpdate.Name = name;
                userUpdate.Email = email;
                userUpdate.BirthDate = birthDate;
                userUpdate.UserName = userName;


                var locationUpdate = (from loc in _context.Locations
                                      where loc.Uuid == userUpdate.Uuid
                                      select loc).FirstOrDefault();

                locationUpdate.State = state;
                locationUpdate.Street = street;
                locationUpdate.City = city;
                locationUpdate.PostCode = postalCode;

                _context.SaveChanges();
            }

            return GetUser(idValue);
        }

        public void DeleteUser(string id)
        {
            var userRemove = (from u in _context.Users
                              where u.IdValue == id
                              select u).FirstOrDefault();
            if(userRemove != null)
            {
                var locationRemove = (from loc in _context.Locations
                                      where loc.Uuid == userRemove.Uuid
                                      select loc).FirstOrDefault();

                _context.Locations.Remove(locationRemove);
                _context.Users.Remove(userRemove);
                _context.SaveChanges();
            }

        }
    }
}

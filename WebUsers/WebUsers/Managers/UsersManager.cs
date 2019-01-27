using System;
using System.Collections.Generic;
using System.Linq;
using WebUsers.Models;
using WebUsers.Services;

namespace WebUsers.Managers
{
    public class UsersManager
    {
        private readonly UserService _userService;

        public UsersManager(UserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> GetUsers(int limit, int page)
        {

            var users = _userService.GetUsers();

            return SortUsers(users)
                         .Skip(limit * (page - 1))
                         .Take(limit);
        }

        public User GetUser(string IdValue)
        {
            return _userService.GetUser(IdValue);
        }

        public User InsertUser(string Uuid, string idValue, string gender, string name, string email, DateTime birthDate, string userName, string state, string street, string city, string postalCode)
        {  
            return _userService.InsertUser(Uuid, idValue, gender, name, email, birthDate, userName, state, street, city, postalCode);
        }

        public User UpdateUser(string idValue, string gender, string name, string email, DateTime birthDate, string userName, string state, string street, string city, string postalCode)
        {
            return _userService.UpdateUser(idValue, gender, name, email, birthDate, userName, state, street, city, postalCode);
        }

        public void DeleteUser(string id)
        {
            _userService.DeleteUser(id);
        }

        private List<User> SortUsers(List<User> users)
        {
            List<User> sortedUsers = new List<User>();

            QuickSort(users, 0, users.Count - 1);

            return users;
        }

        private static void QuickSort(List<User> arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right);
                }
            }

        }

        private static int Partition(List<User> arr, int left, int right)
        {
            long pivot = ((DateTimeOffset)arr[left].BirthDate).ToUnixTimeSeconds();
            while (true)
            {

                while (((DateTimeOffset)arr[left].BirthDate).ToUnixTimeSeconds() < pivot)
                {
                    left++;
                }

                while (((DateTimeOffset)arr[right].BirthDate).ToUnixTimeSeconds() > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (((DateTimeOffset)arr[left].BirthDate).ToUnixTimeSeconds() == ((DateTimeOffset)arr[right].BirthDate).ToUnixTimeSeconds()) return right;

                    User temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}

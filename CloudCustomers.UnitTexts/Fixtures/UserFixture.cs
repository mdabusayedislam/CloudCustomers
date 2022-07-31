using CloudCusromers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTexts.Fixtures
{
    public static class UserFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Id = 1,
                Name = "Sayed1",
                Email = "sayed1@example.com",
                Address = new Address()
                {
                    Street = "Main Street",
                    City = "Dhaka",
                    ZipCode = "1000"
                }
            },
            new User
            {
                Id = 2,
                Name = "Sayed2",
                Email = "sayed2@example.com",
                Address = new Address()
                {
                    Street = "Main Street",
                    City = "Dhaka",
                    ZipCode = "1000"
                }
            },
            new User
            {
                Id = 3,
                Name = "Sayed3",
                Email = "sayed3@example.com",
                Address = new Address()
                {
                    Street = "Main Street",
                    City = "Dhaka",
                    ZipCode = "1000"
                }
            }

        };

    }
}

using System.Collections.Generic;
using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UserFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User
        {
            Id = 1,
            Name = "Jane Doe 1",
            Email = "arrange@arrange.com",
            Address = new Address()
            {
                Street = "123 main Street",
                City = "Madison",
                ZipCode = "5796"
            }

        },
        new User
        {
            Id = 2,
            Name = "Jane Doe 2",
            Email = "arrange2@arrange.com",
            Address = new Address()
            {
                Street = "1234 main Street",
                City = "Madison 2",
                ZipCode = "6666"
            }

        },
        new User
        {
            Id = 3,
            Name = "Jane Doe 3",
            Email = "arrange3@arrange.com",
            Address = new Address()
            {
                Street = "12345 main Street",
                City = "Madison 3",
                ZipCode = "33333"
            }

        }

    };
}
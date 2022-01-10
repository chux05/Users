using UserData.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Server.Configuration.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
               new User
               {
                   Id = 1,
                   FirstName = "John",
                   LastName = "Snow",
                   Email = "John@snow.com",
                   Valid = false
               },
                new User
                {
                    Id = 2,
                    FirstName = "Sally",
                    LastName = "Jones",
                    Email = "Sally@jones.com",
                    Valid = false
                },
                 new User
                 {
                     Id = 3,
                     FirstName = "Mary",
                     LastName = "Poppins",
                     Email = "mary@poppins.com",
                     Valid = false
                 }

               );
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SchoolWiz.Entity;

namespace SchoolWiz.Persistence.Extensions
{
    public static class DbContextSeed
    {
        public static void SetSchemas(this ModelBuilder builder)
        {
            builder.Entity<Account>().ToTable("Accounts", "billing");
            builder.Entity<AccountStatus>().ToTable("AccountStatuses", "billing");
            builder.Entity<AccountType>().ToTable("AccountTypes", "billing");
            builder.Entity<Invoice>().ToTable("Invoices", "billing");
            builder.Entity<InvoiceItem>().ToTable("InvoiceItems", "billing");
            builder.Entity<Statement>().ToTable("Statements", "billing");
            builder.Entity<AccountRate>().ToTable("AccountRates", "billing");
            builder.Entity<Rate>().ToTable("Rates", "billing");
        }

        public static void CreateIndexes(this ModelBuilder builder)
        {
            builder.Entity<AccountStatus>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<AccountType>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<Address>()
                .HasIndex(a => new { a.UnitNumber, a.ComplexName, a.StreetAddress, a.Suburb, a.CityId, a.PostalCode })
                .IsUnique(true);

            builder.Entity<AddressType>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<City>().HasIndex(c => new { c.Name, c.ProvinceId }).IsUnique();

            builder.Entity<Country>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<Grade>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<Guardian>().HasIndex(g => new { g.IdentityNumber, g.FirstName, g.LastName }).IsUnique();

            builder.Entity<GuardianType>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<GuardianAddress>().HasIndex(gt => new { gt.GuardianId, gt.AddressId }).IsUnique();

            builder.Entity<Province>().HasIndex(a => a.Name).IsUnique();

            builder.Entity<SchoolAddress>().HasIndex(sa => new { sa.SchoolId, sa.AddressId });

            builder.Entity<Student>().HasIndex(s => new { s.IdentityNumber, s.FirstName, s.LastName }).IsUnique();

            builder.Entity<StudentGuardian>().HasIndex(sg => new { sg.StudentId, sg.GuardianId }).IsUnique();

            builder.Entity<Vat>().HasIndex(v => new { v.Value, v.ValidFrom, v.ValidTo });

            builder.Entity<Rate>().HasIndex(r => new { r.Description, r.ValidFrom, r.ValidTo });

            builder.Entity<AccountRate>().HasIndex(ar => new { ar.AccountId, ar.RateId });
        }

        public static void SeedData(this ModelBuilder builder)
        {
            //Roles
            var administratorRoleId = Guid.NewGuid();
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = administratorRoleId,
                Name = "Administrator",
                NormalizedName = "Administrator"
            }, new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = "Teacher",
                NormalizedName = "Teacher"
            }, new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = "Billing",
                NormalizedName = "Billing"
            });
            
            var userId = Guid.NewGuid();
            var administrator = new ApplicationUser
            {
                Id = userId,
                UserName = "ludwign@lucasdev.co.za",
                NormalizedUserName = "LUDWIGN@LUCASDEV.CO.ZA",
                FirstName = "Ludwig",
                MiddleName = "Paul",
                LastName = "Nel",
                MobileNumber = "0835651243",
                Email = ";udwign@lucasdev.co.za",
                NormalizedEmail = "LUDWIGN@LUCASDEV.CO.ZA",
                IdentityNumber = "8101145019087",
                CreatedById = userId,
                DateCreated = DateTime.Now,
                EmailConfirmed = true,
                SecurityStamp = new Guid().ToString("D")
            };

            administrator.PasswordHash = PassGenerate(administrator, "LuCas@18701");

            builder.Entity<ApplicationUser>().HasData(administrator);

            var userRole = new IdentityUserRole<Guid>
            {
                RoleId = administratorRoleId,
                UserId = userId
            };

            builder.Entity<IdentityUserRole<Guid>>().HasData(userRole);

            //School.
            builder.Entity<School>().HasData(new School
            {
                Id = Guid.NewGuid(),
                Name = "Destiny Changers Academy",
                RegistrationNo = string.Empty,
                VatNo = string.Empty,
                ContactPerson = "Ted Coffin",
                PhoneNumber = "0824693520",
                Email = "tedc@destinychangeracademy.co.za",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.Now
            });

            //AccountStatus
            builder.Entity<AccountStatus>().HasData(new AccountStatus
                {
                    Id = Guid.Parse("55B35573-7D5C-4142-8C7F-9E1CE1425898"),
                    Name = "Active",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                },
                new AccountStatus
                {
                    Id = Guid.Parse("DE4649FD-E49D-43F7-9E04-A779CFADE4AE"),
                    Name = "Inactive",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                });

            builder.Entity<AccountStatus>().HasIndex(a => a.Name).IsUnique();

            //AccountType
            builder.Entity<AccountType>().HasData(new AccountType
            {
                Id = Guid.Parse("9CEDA47F-C8BA-4AF3-A9E0-DBF4ADBE80B1"),
                Name = "Monthly",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new AccountType
            {
                Id = Guid.Parse("23EC936B-C364-4E7B-BFFD-52DFFA0B4C94"),
                Name = "Annual",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new AccountType
            {
                Id = Guid.Parse("068CD348-F51F-4D5E-B265-5FF8DB1DE74B"),
                Name = "Add-hoc",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            });

            builder.Entity<AccountType>().HasIndex(a => a.Name).IsUnique();

            //AddressType
            builder.Entity<AddressType>().HasData(new AddressType
            {
                Id = Guid.Parse("0943A261-05B3-4334-9090-7B053166C2CE"),
                Name = "Postal",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            },
                new AddressType
                {
                    Id = Guid.Parse("4AB8918D-44C4-48F0-A7AC-11E1B21739BC"),
                    Name = "Physical",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                });

            builder.Entity<AddressType>().HasIndex(a => a.Name).IsUnique();

            //GuardianType
            builder.Entity<GuardianType>().HasData(new GuardianType
            {
                Id = Guid.Parse("8296C7A7-62E5-4FFA-BCDA-8EEF49DBB433"),
                Name = "Mother",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            },
                new GuardianType
                {
                    Id = Guid.Parse("B24A381B-D887-4AF2-A3DB-CD1226E03E86"),
                    Name = "Father",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                },
                new GuardianType
                {
                    Id = Guid.Parse("B9946F91-8E24-492F-9656-33FE2FEEEBC2"),
                    Name = "Grand Mother",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                },
                new GuardianType
                {
                    Id = Guid.Parse("1A227C52-A576-4222-8AFC-424921239072"),
                    Name = "Grand Father",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                },
                new GuardianType
                {
                    Id = Guid.Parse("73D81136-2647-45C7-A011-018F6BED2C8A"),
                    Name = "Aunt",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                },
                new GuardianType
                {
                    Id = Guid.Parse("3087B823-1620-4E7B-87FD-B5DD45F3D5F3"),
                    Name = "Uncle",
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                });

            //Grades
            builder.Entity<Grade>().HasData(new Grade
            {
                Id = Guid.NewGuid(),
                Name = "1",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "2",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "3",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "4",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "5",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "6",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "7",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "8",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "9",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "10",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "11",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Grade
            {
                Id = Guid.NewGuid(),
                Name = "12",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            });

            //Country
            var southAfricaId = Guid.NewGuid();
            builder.Entity<Country>().HasData(new Country
            {
                Id = southAfricaId,
                Name = "South Africa",
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            });

            //Provinces
            var gautengId = Guid.NewGuid();

            builder.Entity<Province>().HasData(new Province
            {
                Id = Guid.NewGuid(),
                Name = "Eastern Cape",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = Guid.NewGuid(),
                Name = "Free state",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = gautengId,
                Name = "Gauteng",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = Guid.NewGuid(),
                Name = "KwaZulu-Natal",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = Guid.NewGuid(),
                Name = "Limpopo",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = Guid.NewGuid(),
                Name = "Mpumalanga",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = Guid.NewGuid(),
                Name = "North West",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = Guid.NewGuid(),
                Name = "Northern Cape",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            }, new Province
            {
                Id = Guid.NewGuid(),
                Name = "Western Cape",
                CountryId = southAfricaId,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow
            });

            //Cities

            builder.Entity<City>().HasData(
                //Gauteng cities
                new City {
                    Id = Guid.NewGuid(),
                    Name = "Alexandra",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Johannesburg",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Lenasia",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Midrand",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Randburg",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Roodepoort",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Sandton",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Soweto",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Alberton",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Benoni",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Boksburg",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Brakpan",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Daveyton",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Edenvale",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Germiston",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Isando",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Kempton Park",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Nigel",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Reiger Park",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Springs",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Tembisa",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Centurion",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Hammanskraal",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Irene",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Pretoria",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Vanderbijlpark",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Vereeniging",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Meyerton",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Heidelberg",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Krugersdorp",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Magaliesburg",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Randfontein",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Bekkersdal",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                }, new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Westonaria",
                    ProvinceId = gautengId,
                    IsDeleted = false,
                    CreatedById = userId,
                    CreatedDate = DateTime.UtcNow
                });

            //VAT
            builder.Entity<Vat>().HasData(new Vat
            {
                Id = Guid.NewGuid(),
                Value = 15,
                ValidFrom = null,
                ValidTo = null,
                IsDeleted = false,
                CreatedById = userId,
                CreatedDate = DateTime.Now
            });
        }

        private static string PassGenerate(ApplicationUser user, string password)
        {
            var passHash = new PasswordHasher<ApplicationUser>();
            return passHash.HashPassword(user, password);
        }
    }
}

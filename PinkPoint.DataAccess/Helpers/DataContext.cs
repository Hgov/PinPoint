using Microsoft.EntityFrameworkCore;
using PinkPoint.Data.Domain;
using PinkPoint.Data.Enums;

namespace PinkPoint.DataAccess.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //test Users
            modelBuilder.Entity<User>().HasData(new User[] {
                new User{user_id=new Guid("c94b6b4b-b72a-4ad9-8ee1-357128f3bd95"),
                    first_name="Hayrettin",
                    last_name="Göv",
                    gender=Enums.Gender.Male,
                    birth_date=new DateTime(1993,04,09),
                    email="hayrettin.gov@gmail.com",
                    phone="05070053711",
                    bio="test aşamsında kullanıcı biyografisi",
                    status_active=true,
                    status_visibility=true,
                    creation_tsz=new DateTime(2022,10,23),
                    last_updated_tsz=new DateTime(2022,10,23)
                },
                 new User{user_id=new Guid("a94b6b4b-b72a-4ad9-8ee1-357128f3bd95"),
                    first_name="Victoria",
                    last_name="Mercedes",
                    gender=Enums.Gender.Female,
                    birth_date=new DateTime(1990,06,18),
                    email="victoria@gmail.com",
                    phone="05070053723",
                    bio="test aşamsında kullanıcı biyografisi",
                    status_active=true,
                    status_visibility=true,
                    creation_tsz=new DateTime(2022,10,23),
                    last_updated_tsz=new DateTime(2022,10,23)
                },
                  new User{user_id=new Guid("b94b6b4b-b72a-4ad9-8ee1-357128f3bd95"),
                    first_name="Angela",
                    last_name="Bear",
                    gender=Enums.Gender.Female,
                    birth_date=new DateTime(1992,04,09),
                    email="angela@gmail.com",
                    phone="05070053755",
                    bio="test aşamsında kullanıcı biyografisi",
                    status_active=true,
                    status_visibility=true,
                    creation_tsz=new DateTime(2022,10,23),
                    last_updated_tsz=new DateTime(2022,10,23)
                },
            });
        }
        public DbSet<User> Users { get; set; }
        // public DbSet<Log> Logs { get; set; }
    }
}

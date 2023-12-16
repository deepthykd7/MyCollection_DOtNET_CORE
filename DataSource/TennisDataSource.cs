using DTO.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataSource
{
    public class TennisDb : DbContext
    {
        private readonly IConfiguration Configuration;

        public TennisDb(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<UserRegistartionEntity> Users { get; set; }
        public DbSet<TennisCourtEntity> TennisCourts { get; set; }
        public DbSet<CourtBookingEntity> CourtBookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbConnection = Configuration.GetConnectionString("DbConnection");
                string password = Configuration.GetConnectionString("Password");

                string connectionString = $"{dbConnection}{password}";

                optionsBuilder.UseOracle(connectionString);
                optionsBuilder.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRegistartionEntity>().ToTable("USER_TENN");
            modelBuilder.Entity<TennisCourtEntity>().ToTable("TENNIS_COURTS");
            modelBuilder.Entity<CourtBookingEntity>().ToTable("COURT_BOOKINGS");
        }
    }



    public class TennisDataSource
    {
        private readonly TennisDb tennis;

        public TennisDataSource(TennisDb tennisDb)
        {
            tennis = tennisDb;
        }


        public void InsertUser(UserRegistartionEntity request)
        {

            tennis.Users.Add(request);
            tennis.SaveChanges();

        }

        public UserCredentials GetUserCredentialsByUsernameAndPassword(Login_request request)
        {
            var user = tennis.Users
                .Where(u => u.USERNAME == request.Username && u.PASSWORD == request.Password)
                .Select(u => new UserCredentials { USERNAME = u.USERNAME, PASSWORD = u.PASSWORD })
                .FirstOrDefault();

            return user;
        }
        public List<TennisCourtEntity> GetAllCourts()
        {
            return tennis.TennisCourts.ToList();
        }

        public TennisCourtEntity GetCourtById(string id)
        {
            return tennis.TennisCourts.Find(id);
        }

        public TennisCourtEntity CreateCourt(TennisCourtRequest request)
        {
            var newCourt = new TennisCourtEntity
            {
                COURT_NAME = request.CourtName,
                IS_INDOOR = request.IsIndoor,
                CAPACITY = request.Capacity,
                LOCATION = request.Location,
            };

            tennis.TennisCourts.Add(newCourt);
            tennis.SaveChanges();

            return newCourt;
        }

        public TennisCourtEntity UpdateCourt(string id, TennisCourtRequest request)
        {
            var existingCourt = tennis.TennisCourts.Find(id);

            if (existingCourt != null)
            {
                existingCourt.COURT_NAME = request.CourtName;
                existingCourt.IS_INDOOR = request.IsIndoor;
                existingCourt.CAPACITY = request.Capacity;
                existingCourt.LOCATION = request.Location;

                tennis.SaveChanges();
            }

            return existingCourt;
        }

        public bool DeleteCourt(string id)
        {
            var courtToDelete = tennis.TennisCourts.Find(id);

            if (courtToDelete != null)
            {
                tennis.TennisCourts.Remove(courtToDelete);
                tennis.SaveChanges();
                return true;
            }

            return false;
        }
        public List<CourtBookingEntity> GetAllBookings()
        {
            return tennis.CourtBookings.ToList();
        }

        public CourtBookingEntity GetBookingById(string id)
        {
            return tennis.CourtBookings.Find(id);
        }

        public void InsertCourtBooking(CourtBookingEntity courtBooking)
        {
            tennis.CourtBookings.Add(courtBooking);
            tennis.SaveChanges();
        }

        public void UpdateCourtBooking(CourtBookingEntity courtBooking)
        {
            tennis.CourtBookings.Update(courtBooking);
            tennis.SaveChanges();
        }

        public bool DeleteCourtBooking(int id)
        {
            var courtBookingToDelete = tennis.CourtBookings.Find(id);

            if (courtBookingToDelete != null)
            {
                tennis.CourtBookings.Remove(courtBookingToDelete);
                tennis.SaveChanges();
                return true;
            }

            return false;
        }



    }
    

    
}

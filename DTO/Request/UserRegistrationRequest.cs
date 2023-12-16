using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class UserRegistrationRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Name must contain only letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password and confirm password must match.")]
        public string ConfirmPassword { get; set; }


    }
    public class UserRegistartionEntity
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }


    }
    public class TennisCourtRequest
    {
        public string CourtName { get; set; }
        public bool IsIndoor { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
       
    }
    public class TennisCourtEntity
    {
        [Key]
        public string COURT_ID { get; set; }
        public string COURT_NAME { get; set; }
        public bool IS_INDOOR { get; set; }
        public int CAPACITY { get; set; }
        public string LOCATION { get; set; }
        
    }
    public class CourtBookingRequest
    {
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "CourtId is required")]
        public string CourtId { get; set; }

        [Required(ErrorMessage = "BookingDate is required")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        
            }
    public class CourtBookingEntity
    {
        [Key]
        public string BOOKING_ID { get; set; }

        [Required]
        public string USER_ID { get; set; }

        [Required]
        public string COURT_ID { get; set; }

        [Required]
        public DateTime BOOKING_DATE { get; set; }

        [Required]
        public DateTime BOOKED_DATE { get; set; }

        [Required]
        [MaxLength(50)]        
        public string STATUS { get; set; }

            }
    public class UserCredentials
    {
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
    }

}

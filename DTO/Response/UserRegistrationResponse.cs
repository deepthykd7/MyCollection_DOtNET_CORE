using System;

namespace DTO.Response
{
    public class UserRegistrationResponse
    {
        public string status { get; set; }
    }
    public class TennisCourtResponse
    {
        public string CourtId { get; set; }
        public string CourtName { get; set; }
        public bool IsIndoor { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
   
    }
    public class CourtBookingResponse
    {
        public string BookingId { get; set; }
        public string UserId { get; set; }
        public string CourtId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookedDate { get; set; }
        public string Status { get; set; }

            }
}

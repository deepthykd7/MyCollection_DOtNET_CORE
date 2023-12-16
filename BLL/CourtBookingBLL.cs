using DataSource;
using DTO;
using DTO.Request;
using DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CourtBookingBLL
    {
        private readonly TennisDataSource _tennisDataSource;

        public CourtBookingBLL(TennisDataSource tennisDataSource)
        {
            _tennisDataSource = tennisDataSource ?? throw new ArgumentNullException(nameof(tennisDataSource));
        }

        public BaseResponse<List<CourtBookingResponse>> GetAllBookings()
        {
            BaseResponse<List<CourtBookingResponse>> response = new BaseResponse<List<CourtBookingResponse>>();

            try
            {
                                var bookings = _tennisDataSource.GetAllBookings();

                                var bookingResponses = bookings.Select(b => new CourtBookingResponse
                {
                    BookingId = b.BOOKING_ID,
                    UserId = b.USER_ID,
                    CourtId = b.COURT_ID,
                    BookingDate = b.BOOKING_DATE,
                    BookedDate = b.BOOKED_DATE,
                    Status = b.STATUS
                                    }).ToList();

                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "Successfully retrieved all bookings";
                response.Data = bookingResponses;
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public BaseResponse<CourtBookingResponse> GetBookingById(string id)
        {
            BaseResponse<CourtBookingResponse> response = new BaseResponse<CourtBookingResponse>();

            try
            {
                                var booking = _tennisDataSource.GetBookingById(id);

                if (booking != null)
                {
                                        var bookingResponse = new CourtBookingResponse
                    {
                        BookingId = booking.BOOKING_ID,
                        UserId = booking.USER_ID,
                        CourtId = booking.COURT_ID,
                        BookingDate = booking.BOOKING_DATE,
                        BookedDate = booking.BOOKED_DATE,
                        Status = booking.STATUS
                                            };

                    response.apiStatus = ResponseTypeContants.SUCCESS;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.responseMsg = "Successfully retrieved booking by ID";
                    response.Data = bookingResponse;
                }
                else
                {
                    response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Booking not found";
                }
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public BaseResponse<CourtBookingResponse> CreateBooking(CourtBookingRequest request)
        {
            BaseResponse<CourtBookingResponse> response = new BaseResponse<CourtBookingResponse>();

            try
            {
                                if (request.BookingDate < DateTime.Now)
                {
                    response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Booking date cannot be in the past";
                    return response;
                }

                                var newBooking = new CourtBookingEntity
                {
                     USER_ID= request.UserId,
                     COURT_ID= request.CourtId,
                     BOOKING_DATE = request.BookingDate,
                     BOOKED_DATE  = DateTime.Now,
                                          STATUS = "Booked"                                     };

                _tennisDataSource.InsertCourtBooking(newBooking);

                                var bookingResponse = new CourtBookingResponse
                {
                    BookingId = newBooking.BOOKING_ID,
                    UserId = newBooking.USER_ID,
                    CourtId = newBooking.COURT_ID,
                    BookingDate = newBooking.BOOKING_DATE,
                    BookedDate = newBooking.BOOKED_DATE,
                    Status = newBooking.STATUS
                                    };

                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "Successfully created court booking";
                response.Data = bookingResponse;
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public BaseResponse<CourtBookingResponse> UpdateBooking(string id, CourtBookingRequest request)
        {
            BaseResponse<CourtBookingResponse> response = new BaseResponse<CourtBookingResponse>();

            try
            {
                                var existingBooking = _tennisDataSource.GetBookingById(id);

                if (existingBooking != null)
                {
                                        if (request.BookingDate < DateTime.Now)
                    {
                        response.apiStatus = ResponseTypeContants.FAIL;
                        response.status = ResponseTypeContants.FAIL;
                        response.responseMsg = "Booking date cannot be in the past";
                        return response;
                    }

                                        existingBooking.USER_ID = request.UserId;
                    existingBooking.COURT_ID = request.CourtId;
                    existingBooking.BOOKING_DATE = request.BookingDate;
                    existingBooking.STATUS = "Booked";                     
                    _tennisDataSource.UpdateCourtBooking(existingBooking);

                                        var bookingResponse = new CourtBookingResponse
                    {
                        BookingId = existingBooking.BOOKING_ID,
                        UserId = existingBooking.USER_ID,
                        CourtId = existingBooking.COURT_ID,
                        BookingDate = existingBooking.BOOKING_DATE,
                        BookedDate = existingBooking.BOOKED_DATE,
                        Status = existingBooking.STATUS
                                            };

                    response.apiStatus = ResponseTypeContants.SUCCESS;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.responseMsg = "Successfully updated court booking";
                    response.Data = bookingResponse;
                }
                else
                {
                    response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Booking not found";
                }
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public BaseResponse<string> CancelBooking(string id)
        {
            BaseResponse<string> response = new BaseResponse<string>();

            try
            {
                                var existingBooking = _tennisDataSource.GetBookingById(id);

                if (existingBooking != null)
                {
                                        existingBooking.STATUS= "Cancelled";                     
                    _tennisDataSource.UpdateCourtBooking(existingBooking);

                    response.apiStatus = ResponseTypeContants.SUCCESS;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.responseMsg = "Successfully cancelled court booking";
                                   }
                else
                {
                    response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Booking not found";
                                 }
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = $"An error occurred: {ex.Message}";
                          }

            return response;
        }
    }
}

using Core;
using DTO;
using DTO.Request;
using DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisCourtApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourtBookingController : ControllerBase
    {
            private readonly CourtBookingBLL _courtBookingBLL;

                        public CourtBookingController(CourtBookingBLL courtBookingBLL)
            {
                _courtBookingBLL = courtBookingBLL ?? throw new ArgumentNullException(nameof(courtBookingBLL));
            }

            [HttpGet("GetAllBookings")]
            public ActionResult<BaseResponse<List<CourtBookingResponse>>> GetAllBookings()
            {
                var response = _courtBookingBLL.GetAllBookings();
                return response;
            }

            [HttpGet("GetBookingById/{id}")]
            public ActionResult<BaseResponse<CourtBookingResponse>> GetBookingById(string id)
            {
                var response = _courtBookingBLL.GetBookingById(id);
                return response;
            }

            [HttpPost("CreateBooking")]
            public ActionResult<BaseResponse<CourtBookingResponse>> CreateBooking([FromBody] CourtBookingRequest request)
            {
                if (ModelState.IsValid)
                {
                    var response = _courtBookingBLL.CreateBooking(request);
                    return response;
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            [HttpPut("UpdateBooking/{id}")]
            public ActionResult<BaseResponse<CourtBookingResponse>> UpdateBooking(string id, [FromBody] CourtBookingRequest request)
            {
                if (ModelState.IsValid)
                {
                    var response = _courtBookingBLL.UpdateBooking(id, request);
                    return response;
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            [HttpDelete("CancelBooking/{id}")]
            public ActionResult<BaseResponse<string>> CancelBooking(string id)
            {
                var response = _courtBookingBLL.CancelBooking(id);
                return response;
            }
        }
    }


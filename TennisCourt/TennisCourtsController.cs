using BLL;
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
    public class TennisCourtsController : ControllerBase
    {
        private readonly TennisCourtBLL _tennisCourtBLL;

                public TennisCourtsController(TennisCourtBLL tennisCourtBLL)
        {
            _tennisCourtBLL = tennisCourtBLL ?? throw new ArgumentNullException(nameof(tennisCourtBLL));
        }

        [HttpGet("GetAllCourts")]
        public ActionResult<BaseResponse<List<TennisCourtResponse>>> GetAllCourts()
        {
            var response = _tennisCourtBLL.GetAllCourts();
            return response;
        }

        [HttpGet("GetCourtById/{id}")]
        public ActionResult<BaseResponse<TennisCourtResponse>> GetCourtById(string id)
        {
            var response = _tennisCourtBLL.GetCourtById(id);
            return response;
        }

        [HttpPost("CreateCourt")]
        public ActionResult<BaseResponse<TennisCourtResponse>> CreateCourt([FromBody] TennisCourtRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _tennisCourtBLL.CreateCourt(request);
                return response;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("UpdateCourt/{id}")]
        public ActionResult<BaseResponse<TennisCourtResponse>> UpdateCourt(string id, [FromBody] TennisCourtRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _tennisCourtBLL.UpdateCourt(id, request);
                return response;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("DeleteCourt/{id}")]
        public ActionResult<BaseResponse<string>>DeleteCourt(string id)
        {
            var response = _tennisCourtBLL.DeleteCourt(id);
            return response;
        }
    }
}

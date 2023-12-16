using DataSource;
using DTO;
using DTO.Request;
using DTO.Response;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class TennisCourtBLL
    {
        private readonly TennisDataSource _tennisDataSource;

                public TennisCourtBLL(TennisDataSource tennisDataSource)
        {
            _tennisDataSource = tennisDataSource ?? throw new ArgumentNullException(nameof(tennisDataSource));
        }

        public BaseResponse<List<TennisCourtResponse>> GetAllCourts()
        {
            BaseResponse<List<TennisCourtResponse>> response = new BaseResponse<List<TennisCourtResponse>>();
            List<TennisCourtResponse> listresponse = new List<TennisCourtResponse>();

            try
            {
                List<TennisCourtEntity> tennisCourts = _tennisDataSource.GetAllCourts();
                foreach (var entity in tennisCourts)
                {
                    TennisCourtResponse tennisCourtResponse = new TennisCourtResponse
                    {
                                                  Capacity = entity.CAPACITY,
                           CourtId=entity.COURT_ID,
                            CourtName=entity.COURT_NAME,
                             IsIndoor=entity.IS_INDOOR,
                              Location=entity.LOCATION
                        
                                            };

                    listresponse.Add(tennisCourtResponse);
                }
                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "Tennis courts retrieved successfully";
                response.Data = listresponse;
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = "An error occurred while retrieving tennis courts";
                response.status = ex.Message;
            }

            return response;
        }

        public BaseResponse<TennisCourtResponse> GetCourtById(string id)
        {
            BaseResponse<TennisCourtResponse> response = new BaseResponse<TennisCourtResponse>();

            try
            {
                TennisCourtResponse tennisCourt = new TennisCourtResponse();
                TennisCourtEntity courtEntity = new TennisCourtEntity();
                courtEntity = _tennisDataSource.GetCourtById(id);
                tennisCourt.Capacity = courtEntity.CAPACITY;
                tennisCourt.CourtId = courtEntity.COURT_ID;
                tennisCourt.CourtName = courtEntity.COURT_NAME;
                tennisCourt.IsIndoor = courtEntity.IS_INDOOR;
                tennisCourt.Location = courtEntity.LOCATION;

                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "Tennis court retrieved successfully";
                response.Data = (TennisCourtResponse)tennisCourt;
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = "An error occurred while retrieving tennis court";
                response.status = ex.Message;
            }

            return response;
        }

        public BaseResponse<TennisCourtResponse> CreateCourt(TennisCourtRequest request)
        {
            BaseResponse<TennisCourtResponse> response = new BaseResponse<TennisCourtResponse>();

            try
            {
                var tennisCourt = _tennisDataSource.CreateCourt(request);
                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "Tennis court created successfully";
                
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = "An error occurred while creating tennis court";
                response.status = ex.Message;
            }

            return response;
        }

        public BaseResponse<TennisCourtResponse> UpdateCourt(string id, TennisCourtRequest request)
        {
            BaseResponse<TennisCourtResponse> response = new BaseResponse<TennisCourtResponse>();

            try
            {
                var tennisCourt = _tennisDataSource.UpdateCourt(id, request);
                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "Tennis court updated successfully";
              
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = "An error occurred while updating tennis court";
                response.status = ex.Message;
            }

            return response;
        }

        public BaseResponse<string> DeleteCourt(string id)
        {
            BaseResponse<string> response = new BaseResponse<string>();

            try
            {
                var isDeleted = _tennisDataSource.DeleteCourt(id);
                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "Tennis court deleted successfully";
                
            }
            catch (Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = "An error occurred while deleting tennis court";
                response.status = ex.Message;
            }

            return response;
        }
    }
}

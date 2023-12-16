using DataSource;
using DTO;
using DTO.Request;
using DTO.Response;
using Helper;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;

namespace BLL
{
    public class UserRegistrationBLL
    {
        private readonly TennisDataSource _tennisDataSource;

                public UserRegistrationBLL(TennisDataSource tennisDataSource)
        {
            _tennisDataSource = tennisDataSource ?? throw new ArgumentNullException(nameof(tennisDataSource));
        }

        static string GenerateRandomId()
        {
                        string prefix = "TB";

                        string randomPart = Guid.NewGuid().ToString("N").Substring(0, 8);

                        string randomId = prefix + randomPart;

            return randomId;
        }

        public BaseResponse<UserRegistrationResponse> RegisterUser(UserRegistrationRequest request)
        {
            BaseResponse<UserRegistrationResponse> response = new BaseResponse<UserRegistrationResponse>();
            UserRegistrationResponse userRegistration = new UserRegistrationResponse();

            try
            {
                                PasswordHash passwordHasher = new PasswordHash();
                                UserRegistartionEntity registrationEntity = new UserRegistartionEntity();
                registrationEntity.ID= GenerateRandomId();
                registrationEntity.NAME = request.Name;
                registrationEntity.USERNAME = request.Username;
                registrationEntity.PASSWORD = passwordHasher.HashPassword(request.Password);
                _tennisDataSource.InsertUser(registrationEntity);

                                response.apiStatus = ResponseTypeContants.SUCCESS;
                response.status = ResponseTypeContants.SUCCESS;
                response.responseMsg = "User registered successfully";
                response.Data = userRegistration;
            }
            catch (Exception ex)

            {
                if (ex.InnerException is OracleException oracleException1 && oracleException1.Number == 1 && oracleException1.Message.Contains("unique constraint"))
                {
                                        response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Username must be unique. Please try another.";
                    response.status = ex.Message;
                }
                else if (ex.InnerException is OracleException oracleException && oracleException.Number == 12899)
                {
                                        response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Value too large for the column.";
                    response.status = ex.Message;
                }
                                                else
                {
                    response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "An error occurred during user registration";
                    response.status = ex.Message;
                }
            }

            return response;
        }
        
        public BaseResponse<LoginResponse> LoginUser(Login_request login_Request)
        {
            PasswordHash passwordHasher = new PasswordHash();
            BaseResponse<LoginResponse> response = new BaseResponse<LoginResponse>();
          
            string retrievedUsername, retrievedPassword = null;


            try 
            {
                var userCredentials = _tennisDataSource.GetUserCredentialsByUsernameAndPassword(login_Request);

                if (userCredentials != null && userCredentials.USERNAME != null && userCredentials.PASSWORD != null)
                {
                    retrievedUsername = userCredentials.USERNAME;
                    retrievedPassword = userCredentials.PASSWORD;
                }
                else {
                    response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Invalid Username or Password";
                }
               

                                bool passwordMatch = passwordHasher.VerifyPassword(login_Request.password,retrievedPassword);

                if (passwordMatch)
                {
                    response.apiStatus = ResponseTypeContants.SUCCESS;
                    response.status = ResponseTypeContants.SUCCESS;
                    response.responseMsg = "Successfully Logged In";
                }
                else
                {
                    response.apiStatus = ResponseTypeContants.FAIL;
                    response.status = ResponseTypeContants.FAIL;
                    response.responseMsg = "Invalid Username or Password";

                }

            }
            catch(Exception ex)
            {
                response.apiStatus = ResponseTypeContants.FAIL;
                response.status = ResponseTypeContants.FAIL;
                response.responseMsg = "Error";
                response.status = ex.Message;

            }
            return response;
        }
    
    
    
    }


}

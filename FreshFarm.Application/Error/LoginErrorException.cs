using System.Net;
using FreshFarm.Domain.Service.Error;

namespace FreshFarm.Application.Error;
public class LoginErrorException : Exception, IExceptionService
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "User with given email does not exist.";
    }
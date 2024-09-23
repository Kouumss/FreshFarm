using System.Net;
using FreshFarm.Contract.Service.Error;

namespace FreshFarm.Application.Error;

public class DuplicateEmailException : Exception, IExceptionService
{
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Email already exist.";
}

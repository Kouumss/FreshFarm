using System.Net;
using FreshFarm.Domain.Service.Error;

namespace FreshFarm.Application.Error;

public class PasswordErrorException : Exception, IExceptionService
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public string ErrorMessage => "Invalid password.";
}


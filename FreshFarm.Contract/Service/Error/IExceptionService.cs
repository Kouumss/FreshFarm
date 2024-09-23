using System;
using System.Net;

namespace FreshFarm.Contract.Service.Error;

public interface IExceptionService
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}

using System.Net;

namespace FreshFarm.Domain.Service.Error;

public interface IExceptionService
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}

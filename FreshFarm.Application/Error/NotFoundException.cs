using System;
using System.Net;
using FreshFarm.Domain.Service.Error;

namespace FreshFarm.Application.Error;

public class NotFoundException : Exception, IExceptionService
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => "Resource not found.";
        
        public NotFoundException(string message) : base(message)
        {
        }
    }

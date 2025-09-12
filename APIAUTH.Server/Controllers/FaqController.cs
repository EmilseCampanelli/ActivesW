using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : GenericController<IFaqService, FaqDto>
    {
        private readonly IFaqService _service;


        public FaqController(IFaqService service) : base(service)
        {
            _service = service;
        }
    }
}


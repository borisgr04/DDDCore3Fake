using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain.Contracts;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaBancariaController : ControllerBase
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IEmailSender _emailSender;

        public CuentaBancariaController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        [HttpPost]
        public ActionResult<CrearCuentaBancariaResponse> Post(CrearCuentaBancariaRequest request)
        {
            CrearCuentaBancariaService service = new CrearCuentaBancariaService(_unitOfWork, _emailSender);
            CrearCuentaBancariaResponse response = service.Ejecutar(request);
            return Ok(response);
        }

        [HttpPost("consignacion")]
        public ActionResult<ConsignarResponse> Post(ConsignarRequest request)
        {
            var _service = new ConsignarService(_unitOfWork);
            var response = _service.Ejecutar(request);
            return Ok(response);
        }
    }
}
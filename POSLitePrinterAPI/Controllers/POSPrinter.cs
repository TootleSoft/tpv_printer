using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSLitePrinterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class POSPrinterController : ControllerBase
    {

        [HttpPost("Apertura")]
        public IActionResult Apertura([FromBody] POSRegister register)
        {
            PrintService.Apertura(register.POSPrinter, register);
            return Ok();
        }

        [HttpPost("Arqueo")]
        public IActionResult Arqueo([FromBody] POSRegister register)
        {
            PrintService.Arqueo(register.POSPrinter, register);
            return Ok();
        }

        [HttpPost("Cierre")]
        public IActionResult Cierre([FromBody] POSRegister register)
        {
            PrintService.Cierre(register.POSPrinter, register);
            return Ok();
        }

        [HttpPost("Preparacion")]
        public IActionResult Preparacion([FromBody] POSEnc orden)
        {
            PrintService.Preparacion(orden.POSPrinter, orden);
            return Ok();
        }

        [HttpPost("Cocina")]
        public IActionResult Cocina([FromBody] POSEnc orden)
        {
            PrintService.Cocina(orden.POSPrinter, orden);
            return Ok();
        }

        [HttpPost("Pago")]
        public IActionResult Pago([FromBody] POSEnc orden)
        {
            PrintService.Pago(orden.POSPrinter, orden);
            return Ok();
        }

        [HttpGet]
        public IActionResult Load() {
            return Ok("POSPrinterLite funcionando!");
        }
    }
}

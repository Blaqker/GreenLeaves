using Microsoft.AspNetCore.Mvc;
using Models.Domain;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenLeaves.Controllers
{
    [ApiController]
    [Route("api/ubicaciones")]
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionServicio _ubicacionServicio;

        public UbicacionController(IUbicacionServicio ubicacionServicio)
        {
            _ubicacionServicio = ubicacionServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ubicacion>>> Get()
        {
            return await _ubicacionServicio.Get();
        }
    }
}

using GreenLeaves.Models;
using Microsoft.AspNetCore.Mvc;
using Models.Domain;
using Models.Domain.Custom;
using Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLeaves.Controllers
{
    [ApiController]
    [Route("api/contactos")]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService _contactoService;
        private readonly IEmailProviderService _emailProviderService;
        private readonly IUbicacionServicio _ubicacionServicio;

        public ContactoController(
            IContactoService contactoService,
            IEmailProviderService emailProviderService,
            IUbicacionServicio ubicacionServicio)
        {
            _contactoService = contactoService;
            _emailProviderService = emailProviderService;
            _ubicacionServicio = ubicacionServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contacto>>> Get() 
        {
            return await _contactoService.Get();
        }

        [HttpPost]
        [System.Obsolete]
        public async Task<ActionResult> Post([FromBody] ContactoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();

                return BadRequest(errors);
            }

            bool result = await _contactoService.Create(new Contacto { 
                Nombre = model.Nombre,
                Email = model.Email,
                Telefono = model.Telefono,
                Fecha = model.Fecha,
                UbicacionId = model.UbicacionId
            });

            if (!result)
                return BadRequest("Error en la petición.");

            string HtmlBody = string.Empty;

            using (StreamReader SourceReader = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), @"Pages/EmailTemplate.cshtml")))
            {
                HtmlBody = SourceReader.ReadToEnd();
            }

            var ubicacion = await _ubicacionServicio.Get(model.UbicacionId);

            MailRequest mail = new MailRequest();
            mail.Email = model.Email;
            mail.Subject = "GreenLeaves";
            mail.Body = HtmlBody.Replace("{0}", model.Nombre)
                                .Replace("{1}", model.Email)
                                .Replace("{2}", ubicacion.Descripcion)
                                .Replace("{3}", model.Fecha.ToString("dd-MMM-yyyy"));
            await _emailProviderService.Send(mail);
            
            return Ok();
        }
    }
}

using GianssWebApi.Data;
using GianssWebApi.Models;
using GianssWebApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

//El controlador en las apps web sirven para controlar (valga la redundancia) todos los endpoints y las paginas, el metodo por el que se van a realizar, es aca donde se introducen las validaciones, para este caso solo tenemos un controlador porque solo estamos aprendiendo como se usan los metodos http.

namespace GianssWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GianssController : ControllerBase
    {
        private readonly ILogger<GianssController> _logger; //El logger es para mandar mensajes a la consola, para que quede de registro cuando se hizo algun                                                        registro o un error, por ejemplo.


        private readonly ApplicationDbContext _context; //Implementamos la Db en el controlador con la ApplicationDbContext

        public GianssController(ILogger<GianssController> logger, ApplicationDbContext context)
        {

            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<GianDto>> GetGian() //IEnumerable = retorna una lista de objetos
        {
            return Ok(_context.Gians.ToList()); //Retorna TODA la lista de usuarios, con todos sus campos.

        }

        [HttpGet("id", Name = "GetGian")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GianDto> GetGian(int id)
        {
            _logger.LogInformation("Obteniendo todos los registros"); //Aplicando el logger.

            if (id == 0) {
                _logger.LogError("Error con id = " + id);
                return BadRequest("No se ha encontrado, Quizás porque no hay usuarios"); // se manda un 400 porque no existe el id = 0.

            }
           // var gian = GianStore.GianList.FirstOrDefault(x => x.Id == id);//Funcion lambda --- sin base de datos
            var gian = _context.Gians.FirstOrDefault(x => x.Id == id); //Con base de datos
          //            DB      Tabla   Primer id que coincida con el mandado por parametro.             


            if (gian == null) //si no hay ningun registro con el id mandado, se manda un 404.
            {
                return NotFound();
            }

            return Ok(gian); //Si sí, se manda el objeto con el id seleccionado.
        }

        [HttpPost] //meter usuario.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Validaciones
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<GianDto> CreateGian([FromBody] GianDto gianDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //validacion personalizada 
            }
        //  if (GianStore.GianList.FirstOrDefault(x => x.Name.ToLower() == gianDto.Name.ToLower()) != null) //Sin base de datos
            if(_context.Gians.FirstOrDefault(x=> x.Name.ToLower() == gianDto.Name.ToLower()) != null) //Con base de datos
            {
                ModelState.AddModelError("NombreYaExiste", "Ese nombre ya está registrado!");
                return BadRequest(ModelState);
            }

            if (gianDto == null) //si no se ingresa ningun dato, se envia un 400.
            {
                return BadRequest(gianDto);

            }
            if (gianDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            /*
            gianDto.Id = GianStore.GianList.OrderByDescending(x => x.Id) //Se ordena la lista de mayor al menor, y se le asigna al dto el id mas alto +1.
                                           .FirstOrDefault().Id + 1;  */ //Sin base de datos.

            //GianStore.GianList.Add(gianDto);
            Gian model = new()
            {
                //Id = gianDto.Id,
                Name = gianDto.Name,
                Detalle = gianDto.Detalle,
                ImagenUrl = gianDto.ImagenUrl,
                Ocupantes = gianDto.Ocupantes,
                Tarifa = gianDto.Tarifa,
                MetrosCuadrados = gianDto.MetrosCuadrados,
                Amenidad = gianDto.Amenidad,
                CreationDate = DateTime.Now,
            };
            _context.Gians.Add(model);
            _context.SaveChanges();

            return CreatedAtRoute("GetGian", new { id = gianDto.Id }, gianDto);

        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteGian(int id)// se usa IActionResult porque podemos generar un NoContent (204). con ActionResult debemos devolver un Ok.
        {
            if (id == 0)
            {
                return BadRequest();
            }
            // var Gian = GianStore.GianList.FirstOrDefault(x => x.Id == id);
            var Gian =_context.Gians.FirstOrDefault( x => x.Id == id);

            if (Gian == null)
            {
                return NotFound();
            }
            _context.Gians.Remove(Gian);
            _context.SaveChanges();

            //GianStore.GianList.Remove(Gian);

            return NoContent();

        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateGian(int id, [FromBody] GianDto gianDto) //Se introduce un objeto el cual será el que se modifique.
        {
            if (gianDto == null || id != gianDto.Id)
            {
                return BadRequest();
            }
            //var Gian = GianStore.GianList.FirstOrDefault(x => x.Id == id);

            //Gian.Name = gianDto.Name;                        //Al usar HttpPut se tienen que actualizar todos los datos
            //Gian.MetrosCuadrados = gianDto.MetrosCuadrados;
            //Gian.Ocupantes = gianDto.Ocupantes;

            var gian = _context.Gians.AsNoTracking().FirstOrDefault(x => x.Id == id);
            Gian model = new()
            {
                Id = gianDto.Id,
                Name = gianDto.Name,
                Detalle = gianDto.Detalle,
                ImagenUrl = gianDto.ImagenUrl,
                Ocupantes = gianDto.Ocupantes,
                Tarifa = gianDto.Tarifa,
                MetrosCuadrados = gianDto.MetrosCuadrados,
                Amenidad = gianDto.Amenidad,
                UptdateDate = DateTime.Now
            };
            _context.Gians.Update(model);
            _context.SaveChanges();

            return NoContent();

        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialGian(int id, JsonPatchDocument<GianDto> patchDto)
        {

            if (id == 0 || patchDto == null)
            {
                return BadRequest();
            }

            //var Gian = GianStore.GianList.FirstOrDefault(x=> x.Id == id);
            var Gian = _context.Gians.AsNoTracking().FirstOrDefault(x => x.Id == id);
            GianDto gianDto = new()
            {
                Id = Gian.Id,
                Name = Gian.Name,
                Detalle = Gian.Detalle,
                ImagenUrl = Gian.ImagenUrl,
                Ocupantes = Gian.Ocupantes,
                Tarifa = Gian.Tarifa,
                MetrosCuadrados = Gian.MetrosCuadrados,
                Amenidad = Gian.Amenidad,


            };

            if (Gian == null) return BadRequest();

            patchDto.ApplyTo(gianDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Gian model = new()
            {
                Id = gianDto.Id,
                Name = gianDto.Name,
                Detalle = gianDto.Detalle,
                ImagenUrl = gianDto.ImagenUrl,
                Ocupantes = gianDto.Ocupantes,
                Tarifa = gianDto.Tarifa,
                MetrosCuadrados = gianDto.MetrosCuadrados,
                Amenidad = gianDto.Amenidad,
                UptdateDate = DateTime.Now
            };
            _context.Gians.Update(model);
            _context.SaveChanges();

            return NoContent();

        }
    }
}

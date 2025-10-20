using API_CuentasBancarias.Data;
using API_CuentasBancarias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_CuentasBancarias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Inyección de dependencias del DbContext
        public CuentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/cuentas
        // Obtener todas las cuentas bancarias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaBancaria>>> GetCuentasBancarias() 
        {
            var cuentas = await _context.CuentasBancarias.ToListAsync();
            return Ok(cuentas);
        } //async/await: Operación asíncrona para no bloquear el hilo


        // GET: api/cuentas/5
        // Obtener una cuenta específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaBancaria>> GetCuentaBancaria(int id)
        {
            var cuenta = await _context.CuentasBancarias.FindAsync(id);

            if (cuenta == null)
            {
                return NotFound(new { mensaje = $"No se encontró la cuenta con ID {id}" });
            }

            return Ok(cuenta);
        }

        // POST: api/cuentas
        // Crear una nueva cuenta bancaria
        [HttpPost]
        public async Task<ActionResult<CuentaBancaria>> PostCuentaBancaria(CuentaBancaria cuenta)
        {
            // Validar que el modelo sea correcto
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CuentasBancarias.Add(cuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuentaBancaria), new { id = cuenta.Id }, cuenta);
        }

        // PUT: api/cuentas/5
        // Actualizar una cuenta bancaria existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentaBancaria(int id, CuentaBancaria cuenta)
        {
            if (id != cuenta.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID de la cuenta" });
            }

            _context.Entry(cuenta).State = EntityState.Modified; //EntityState.Modified: Marca todas las propiedades como modificadas

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
                {
                    return NotFound(new { mensaje = $"No se encontró la cuenta con ID {id}" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/cuentas/5
        // Eliminar una cuenta bancaria
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentaBancaria(int id)
        {
            var cuenta = await _context.CuentasBancarias.FindAsync(id);

            if (cuenta == null)
            {
                return NotFound(new { mensaje = $"No se encontró la cuenta con ID {id}" });
            }

            _context.CuentasBancarias.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si existe una cuenta
        private bool CuentaExists(int id)
        {
            return _context.CuentasBancarias.Any(e => e.Id == id);
        } //Verifica si existe una cuenta con el ID especificado sin cargar el objeto completo
          //(más eficiente que FindAsync para solo verificar existencia).
    }
}
using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : Controller
{
    private readonly PresupuestosRepository PresuRepo;

    public PresupuestosController()
    {
        PresuRepo = new PresupuestosRepository();
    }

    [HttpPost("AltaPresupuesto")]
    public IActionResult AltaPresupuesto([FromBody] Presupuesto NuevoPresupuesto)
    {
        PresuRepo.CrearPresupuesto(NuevoPresupuesto);
        return Ok("Presupuesto creado con exito");
    }

    [HttpPost("AgregarProducto")]
    public IActionResult AgregarProducto(int Id, [FromBody] PresupuestoDetalle detalle)
    {
        PresuRepo.AgregarProducto(Id,detalle);
        return Ok("Producto agregado con exito");
    }

    [HttpGet("DetallePresupuesto")]
    public ActionResult<Presupuesto> DetallePresupuesto(int Id)
    {
        var detalles = PresuRepo.GetPresupuestoById(Id);
        if (detalles == null) return NotFound($"Presupuesto de id:{Id} no fue encontrado");

        return Ok(detalles);
    }

    [HttpGet("ListarPresupuestos")]
    public ActionResult<List<Presupuesto>> ListarPresupuestos()
    {
        var ListaPresupuesto = PresuRepo.ListaDePresupuesto();
        return Ok(ListaPresupuesto);
    }

    [HttpDelete("EliminarPresupuesto")]
    public IActionResult EliminarPresupuesto(int Id)
    {
        try
        {
            bool check = PresuRepo.EliminarPresupuesto(Id);
            if (!check) return NotFound($"El presupuesto de id:{Id} no fue encontrado");
            return Ok("Presupuesto Eliminado");
        }
        catch (Exception ex)
        {

            return BadRequest($"No se pudo eliminar el presupuesto : {ex.Message}");
        }
    }
}
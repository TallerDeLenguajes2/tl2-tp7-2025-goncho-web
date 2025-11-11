using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]

public class ProductosController : ControllerBase
{
    private ProductoRepository productoRepo;
    public ProductosController()
    {
        productoRepo = new ProductoRepository();
    }
    //A partir de aquí van todos los Action Methods (Get, Post,etc.)

    [HttpPost("CrearProducto")]
    public IActionResult CrearProducto([FromBody] Producto Nuevoproducto)
    {
        try
        {
            productoRepo.CreaProducto(Nuevoproducto);
            return Ok("Producto fue creado con exito");
        }
        catch (Exception ex)
        {
            return BadRequest($"No fue posible crear el producto {ex.Message}");
        }

        /* productoRepo.CrearProducto(NuevoProducto);
        return Ok("Producto dado de alta exitosamente"); */
    }

    [HttpPut("ModificarPorducto")]
    public IActionResult ModificarProducto(int id, [FromBody] Producto producto)
    {
        try
        {
            bool check = productoRepo.ModificarProducto(id, producto);
            if (!check) return NotFound($"El producto de id : {id} no fue encontrado");
            return Ok($"El producto de id : {id} fue modificado");
        }
        catch (Exception ex)
        {

            return BadRequest($"$No fue posible modificar el producto {ex.Message}");
        }
    }

    [HttpGet("ListarProducto")]
    public IActionResult ListarProducto()
    {
        var ListProductos = productoRepo.ListaDeProductos();
        return Ok(ListProductos);
    }

    [HttpGet("ObtenerProducto")]
    public IActionResult ObtenerProducto(int Id)
    {
        var producto = productoRepo.DetalleProducto(Id);
        if (producto == null) return NotFound($"No se encontro el producto de id:{Id}");

        return Ok(producto);
    }

    [HttpDelete("EliminarProducto")]
    public IActionResult EliminarProducto(int Id)
    {
        bool check = productoRepo.EliminarProducto(Id);
        if (!check) return NotFound($"No se encontro el producto de id:{Id}");

        return NoContent();
    }
}
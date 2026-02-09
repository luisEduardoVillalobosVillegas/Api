using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Custom;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Reflection.Metadata.Ecma335;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DbpruebaContext _dbPruebaContext;
        public ProductoController(DbpruebaContext dbPruebaContext)
        {
            _dbPruebaContext = dbPruebaContext;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _dbPruebaContext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }


        [HttpPost]
        [Route("NuevoProducto")]
        public async Task<IActionResult> NuevoProducto(ProductoDTO objeto)
        {
            var modeloProducto = new Producto
            {
                Nombre = objeto.Nombre,
                Marca = objeto.Marca,
                Precio = objeto.Precio
            };

            await _dbPruebaContext.Productos.AddAsync(modeloProducto);
            await _dbPruebaContext.SaveChangesAsync();

            if (modeloProducto.IdProducto != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });



        }

        [HttpPut]
        [Route("ActualizarProducto")]
        public async Task<IActionResult> ActualizarProducto(
    [FromBody] ProductoUpdateDTO objeto)
        {
            var producto = await _dbPruebaContext.Productos
                .FindAsync(objeto.IdProducto);

            if (producto == null)
                return NotFound();

            producto.Nombre = objeto.Nombre;
            producto.Marca = objeto.Marca;
            producto.Precio = objeto.Precio;

            await _dbPruebaContext.SaveChangesAsync();

            return Ok(new { isSuccess = true });
        }

        [HttpDelete("EliminarProducto/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _dbPruebaContext.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            _dbPruebaContext.Productos.Remove(producto);
            await _dbPruebaContext.SaveChangesAsync();

            return NoContent(); // 204
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using JWTWebAPI.Data;
using JWTWebAPI.Models;

namespace JWTWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        protected readonly ConexionDBContext _context;

        public ProductoController(ConexionDBContext context)
        {
            _context = context;
        }
        //Read
        [HttpGet]
        [Route("ListaProductos")]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            var listaProductos = await _context.Producto.ToListAsync();

            return listaProductos;
        }
        //ReadBy
        [HttpGet]
        [Route("VerProducto")]
        public async Task<ActionResult<Producto?>> Get(int id)
        {
            var producto = await _context.Producto.Where(p => p.Id == id).FirstOrDefaultAsync();

            return producto;
        }
        //Create
        [HttpPost]
        [Route("CrearProducto")]
        public async Task<ActionResult<Producto>> POST(Producto producto)
        {
            _context.Producto.Add(producto);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new
            {
                id = producto.Id
            }, producto);
        }
        //Update
        [HttpPut]
        [Route("ActualizarProducto")]
        public async Task<ActionResult<Producto?>> Update(Producto producto)
        {
            _context.Producto.Update(producto);

            await _context.SaveChangesAsync();

            return await _context.Producto.FindAsync(producto.Id);
        }
        //Delete
        [HttpDelete]
        [Route("BorrarProducto")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
                var producto = await _context.Producto.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (producto != null)
                {
                    _context.Producto.Remove(producto);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
        }
    }
}

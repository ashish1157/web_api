using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProduct()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<Products> products = await SelectAllProducts(connection);
            return Ok(products);
        }



        




        [HttpGet("{productId}")]
        public async Task<ActionResult<Products>> GetProduct(int productId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var products = await connection.QueryFirstAsync<Products>("select * from products where productid=@Id",
                new { Id= productId});
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Products>>> CreateProduct(Products pro)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into products (name,unitPrice ) values (@Name, @UnitPrice)", pro);
            return Ok(await SelectAllProducts(connection));
        }


        [HttpPut]
        public async Task<ActionResult<IEnumerable<Products>>> UpdateProduct(Products pro)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("update products set name= @Name, unitPrice=@UnitPrice where productid=@ProductId", pro);
            return Ok(await SelectAllProducts(connection));
        }


        [HttpDelete]
        public async Task<ActionResult<IEnumerable<Products>>> DeleteProduct(int productId )
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from products where productid=@Id", new { Id = productId });
            return Ok(await SelectAllProducts(connection));
        }


        private static async Task<IEnumerable<Products>> SelectAllProducts(SqlConnection connection)
        {
            return await connection.QueryAsync<Products>("select * from products");
        }


    }
}

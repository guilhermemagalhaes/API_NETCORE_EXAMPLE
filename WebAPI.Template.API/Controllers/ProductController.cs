using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Template.API.Models.Response;

namespace WebAPI.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("GetProducts")]        
        [Authorize]
        public ActionResult<IList<ProductResponse>> Get()
        {
            List<ProductResponse> products = new List<ProductResponse>();

            for (int i = 1; i < 11; i++)
            {
                products.Add(new ProductResponse()
                {
                    ProductId = i,
                    Name = "Product Teste " + i.ToString(),
                    Price = Convert.ToDouble(i * 10)
                }); ;
            }

            return Ok(products);
        }
    }
}
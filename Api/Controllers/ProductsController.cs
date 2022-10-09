using Api.Base;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return FromResult(await _productService.GetAsync(id, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return FromResult(await _productService.GetAsync(cancellationToken));
    }

    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetByCategory(int id, CancellationToken cancellationToken)
    {
        return FromResult(await _productService.GetAsync(id, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product product, CancellationToken cancellationToken)
    {
        return FromResult(await _productService.AddAsync(product, cancellationToken));
    }

    [HttpPut]
    public async Task<IActionResult> Put(Product product, CancellationToken cancellationToken)
    {
        return FromResult(await _productService.UpdateAsync(product, cancellationToken));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Product product, CancellationToken cancellationToken)
    {
        return FromResult(await _productService.DeleteAsync(product, cancellationToken));
    }
}

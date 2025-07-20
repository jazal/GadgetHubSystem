using AutoMapper;
using GadgetHub2.API.DTOs.Product;
using GadgetHub2.API.Models;
using GadgetHub2.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductController(ProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _repo.GetAll();
        var productsDto = _mapper.Map<List<ProductDto>>(products);
        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _repo.GetById(id);
        if (product == null) return NotFound();

        var productDto = _mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        _repo.Add(product);

        var productDto = _mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        var existingProduct = _repo.GetById(id);
        if (existingProduct == null) return NotFound();

        _mapper.Map(dto, existingProduct);
        _repo.Update(existingProduct);

        var productDto = _mapper.Map<ProductDto>(existingProduct);
        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _repo.GetById(id);
        if (product == null) return NotFound();

        _repo.Delete(product);
        return Ok();
    }
}
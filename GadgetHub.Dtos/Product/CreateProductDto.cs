﻿namespace GadgetHub.Dtos.Product;

public class CreateProductDto
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }
}

﻿namespace GadgetHub.Web.Models;

public class ProductViewModel
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public DateTime? CreatedOn { get; set; }
}

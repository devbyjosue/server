using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 
namespace server.Models;

/// <summary>
/// Unit of measure lookup table.
/// </summary>
public partial class UnitMeasure
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public string UnitMeasureCode { get; set; } = null!;

    /// <summary>
    /// Unit of measure description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    [NotMapped]
    public virtual ICollection<BillOfMaterial> BillOfMaterials { get; set; } = new List<BillOfMaterial>();

    [NotMapped]
    public virtual ICollection<Product> ProductSizeUnitMeasureCodeNavigations { get; set; } = new List<Product>();

    [NotMapped]
    public virtual ICollection<ProductVendor> ProductVendors { get; set; } = new List<ProductVendor>();

    [NotMapped]
    public virtual ICollection<Product> ProductWeightUnitMeasureCodeNavigations { get; set; } = new List<Product>();
}

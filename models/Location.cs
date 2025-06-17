using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 

namespace server.Models;

/// <summary>
/// Product inventory and manufacturing locations.
/// </summary>
public partial class Location
{
    /// <summary>
    /// Primary key for Location records.
    /// </summary>
    public short LocationId { get; set; }

    /// <summary>
    /// Location description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Standard hourly cost of the manufacturing location.
    /// </summary>
    public decimal CostRate { get; set; }

    /// <summary>
    /// Work capacity (in hours) of the manufacturing location.
    /// </summary>
    public decimal Availability { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    [NotMapped]
    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    [NotMapped]
    public virtual ICollection<WorkOrderRouting> WorkOrderRoutings { get; set; } = new List<WorkOrderRouting>();
}

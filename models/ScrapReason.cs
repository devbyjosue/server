using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

/// <summary>
/// Manufacturing failure reasons lookup table.
/// </summary>
public partial class ScrapReason
{
    /// <summary>
    /// Primary key for ScrapReason records.
    /// </summary>
    public short ScrapReasonId { get; set; }

    /// <summary>
    /// Failure description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    [NotMapped]
    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

/// <summary>
/// Lookup table containing the languages in which some AdventureWorks data is stored.
/// </summary>
public partial class Culture
{
    /// <summary>
    /// Primary key for Culture records.
    /// </summary>
    public string CultureId { get; set; } = null!;

    /// <summary>
    /// Culture description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    [NotMapped]
    public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; } = new List<ProductModelProductDescriptionCulture>();
}

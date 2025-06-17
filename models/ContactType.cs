using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 

namespace server.Models;

/// <summary>
/// Lookup table containing the types of business entity contacts.
/// </summary>
public partial class ContactType
{
    /// <summary>
    /// Primary key for ContactType records.
    /// </summary>
    public int ContactTypeId { get; set; }

    /// <summary>
    /// Contact type description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    [NotMapped]
    public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new List<BusinessEntityContact>();
}

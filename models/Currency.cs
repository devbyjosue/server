using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema; 

namespace server.Models;

/// <summary>
/// Lookup table containing standard ISO currencies.
/// </summary>
public partial class Currency
{
    /// <summary>
    /// The ISO code for the Currency.
    /// </summary>
    public string CurrencyCode { get; set; } = null!;

    /// <summary>
    /// Currency name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<CountryRegionCurrency> CountryRegionCurrencies { get; set; } = new List<CountryRegionCurrency>();

    [NotMapped]
    public virtual ICollection<CurrencyRate> CurrencyRateFromCurrencyCodeNavigations { get; set; } = new List<CurrencyRate>();

    [NotMapped]
    public virtual ICollection<CurrencyRate> CurrencyRateToCurrencyCodeNavigations { get; set; } = new List<CurrencyRate>();
}

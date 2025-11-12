using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ReadModels;

/// <summary>
/// Aggregated sales information per product (multi-stream projection target).
/// </summary>
public class ProductSales
{
    public Guid Id { get; set; }                     
    public string ProductName { get; set; } = string.Empty;
    public long TotalQuantitySold { get; set; }
    public DateTime? LastSaleAt { get; set; }
}

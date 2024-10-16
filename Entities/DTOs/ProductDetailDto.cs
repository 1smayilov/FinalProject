using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    // DTO - Bir e-ticaret saytına girəndə ürünün listesinde aslında ilişkisel tablolardakı dataları da görüyorsun
    // yani ürünün ismi de yazıyor ama yanında categori ismi de yaziyor bizə də bu lazımdır(join)
    public class ProductDetailDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }
    }
}

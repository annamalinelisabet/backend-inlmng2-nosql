using System.ComponentModel.DataAnnotations;

namespace CosmosDB.Models
{
    public class Product    {

        public string ArtNo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;

    }
}

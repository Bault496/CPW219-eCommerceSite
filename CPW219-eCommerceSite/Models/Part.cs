using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents a single part for purchase
    /// </summary>
    public class Part
    {
        /// <summary>
        /// Unique identifier for each computer part
        /// </summary>
        [Key]
        public int PartId { get; set; }

        /// <summary>
        /// Name of the computer part
        /// </summary
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Sales price of the computer part
        /// </summary>
        [Range(0, 10000)]
        public double Price { get; set; }

    }
}

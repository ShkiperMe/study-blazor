using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fridge.Shared
{
    public class Indicator
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required]
        public float Value { get; set; }

        public int FridgeId { get; set; }
    }
}
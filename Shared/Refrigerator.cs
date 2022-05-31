using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fridge.Shared
{
    public class Refrigerator
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public bool Deleted { get; set; } = false;

        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;

        public List<Indicator> Indicators { get; set; }
        public Refrigerator()
        {
            Indicators = new List<Indicator>();
        }
    }
}
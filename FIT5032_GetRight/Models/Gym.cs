namespace FIT5032_GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Gym")]
    public partial class Gym
    {
        public int GymId { get; set; }

        [Required]
        [StringLength(50)]
        public string GymName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Longitude { get; set; }
    }
}

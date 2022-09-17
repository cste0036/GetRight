namespace FIT5032_GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trainer")]
    public partial class Trainer
    {
        public int TrainerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Tags { get; set; }

        [Required]
        public string Description { get; set; }

        public int GymId { get; set; }

        public virtual Gym Gym { get; set; }
    }
}

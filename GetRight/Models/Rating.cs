namespace GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        public int Id { get; set; }

        public int DieterId { get; set; }

        public int TrainerId { get; set; }

        [Column("rating")]
        public int rating1 { get; set; }

        public string TrainerName { get; set; }
    }
}

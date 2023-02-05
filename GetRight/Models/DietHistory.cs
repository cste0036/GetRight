namespace GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DietHistory")]
    public partial class DietHistory
    {
        [Key]
        [Column(TypeName = "date")]
        public DateTime RecordDate { get; set; }

        public double Weight { get; set; }

        public int DieterId { get; set; }

        public virtual Dieter Dieter { get; set; }
    }
}

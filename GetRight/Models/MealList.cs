namespace GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MealList")]
    public partial class MealList
    {
        [Key]
        public int MealId { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MealDate { get; set; }

        [Required]
        [StringLength(25)]
        public string MealName { get; set; }

        public int KiloJoule { get; set; }

        public int DieterId { get; set; }

        public virtual Dieter Dieter { get; set; }
    }
}

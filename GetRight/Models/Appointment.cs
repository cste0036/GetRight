namespace GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class Appointment
    {
        [Key]
        [Column(Order = 0)]
        public int AppointmentId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime AppDate { get; set; }

        public int Length { get; set; }

        public int DieterId { get; set; }

        public int TrainerId { get; set; }

        public virtual Dieter Dieter { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}

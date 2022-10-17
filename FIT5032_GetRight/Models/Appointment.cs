namespace FIT5032_GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class Appointment
    {
        public int AppointmentId { get; set; }

        public DateTime AppDate { get; set; }

        public int Length { get; set; }

        public int DieterId { get; set; }

        public int TrainerId { get; set; }
    }
}

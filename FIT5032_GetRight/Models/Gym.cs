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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gym()
        {
            Trainers = new HashSet<Trainer>();
        }

        public int GymId { get; set; }

        [Required]
        [StringLength(50)]
        public string GymName { get; set; }

        public int Longitude { get; set; }

        public int Latitude { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}

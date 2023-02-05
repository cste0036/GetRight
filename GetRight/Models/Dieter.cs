namespace GetRight.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dieter")]
    public partial class Dieter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dieter()
        {
            Appointments = new HashSet<Appointment>();
            DietHistories = new HashSet<DietHistory>();
            MealLists = new HashSet<MealList>();
        }

        public int DieterId { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be less than {2} characters long.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be less than {2} characters long.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be less than {2} characters long.")]
        public string UserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DietHistory> DietHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MealList> MealLists { get; set; }
    }
}

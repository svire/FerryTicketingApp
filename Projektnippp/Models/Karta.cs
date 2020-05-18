namespace Projektnippp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Karta")]
    public partial class Karta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Karta()
        {
            Racuns = new HashSet<Racun>();
        }

        public int KartaId { get; set; }

        public int? Putnik { get; set; }

        public int? Voznja { get; set; }
        [Required(ErrorMessage = "Unesite broj sjedista:")]
        public int? Sjediste_Broj { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite status:")]
        public string Status { get; set; }

        public virtual Putnik Putnik1 { get; set; }

        public virtual Voznja Voznja1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Racun> Racuns { get; set; }
    }
}

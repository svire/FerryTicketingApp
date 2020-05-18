namespace Projektnippp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brod")]
    public partial class Brod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brod()
        {
            Voznjas = new HashSet<Voznja>();
        }

        public int BrodId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Unesite naziv broda:")]
        public string Naziv { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite tip broda:")]
        public string Tip { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite registraciju broda:")]
        public string Registracija { get; set; }
        [Required(ErrorMessage = "Unesite broj sjedista:")]
        public int? Brojsjedista { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite status broda:")]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voznja> Voznjas { get; set; }
    }
}

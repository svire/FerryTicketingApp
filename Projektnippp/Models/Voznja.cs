namespace Projektnippp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voznja")]
    public partial class Voznja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voznja()
        {
            Kartas = new HashSet<Karta>();
        }

        public int VoznjaId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite skracenicu(npr. BG-WIEN):")]
        public string Skracenica { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite polaziste:")]
        public string Polaziste { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite dolaziste:")]
        public string Dolaziste { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Unesite vrijeme polaska:")]
        public string Vrijeme_polaska { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Unesite datum polska:")]
        public string Datum_polaska { get; set; }

       
        public int? BrodId { get; set; }
        [Required(ErrorMessage = "Unesite slobodna mjesta:")]
        public int? Slobodna_mjesta { get; set; }
        
        public decimal? Cijena { get; set; }

        public virtual Brod Brod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Karta> Kartas { get; set; }
    }
}

namespace Projektnippp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Putnik")]
    public partial class Putnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Putnik()
        {
            Kartas = new HashSet<Karta>();
        }

        public int PutnikId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite ime i prezime:")]
        public string ImePrezime { get; set; }

        [Required(ErrorMessage = "Unesite godine:")]
        public int? Godine { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite mjesto stanovanja:")]
        public string MjestoStanovanja { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite adresu:")]
        public string Adresa { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite telefon:")]
        public string Telefon { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Unesite email:")]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Karta> Kartas { get; set; }
    }
}

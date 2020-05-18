namespace Projektnippp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Racun")]
    public partial class Racun
    {
        public int RacunId { get; set; }

        public int? KartaId { get; set; }

        public decimal? Iznos { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string DatumVrijeme { get; set; }

        public virtual Karta Karta { get; set; }
    }
}

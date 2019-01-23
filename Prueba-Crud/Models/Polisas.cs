using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Crud.Models
{
    public class Polisas
    {
        public int id { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string name { get; set; }

        [Column(TypeName ="VARCHAR(250)")]
        public string description { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string typeCoverings { get; set; }

        [Required]
        public double porcentage { get; set; }

        public DateTime dateInitial { get; set; }

        public DateTime dateFinal { get; set; }

        public double price { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string typeRisk { get; set; }


    }
}

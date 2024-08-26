using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entites
{
    public class Invoice : BaseEntity
    {
        public int Patient_ID { get; set; }
        public Patient Patient { get; set; }
        public string? Service_Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Total { get; set; }
    }
}

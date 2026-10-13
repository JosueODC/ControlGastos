using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
namespace ControlGastos.Models
{
    public class Movimiento
    {
        public int id{get;set;}
        [Required]
        public string Tipo {get;set;}="Gasto";
        [Required]
        public string Descripcion {get; set;}="";
        [Range (0.01,9999999999)]
        public decimal Cantidad {get;set;}
        [Required]
        public string Categoria {get;set;} = "";
        [DataType(DataType.Date)]
        public DateTime Fecha {get;set;} = DateTime.Today;
    }
}
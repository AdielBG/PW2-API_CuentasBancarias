using System.ComponentModel.DataAnnotations;

namespace API_CuentasBancarias.Models
{
    public class CuentaBancaria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroCuenta { get; set; }

        [Required]
        [StringLength(100)]
        public string Titular { get; set; }

        [Required]
        public decimal Saldo { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoCuenta { get; set; } // "Ahorro" o "Corriente"

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public bool Activa { get; set; } = true;
    }
}
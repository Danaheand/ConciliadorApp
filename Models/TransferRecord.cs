namespace ConciliadorApp.Models
{
    public class TransferRecord
    {
        public string CuentaOrigen { get; set; } = string.Empty;
        public string CuentaDestino { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string NumeroIdentificacionBeneficiario { get; set; } = string.Empty;
        public string NombreBeneficiario { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}
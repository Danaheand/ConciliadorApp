using ConciliadorApp.Models;
using ConciliadorApp.Utils;
using System.Globalization;

namespace ConciliadorApp.Services
{
    public class FileReaderService
    {
        public List<TransferRecord> ReadFile(string path)
        {
            ValidateFile(path);

            var lines = File.ReadAllLines(path);

            if (lines.Length < 2)
                throw new Exception("El archivo no contiene registros.");

            ValidateHeader(lines[0]);

            var records = new List<TransferRecord>();

            for (int i = 1; i < lines.Length; i++)
            {
                var record = ParseLine(lines[i], i + 1);
                records.Add(record);
            }

            return records;
        }

        private void ValidateFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("El archivo no existe.");

            var extension = Path.GetExtension(path).ToLower();

            if (extension != ".csv" && extension != ".txt")
                throw new Exception("Formato inválido. Solo se permiten archivos .csv o .txt.");
        }

        private void ValidateHeader(string headerLine)
        {
            var headers = headerLine.Split(',');

            if (headers.Length != Constants.ExpectedHeaders.Length)
                throw new Exception("Número incorrecto de columnas.");

            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Trim().ToLower() != Constants.ExpectedHeaders[i])
                    throw new Exception($"Columna incorrecta en posición {i + 1}.");
            }
        }

        private TransferRecord ParseLine(string line, int lineNumber)
        {
            var columns = line.Split(',');

            if (columns.Length != 7)
                throw new Exception($"Número incorrecto de columnas en la línea {lineNumber}.");

            if (!decimal.TryParse(columns[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal monto))
                throw new Exception($"Monto inválido en la línea {lineNumber}.");

            if (!DateTime.TryParseExact(
    columns[6].Trim(),
    "M/d/yyyy",
    CultureInfo.InvariantCulture,
    DateTimeStyles.None,
    out DateTime fecha))
            {
                throw new Exception($"Fecha inválida en la línea {lineNumber}.");
            }

            if (columns[4].Length < 10)
                throw new Exception($"Número de identificación inválido en la línea {lineNumber}.");

            return new TransferRecord
            {
                CuentaOrigen = columns[0],
                CuentaDestino = columns[1],
                Monto = monto,
                Motivo = columns[3],
                NumeroIdentificacionBeneficiario = columns[4],
                NombreBeneficiario = columns[5],
                FechaRegistro = fecha
            };
        }
    }
}
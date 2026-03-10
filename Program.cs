using ConciliadorApp.Services;

var fileReader = new FileReaderService();
var reconciliationService = new ReconciliationService();
var logService = new LogService();

try
{
    logService.Log("Inicio del proceso de conciliación");

    Console.WriteLine("Ingrese la ruta del archivo de BanRed:");
    var banRedPath = Console.ReadLine();
    logService.Log($"Ruta BanRed ingresada: {banRedPath}");

    Console.WriteLine("Ingrese la ruta del archivo de ProCredit:");
    var proCreditPath = Console.ReadLine();
    logService.Log($"Ruta ProCredit ingresada: {proCreditPath}");

    var banRedRecords = fileReader.ReadFile(banRedPath!);
    logService.Log($"Archivo BanRed leído. Registros: {banRedRecords.Count}");

    var proCreditRecords = fileReader.ReadFile(proCreditPath!);
    logService.Log($"Archivo ProCredit leído. Registros: {proCreditRecords.Count}");

    var result = reconciliationService.Compare(banRedRecords, proCreditRecords);
    logService.Log("Comparación realizada");

    Console.WriteLine();
    Console.WriteLine("===== RESUMEN DE CONCILIACIÓN =====");
    Console.WriteLine($"Registros BanRed: {result.BanRedCount}");
    Console.WriteLine($"Registros ProCredit: {result.ProCreditCount}");
    Console.WriteLine($"Suma BanRed: {result.BanRedTotalAmount}");
    Console.WriteLine($"Suma ProCredit: {result.ProCreditTotalAmount}");
    Console.WriteLine($"Cantidad de registros coincide: {result.IsRecordCountEqual}");
    Console.WriteLine($"Sumatoria de montos coincide: {result.IsTotalAmountEqual}");
    Console.WriteLine($"Estado final: {(result.IsSuccessful ? "CONCILIACIÓN EXITOSA" : "INCONSISTENCIAS DETECTADAS")}");

    logService.Log($"Resultado final: {(result.IsSuccessful ? "EXITOSA" : "INCONSISTENCIAS")}");
}
catch (Exception ex)
{
    logService.Log($"Error: {ex.Message}");
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    logService.Log("Fin del proceso de conciliación");
}
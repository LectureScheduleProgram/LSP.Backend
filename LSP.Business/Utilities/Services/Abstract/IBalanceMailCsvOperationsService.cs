namespace LSP.Business.Utilities.Services.Abstract
{
    public interface IBalanceMailCsvOperationsService
    {
        void BalanceMailCsvCreate();
        void AddnewLine(string coinCode, string subnetWorkCode, string date, string requestedAmount);
        string GetCsvFile(string coinCode, string subnetWorkCode, string date, string requestedAmount);
    }
}

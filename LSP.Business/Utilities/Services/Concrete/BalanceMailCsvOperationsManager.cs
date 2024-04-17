using LSP.Business.Abstract;
using LSP.Business.Utilities.Services.Abstract;

namespace LSP.Business.Utilities.Services.Concrete
{
    public class BalanceMailCsvOperationsManager : IBalanceMailCsvOperationsService
    {
        private readonly IMailService _mailService;

        public BalanceMailCsvOperationsManager(IMailService mailService)
        {
            _mailService = mailService;
        }

        public void BalanceMailCsvCreate()
        {
            string filePath = "wwwroot/BalanceCheck/company-balance-check.csv";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Coin_Code,Subnetwork_Code,Date,Requested_Amount");
            }
        }
        public void AddnewLine(string coinCode, string subnetWorkCode, string date, string requestedAmount)
        {
            string filePath = "wwwroot/BalanceCheck/company-balance-check.csv";
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{coinCode},{subnetWorkCode},{date},{requestedAmount}");
            }
        }
        public string GetCsvFile(string coinCode, string subnetWorkCode, string date, string requestedAmount)
        {
            try
            {
                string filePath = "wwwroot/BalanceCheck/company-balance-check.csv";
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (fields[0] == coinCode)
                    {
                        if (fields[1] == subnetWorkCode)
                        {
                            if (Convert.ToDateTime(fields[2]) <= DateTime.UtcNow.AddMinutes(-15))
                            {
                                lines[i] = $"{fields[0]},{fields[1]},{Convert.ToString(DateTime.UtcNow)},{requestedAmount}";
                                File.WriteAllLines(filePath, lines);
                                _mailService.WithdrawBalanceInfo($"Coin Name: <strong>{fields[0]}</strong><br></br>Subnetwork Name: <strong>{fields[1]}</strong><br></br>DateTime : <strong>{Convert.ToString(DateTime.UtcNow)} (UTC)</strong><br></br>Withdraw Amount : <strong>{requestedAmount}</strong>");

                            }
                            return "";
                        }
                    }
                }
                AddnewLine(coinCode, subnetWorkCode, date, requestedAmount);
                _mailService.WithdrawBalanceInfo($"Coin Name: <strong>{coinCode}</strong><br></br>Subnetwork Name: <strong>{subnetWorkCode}</strong><br></br>DateTime : <strong>{date} (UTC)</strong><br></br>Withdraw Amount : <strong>{requestedAmount}</strong>");
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

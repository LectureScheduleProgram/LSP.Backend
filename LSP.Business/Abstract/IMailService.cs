namespace LSP.Business.Abstract
{
    public interface IMailService
    {
        void Send(string email, string message, string subject);
        bool WithdrawBalanceInfo(string info);
    }
}

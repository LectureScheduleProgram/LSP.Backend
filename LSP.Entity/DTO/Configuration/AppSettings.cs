using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Entity.DTO.Configuration
{
    public class AppSettings
    {
        public SocketSettings SocketSettings { get; set; }
        public SecuritySettings SecuritySettings { get; set; }
        public MailSettings MailSettings { get; set; }
        public WalletSettings WalletSettings { get; set; }
        public CompanySettings CompanySettings { get; set; }
    }
    public class SocketSettings
    {
        public string IP { get; set; }
    }
    public class SecuritySettings
    {
        public string Google2faKey { get; set; }
    }
    public class CompanySettings
    {
        public string Name { get; set; }
    }
    public class MailSettings
    {
        public string EmailHost { get; set; } //
        public int EmailPort { get; set; } //
        public string EmailSmtpUserName { get; set; } //
        public string EmailSmtpPassword { get; set; } //
        public string EmailFromAddress { get; set; } //
        public string EmailFromTitle { get; set; } //
    }
    public class WalletSettings
    {
        public string CreateWalletUrl { get; set; }
        public string WalletControlUrl { get; set; }
        public TronSettings TronSettings { get; set; }
        public EthSettings EthSettings { get; set; }
        public BtcSettings BtcSettings { get; set; }
        public DogeSettings DogeSettings { get; set; }
        public XrpSettings XrpSettings { get; set; }
        public XmlSettings XmlSettings { get; set; }
    }

    public class TronSettings
    {
        public string TronMainWalletAddress { get; set; }
        public string TronMainWalletPrivateKey { get; set; }
        public string TrxSendNodeJsApiUrl { get; set; }
        public string Trc10SendNodeJsApiUrl { get; set; }
        public string Trc20SendNodeJsApiUrl { get; set; }
    }

    public class EthSettings
    {
        public string EthMainWalletAddress { get; set; }
        public string EthMainWalletPrivateKey { get; set; }
        public string EthInfuraUrl { get; set; }
    }
    public class BtcSettings
    {
        public string BtcDeamonUrl { get; set; }
        public string BtcRpcUserName { get; set; }
        public string BtcRpcPassword { get; set; }
        public string BtcWalletPassword { get; set; }
        public short BtcRpcRequestTimeoutInSeconds { get; set; }
        public string BtcMainWalletAddress { get; set; }
    }
    public class DogeSettings
    {
        public string DogeDeamonUrl { get; set; }
        public string DogeRpcUserName { get; set; }
        public string DogeRpcPassword { get; set; }
        public string DogeWalletPassword { get; set; }
        public short DogeRpcRequestTimeoutInSeconds { get; set; }
        public string DogeMainWalletAddress { get; set; }
    }
    public class XrpSettings
    {
        public string XRP_TestWalletSeed { get; set; }
        public string XRP_MainSiteWebSocketUrl { get; set; }
    }
    public class XmlSettings
    {
        public string XLM_TestSecretKey { get; set; }
        public string XLM_MainSiteWebSocketUrl { get; set; }
        public string XLM_Publickey { get; set; }
        public string XLM_urlFromApi { get; set; }
    }
}

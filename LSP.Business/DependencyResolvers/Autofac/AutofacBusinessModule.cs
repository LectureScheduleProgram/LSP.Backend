using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using LSP.Business.Abstract;
using LSP.Business.Concrete;
using LSP.Business.Utilities.Services.Abstract;
using LSP.Business.Utilities.Services.Concrete;
using LSP.Core.Security;
using LSP.Core.Utilities.Interceptors;
using LSP.Dal.Abstract;
using LSP.Dal.Concrete.EntityFramework;
using LSP.Entity.DTO.MailSmsDtos;

namespace LSP.Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterType<CurrenciesManager>().As<ICurrenciesService>();
        builder.RegisterType<EfCurrencyDal>().As<ICurrencyDal>();

        //builder.RegisterType<SubNetworksManager>().As<ISubNetworksService>();
        builder.RegisterType<EfSubNetworkDal>().As<ISubNetworkDal>();

        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<CloudManager>().As<ICloudService>();
        //builder.RegisterType<UserBalanciesManager>().As<IUserBalanciesService>();
        builder.RegisterType<EfBalanceDal>().As<IBalanceDal>();

        //builder.RegisterType<BuyOrdersManager>().As<IBuyOrdersService>();
        builder.RegisterType<EfBuyOrderDal>().As<IBuyOrderDal>();

        //builder.RegisterType<ParitiesManager>().As<IParitiesService>();
        builder.RegisterType<EfParityDal>().As<IParityDal>();

        builder.RegisterType<UserSecurityTypeManager>().As<IUserSecurityTypeService>();
        builder.RegisterType<EfUserSecurityTypeDal>().As<IUserSecurityTypeDal>();

        //builder.RegisterType<SellOrdersManager>().As<ISellOrdersService>();
        builder.RegisterType<EfSellOrderDal>().As<ISellOrderDal>();

        //builder.RegisterType<MainNetworksManager>().As<IMainNetworksService>();
        builder.RegisterType<EfMainNetworkDal>().As<IMainNetworkDal>();

        builder.RegisterType<SwapManager>().As<ISwapService>();
        builder.RegisterType<EfSwapDal>().As<ISwapDal>();

        // Token ve Auth servisi için
        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        builder.RegisterType<PasswordHistoryManager>().As<IPasswordHistoryService>();
        builder.RegisterType<EfPasswordHistoryDal>().As<IPasswordHistoryDal>();

        builder.RegisterType<SecurityHistoryManager>().As<ISecurityHistoryService>();
        builder.RegisterType<EfSecurityHistoryDal>().As<ISecurityHistoryDal>();

        builder.RegisterType<UserStatusHistoryManager>().As<IUserStatusHistoryService>();
        builder.RegisterType<EfUserStatusHistoriesDal>().As<IUserStatusHistoryDal>();

        builder.RegisterType<MailManager>().As<IMailService>();

        builder.RegisterType<ValidationRuleManager>().As<IValidationService>();

        //Kyc
        builder.RegisterType<EfKycDal>().As<IKycDal>();
        builder.RegisterType<KycManager>().As<IKycService>();


        builder.RegisterType<WalletManager>().As<IWalletService>();
        builder.RegisterType<EfWalletDal>().As<IWalletDal>();

        builder.RegisterType<AfkManager>().As<IAfkService>();
        builder.RegisterType<EfAfkDal>().As<IAfkDal>();

        builder.RegisterType<EfDepositDal>().As<IDepositDal>();
        builder.RegisterType<EfWithdrawalDal>().As<IWithdrawalDal>();
        builder.RegisterType<TransactionManager>().As<ITransactionService>();

        builder.RegisterType<DepositManager>().As<IDepositService>();
        builder.RegisterType<WithdrawalManager>().As<IWithdrawalService>();

        builder.RegisterType<AccessControlManager>().As<IAccessControlService>();

        builder.RegisterType<EfBuyLongOrderDal>().As<IBuyLongOrderDal>();
        builder.RegisterType<EfSellShortOrderDal>().As<ISellShortOrderDal>();
        builder.RegisterType<FutureOrderManager>().As<IFutureOrderService>();
        //builder.RegisterType<ServiceResponseFactory>().As<IServiceResponseFactory>().InstancePerDependency();

        builder.RegisterType<EfFutureBalanceDal>().As<IFutureBalanceDal>();
        builder.RegisterType<FutureBalanceManager>().As<IFutureBalanceService>();

        builder.RegisterType<CoinWithdrawManager>().As<ICoinWithdrawService>();
        builder.RegisterType<BalanceMailCsvOperationsManager>().As<IBalanceMailCsvOperationsService>();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}
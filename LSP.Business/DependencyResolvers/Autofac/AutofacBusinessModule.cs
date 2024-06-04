using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using LSP.Business.Abstract;
using LSP.Business.Abstract.Operations;
using LSP.Business.Concrete;
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
        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<CloudManager>().As<ICloudService>();

        builder.RegisterType<UserSecurityTypeManager>().As<IUserSecurityTypeService>();
        builder.RegisterType<EfUserSecurityTypeDal>().As<IUserSecurityTypeDal>();

        // Token ve Auth servisi için
        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        builder.RegisterType<ClassroomManager>().As<IClassroomService>();
        builder.RegisterType<EfClassroomDal>().As<IClassroomDal>();

        builder.RegisterType<LectureManager>().As<ILectureService>();
        builder.RegisterType<EfLectureDal>().As<ILectureDal>();

        builder.RegisterType<FacultyManager>().As<IFacultyService>();
        builder.RegisterType<EfFacultyDal>().As<IFacultyDal>();

        builder.RegisterType<DepartmentManager>().As<IDepartmentService>();
        builder.RegisterType<EfDepartmentDal>().As<IDepartmentDal>();

        builder.RegisterType<ClassroomCapacityManager>().As<IClassroomCapacityService>();
        builder.RegisterType<EfClassroomCapacityDal>().As<IClassroomCapacityDal>();

        builder.RegisterType<ClassroomTypeManager>().As<IClassroomTypeService>();
        builder.RegisterType<EfClassroomTypeDal>().As<IClassroomTypeDal>();

        builder.RegisterType<ScheduleRecordManager>().As<IScheduleRecordService>();
        builder.RegisterType<EfScheduleRecordDal>().As<IScheduleRecordDal>();

        builder.RegisterType<PasswordHistoryManager>().As<IPasswordHistoryService>();
        builder.RegisterType<EfPasswordHistoryDal>().As<IPasswordHistoryDal>();

        builder.RegisterType<SecurityHistoryManager>().As<ISecurityHistoryService>();
        builder.RegisterType<EfSecurityHistoryDal>().As<ISecurityHistoryDal>();

        builder.RegisterType<UserStatusHistoryManager>().As<IUserStatusHistoryService>();
        builder.RegisterType<EfUserStatusHistoriesDal>().As<IUserStatusHistoryDal>();

        builder.RegisterType<MailManager>().As<IMailService>();

        builder.RegisterType<ValidationRuleManager>().As<IValidationService>();

        builder.RegisterType<AccessControlManager>().As<IAccessControlService>();

        builder.RegisterType<DashboardManager>().As<IDashboardService>();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}
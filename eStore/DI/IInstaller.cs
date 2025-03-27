namespace eStore.DI
{
    public interface IInstaller
    {
        void InstallService(IServiceCollection services, IConfiguration configuration);
    }
}

namespace Schema.Core.Services
{
    public interface IConfigService
    {
        string GetAppSetting(string name);
        string GetConnectionString(string name);
    }
}

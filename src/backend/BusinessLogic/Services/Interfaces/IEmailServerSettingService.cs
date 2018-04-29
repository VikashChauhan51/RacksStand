using Domain.Core;

namespace Services.Interfaces
{
    public interface IEmailServerSettingService
    {
        EmailSetting GetServerSetting();
    }
}

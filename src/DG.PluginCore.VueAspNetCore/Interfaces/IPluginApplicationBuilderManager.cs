namespace PluginCore.AspNetCore.Interfaces
{
    public interface IPluginApplicationBuilderManager
    {
        void ReBuild();

        RequestDelegate GetBuildResult();
    }
}

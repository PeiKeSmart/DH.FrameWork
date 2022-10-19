using Microsoft.AspNetCore.Authorization;

namespace PluginCore.AspNetCore.Authorization
{
    /// <summary>
    /// 注意: PluginCoreAdmin -> PluginCore.Admin
    /// </summary>
    public class PluginCoreAdminAuthorizeAttribute : AuthorizeAttribute
    {
        public PluginCoreAdminAuthorizeAttribute() : base("PluginCore.Admin")
        {

        }
    }
}

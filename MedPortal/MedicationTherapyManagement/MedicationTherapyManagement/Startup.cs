using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicationTherapyManagement.Startup))]
namespace MedicationTherapyManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using IntroSkip.Configuration;
using Zenject;

namespace IntroSkip.Installers
{
    internal class IntroSkipInstaller : Installer
    {
        public override void InstallBindings()
        {
            if (!Container.Resolve<IntroSkipConfig>().Enabled) return;
            Container.BindInterfacesAndSelfTo<IntroSkipController>().AsSingle();
        }
    }
}

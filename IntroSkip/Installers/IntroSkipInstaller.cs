using IntroSkip.Configuration;
using Zenject;

namespace IntroSkip.Installers
{
    internal class IntroSkipInstaller : Installer
    {
        [Inject] IntroSkipConfig _config;
        public override void InstallBindings()
        {
            if (!_config.Enabled) return;
            Container.BindInterfacesAndSelfTo<IntroSkipController>().AsSingle();
        }
    }
}

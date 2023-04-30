using IntroSkip.Configuration;
using Zenject;

namespace IntroSkip.Installers
{
    internal class IntroSkipInstaller : Installer
    {
        public override void InstallBindings()
        {
            if (!PluginConfig.Instance.Enabled) return;
            Container.BindInterfacesAndSelfTo<IntroSkipController>().AsSingle();
        }
    }
}

using IntroSkip.UI;
using Zenject;

namespace IntroSkip.Installers
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IntroSkipSettingsViewController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<IntroSkipSettingsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}

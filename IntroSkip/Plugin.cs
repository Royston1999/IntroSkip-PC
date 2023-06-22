using IntroSkip.Configuration;
using IntroSkip.Installers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace IntroSkip
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger, Config config, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            IntroSkipConfig myConfig = config.Generated<IntroSkipConfig>();
            zenjector.Expose<CoreGameHUDController>("Environment");
            zenjector.Install<IntroSkipInstaller>(Location.StandardPlayer);
            zenjector.Install<MenuInstaller>(Location.Menu);
            zenjector.Install(Location.App, container => container.BindInstance(myConfig));
            Log.Info("IntroSkip initialized.");
        }
    }
}

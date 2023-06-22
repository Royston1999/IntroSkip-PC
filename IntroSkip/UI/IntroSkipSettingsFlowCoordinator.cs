using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using Zenject;

namespace IntroSkip.UI
{
    internal class IntroSkipSettingsFlowCoordinator : FlowCoordinator, IInitializable
    {
        private IntroSkipSettingsViewController _viewController;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation) return;
            SetTitle("Intro Skip");
            showBackButton = true;
            ProvideInitialViewControllers(_viewController);
        }

        [Inject]
        public void Construct(IntroSkipSettingsViewController viewController) => _viewController = viewController;
        

        public void Present() => BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this);
        

        protected override void BackButtonWasPressed(ViewController _) => BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        

        public void Initialize()
        {
            MenuButtons.instance.RegisterButton(new MenuButton("Intro Skip", "skip those long intros!", Present));
        }
    }
}

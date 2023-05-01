using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using IntroSkip.Configuration;
using System;
using Zenject;

namespace IntroSkip.UI
{
    [ViewDefinition("IntroSkip.UI.IntroSkipSettingsViewController.bsml")]
    internal class IntroSkipSettingsViewController : BSMLAutomaticViewController
    {
        [Inject] private IntroSkipConfig _config;

        [UIValue("enabled")]
        private bool Enabled
        {
            get => _config.Enabled;
            set => _config.Enabled = value;
        }

        [UIValue("skip_intro")]
        private bool SkipIntro
        {
            get => _config.SkipIntro;
            set => _config.SkipIntro = value;
        }

        [UIValue("skip_middle")]
        private bool SkipMiddle
        {
            get => _config.SkipMiddle;
            set => _config.SkipMiddle = value;
        }

        [UIValue("skip_outro")]
        private bool SkipOutro
        {
            get => _config.SkipOutro;
            set => _config.SkipOutro = value;
        }

        [UIValue("both_pressed")]
        private bool BothTriggers
        {
            get => _config.BothTriggers;
            set => _config.BothTriggers = value;
        }

        [UIValue("min_skip_time")]
        private float MinSkipTime
        {
            get => _config.MinSkipTime;
            set => _config.MinSkipTime = value;
        }

        [UIValue("min_hold_time")]
        private float MinHoldTime
        {
            get => _config.MinHoldTime;
            set => _config.MinHoldTime = value;
        }

        [UIAction("min_time_format")]
        private String FormatSlider(float value) => value.ToString("0.0") + "s";
    }
}

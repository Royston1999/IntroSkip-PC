using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using IntroSkip.Configuration;
using System;

namespace IntroSkip.UI
{
    [ViewDefinition("IntroSkip.UI.IntroSkipSettingsViewController.bsml")]
    internal class IntroSkipSettingsViewController : BSMLAutomaticViewController
    { 

        [UIValue("enabled")]
        private bool Enabled
        {
            get => PluginConfig.Instance.Enabled;
            set => PluginConfig.Instance.Enabled = value;
        }

        [UIValue("skip_intro")]
        private bool SkipIntro
        {
            get => PluginConfig.Instance.SkipIntro;
            set => PluginConfig.Instance.SkipIntro = value;
        }

        [UIValue("skip_middle")]
        private bool SkipMiddle
        {
            get => PluginConfig.Instance.SkipMiddle;
            set => PluginConfig.Instance.SkipMiddle = value;
        }

        [UIValue("skip_outro")]
        private bool SkipOutro
        {
            get => PluginConfig.Instance.SkipOutro;
            set => PluginConfig.Instance.SkipOutro = value;
        }

        [UIValue("min_skip_time")]
        private float MinSkipTime
        {
            get => PluginConfig.Instance.MinSkipTime;
            set => PluginConfig.Instance.MinSkipTime = value;
        }

        [UIValue("min_hold_time")]
        private float MinHoldTime
        {
            get => PluginConfig.Instance.MinHoldTime;
            set => PluginConfig.Instance.MinHoldTime = value;
        }

        [UIAction("min_time_format")]
        private String FormatSlider(float value) => value.ToString("0.0") + "s";
    }
}

using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace IntroSkip.Configuration
{
    internal class IntroSkipConfig
    {
        public virtual bool Enabled { get; set; } = true;
        public virtual bool SkipIntro { get; set; } = true;
        public virtual bool SkipMiddle { get; set; } = true;
        public virtual bool SkipOutro { get; set; } = true;
        public virtual bool BothTriggers { get; set; } = true;
        public virtual float MinSkipTime { get; set; } = 2.5f;
        public virtual float MinHoldTime { get; set; } = 0f;
    }
}

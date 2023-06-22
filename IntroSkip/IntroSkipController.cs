using IPA.Utilities;
using TMPro;
using UnityEngine;
using Zenject;
using IntroSkip.Utils;
using SkipTimePairs = System.Collections.Generic.List<System.Tuple<float, float>>;
using IntroSkip.Configuration;

namespace IntroSkip
{
    public class IntroSkipController : IInitializable, ITickable
    {
        [Inject] private IntroSkipConfig _config;
        [Inject] private AudioTimeSyncController _audioTimeSyncController;
        [Inject] private IDifficultyBeatmap _difficultyBeatmap;
        [Inject] private IReadonlyBeatmapData _readonlyBeatmapData;
        [Inject] private PauseMenuManager _pauseMenuManager;
        [Inject] private CoreGameHUDController _coreGameHudController;
        private VRController _leftController;
        private VRController _rightController;
        private AudioSource _audioSource;
        private TextMeshProUGUI _skipText;
        private SkipTimePairs _skipTimePairs;
        private SkipTimePairs.Enumerator _skipItr;
        private float _requiredHoldTime;
        private float _timeHeld;
        private bool _requiresBothTriggers;
        private bool _iterable;

        public void Initialize()
        {
            _leftController = _pauseMenuManager.transform.Find("MenuControllers/ControllerLeft").GetComponent<VRController>();
            _rightController = _pauseMenuManager.transform.Find("MenuControllers/ControllerRight").GetComponent<VRController>();
            _audioSource = _audioTimeSyncController.GetField<AudioSource, AudioTimeSyncController>("_audioSource");

            _skipTimePairs = IntroSkipUtils.CreateSkipTimePairs(_readonlyBeatmapData, _difficultyBeatmap.level.songDuration, _config);
            _skipText = IntroSkipUtils.CreateSkipText(_coreGameHudController);
            _requiredHoldTime = _config.MinHoldTime;
            _requiresBothTriggers = _config.BothTriggers;
            _skipItr = _skipTimePairs.GetEnumerator();
            IterateToNextPair();
        }

        public void Tick()
        {
            if (!_iterable) return;

            float currentTime = _audioTimeSyncController.songTime;
            float bufferedCurrentTime = currentTime + 10 * Time.deltaTime;
            bool triggersPressed = TriggeresPressed();
            bool notPaused = _audioTimeSyncController.state == AudioTimeSyncController.State.Playing;
            float skipStart = _skipItr.Current.Item1;
            float skipEnd = _skipItr.Current.Item2;

            if (skipEnd <= bufferedCurrentTime)
            {
                IterateToNextPair(); // passed end of skippable range
                return;
            }
            else if (skipStart >= currentTime) return; // not yet reached next skippable point
            else SetSkipText(true); // woo skippable
            if (triggersPressed && notPaused && _timeHeld >= _requiredHoldTime) _audioSource.time = skipEnd; // skip to the end of the range
            if (triggersPressed) _timeHeld += Time.deltaTime; // increase time held
            else if (_timeHeld > 0) _timeHeld = 0; // reset if no longer holding triggers
        }

        void SetSkipText(bool value)
        {
            if (_skipText != null && _skipText.gameObject.activeSelf != value) _skipText.gameObject.SetActive(value);
        }

        void IterateToNextPair()
        {
            SetSkipText(false);
            _timeHeld = 0f;
            _iterable = _skipItr.MoveNext();
        }

        bool TriggeresPressed()
        {
            bool leftPressed = _leftController.triggerValue > 0.85f;
            bool rightPressed = _rightController.triggerValue > 0.85f;

            return _requiresBothTriggers ? leftPressed && rightPressed : leftPressed || rightPressed;
        }
    }
}

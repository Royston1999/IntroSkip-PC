using IntroSkip.Configuration;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using SkipTimePairs = System.Collections.Generic.List<System.Tuple<float, float>>;
using Pair = System.Tuple<float, float>;

namespace IntroSkip.Utils
{
    internal class IntroSkipUtils
    {
        public static SkipTimePairs CreateSkipTimePairs(IReadonlyBeatmapData mapData, float songLength, IntroSkipConfig config)
        {
            SkipTimePairs skipTimePairs = new SkipTimePairs();
            List<float> mapValues = new List<float>();
            foreach (var noteData in mapData.GetBeatmapDataItems<NoteData>(0))
            {
                if (noteData.scoringType != NoteData.ScoringType.Ignore) mapValues.Add(noteData.time);
            }
            foreach (var sliderData in mapData.GetBeatmapDataItems<SliderData>(0))
            {
                if (sliderData.sliderType == SliderData.Type.Burst)
                {
                    int slices = sliderData.sliceCount;
                    for (int i = 1; i < slices; i++) mapValues.Add(Mathf.LerpUnclamped(sliderData.time, sliderData.tailTime, (float)i / (slices - 1)));
                }
            }
            foreach (var obstacleData in mapData.GetBeatmapDataItems<ObstacleData>(0))
            {
                float startIndex = obstacleData.lineIndex;
                float endIndex = startIndex + obstacleData.width - 1;
                if (startIndex <= 2 && endIndex >= 1)
                {
                    for (int i = 0; i <= obstacleData.duration / 2; i++) mapValues.Add(obstacleData.time + (i * 2));
                    mapValues.Add(obstacleData.time + obstacleData.duration);
                }
            }
            if (!mapValues.Any()) skipTimePairs.Add(new Pair(0.1f, songLength - 0.5f));
            else
            {
                mapValues.Sort();
                float currentTime = mapValues[0];

                if (config.SkipIntro && currentTime > config.MinSkipTime) skipTimePairs.Add(new Pair(0.1f, currentTime - 1.5f));
                if (config.SkipMiddle)
                {
                    foreach (var time in mapValues)
                    {
                        if (time - currentTime > config.MinSkipTime) skipTimePairs.Add(new Pair(currentTime, time - 1.5f));
                        currentTime = time;
                    }
                }
                if (config.SkipOutro && songLength - mapValues.Last() > config.MinSkipTime) skipTimePairs.Add(new Pair(mapValues.Last(), songLength - 0.5f));
            }

            return skipTimePairs;
        }

        public static TextMeshProUGUI CreateSkipText(CoreGameHUDController gameHud)
        {
            var GO = new GameObject();
            GO.AddComponent<Canvas>();
            var text = GO.AddComponent<TextMeshProUGUI>();
            GO.transform.parent = gameHud.transform;

            text.text = "Press Triggers to Skip";
            text.fontSize = 8f;
            text.transform.position = new Vector3(-3.2f, 2.35f, 7f);
            text.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
            text.alignment = TextAlignmentOptions.Center;
            text.gameObject.SetActive(false);
            return text;
        }
    }
}

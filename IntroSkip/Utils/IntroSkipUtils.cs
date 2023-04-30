using IntroSkip.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SkipTimePairs = System.Collections.Generic.List<System.Tuple<float, float>>;

namespace IntroSkip.Utils
{
    internal class IntroSkipUtils
    {
        public static SkipTimePairs CreateSkipTimePairs(IReadonlyBeatmapData mapData, float songLength)
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
                    for (int i = 1; i < slices; i++) mapValues.Add(Mathf.LerpUnclamped(sliderData.time, sliderData.tailTime, i / (slices - 1)));
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
            if (!mapValues.Any()) skipTimePairs.Add(new Tuple<float, float>(0.1f, songLength - 0.5f));
            else
            {
                PluginConfig config = PluginConfig.Instance;
                mapValues.Sort();
                float currentTime = mapValues[0];

                if (config.SkipIntro && currentTime > config.MinSkipTime) skipTimePairs.Add(new Tuple<float, float>(0.1f, currentTime - 1.5f));
                if (config.SkipMiddle)
                {
                    foreach (var time in mapValues)
                    {
                        if (time - currentTime > config.MinSkipTime) skipTimePairs.Add(new Tuple<float, float>(currentTime, time - 1.5f));
                        currentTime = time;
                    }
                }
                if (config.SkipOutro && songLength - mapValues.Last() > config.MinSkipTime) skipTimePairs.Add(new Tuple<float, float>(mapValues.Last(), songLength - 0.5f));
            }

            return skipTimePairs;
        } 
    }
}

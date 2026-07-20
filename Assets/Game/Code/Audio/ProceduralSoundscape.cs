using System.Collections.Generic;
using Wayroot.Gathering;
using UnityEngine;

namespace Wayroot.Audio
{
    /// <summary>Creates a quiet original exploration bed and short warm feedback tones without asset dependencies.</summary>
    public sealed class ProceduralSoundscape : MonoBehaviour
    {
        private const int SampleRate = 22050;
        private const float AmbientDurationSeconds = 8f;
        private readonly Dictionary<SoundscapeCue, AudioClip> _cueClips = new();
        private AudioSource _ambientSource = null!;
        private AudioSource _cueSource = null!;
        private PrototypeGatheringController _gathering = null!;

        public bool IsSoundEnabled => _gathering != null && _gathering.SoundEnabled;

        public void Configure(PrototypeGatheringController gathering)
        {
            _gathering = gathering;
            if (Application.isBatchMode)
            {
                return;
            }

            _ambientSource = gameObject.AddComponent<AudioSource>();
            _ambientSource.loop = true;
            _ambientSource.playOnAwake = false;
            _ambientSource.volume = 0.075f;
            _ambientSource.clip = CreateAmbientClip();
            _cueSource = gameObject.AddComponent<AudioSource>();
            _cueSource.playOnAwake = false;
            _cueSource.volume = 1f;
            ApplyEnabledState();
        }

        public void ToggleSound()
        {
            SetSoundEnabled(!IsSoundEnabled);
        }

        public void SetSoundEnabled(bool enabled)
        {
            _gathering.SetSoundEnabled(enabled);
            ApplyEnabledState();
            if (enabled)
            {
                Play(SoundscapeCue.Ui);
            }
        }

        public void Play(SoundscapeCue cue)
        {
            if (Application.isBatchMode || !SoundscapeRules.IsAudible(IsSoundEnabled, cue) || _cueSource == null)
            {
                return;
            }

            if (!_cueClips.TryGetValue(cue, out AudioClip clip))
            {
                clip = CreateCueClip(cue, SoundscapeRules.GetProfile(cue));
                _cueClips.Add(cue, clip);
            }

            _cueSource.PlayOneShot(clip);
        }

        private void ApplyEnabledState()
        {
            if (Application.isBatchMode || _ambientSource == null)
            {
                return;
            }

            if (IsSoundEnabled)
            {
                if (!_ambientSource.isPlaying) _ambientSource.Play();
            }
            else
            {
                _ambientSource.Stop();
                _cueSource.Stop();
            }
        }

        private static AudioClip CreateAmbientClip()
        {
            int samples = Mathf.RoundToInt(AmbientDurationSeconds * SampleRate);
            float[] data = new float[samples];
            for (int sample = 0; sample < samples; sample++)
            {
                float time = sample / (float)SampleRate;
                float slowSwell = 0.72f + 0.28f * Mathf.Sin(time * Mathf.PI * 2f / AmbientDurationSeconds);
                data[sample] = slowSwell * (0.026f * Mathf.Sin(time * Mathf.PI * 2f * 174f) + 0.014f * Mathf.Sin(time * Mathf.PI * 2f * 261.6f));
            }

            AudioClip clip = AudioClip.Create("Procedural Cozy Ambient", samples, 1, SampleRate, false);
            clip.SetData(data, 0);
            return clip;
        }

        private static AudioClip CreateCueClip(SoundscapeCue cue, SoundscapeProfile profile)
        {
            int samples = Mathf.CeilToInt(profile.DurationSeconds * SampleRate);
            float[] data = new float[samples];
            for (int sample = 0; sample < samples; sample++)
            {
                float normalizedTime = sample / (float)samples;
                float envelope = Mathf.Sin(normalizedTime * Mathf.PI) * (1f - normalizedTime * 0.25f);
                float time = sample / (float)SampleRate;
                float tone = Mathf.Sin(time * Mathf.PI * 2f * profile.FrequencyHz);
                if (profile.SecondFrequencyHz > 0f)
                {
                    tone = tone * 0.72f + Mathf.Sin(time * Mathf.PI * 2f * profile.SecondFrequencyHz) * 0.28f;
                }

                data[sample] = tone * envelope * profile.Volume;
            }

            AudioClip clip = AudioClip.Create($"Procedural {cue}", samples, 1, SampleRate, false);
            clip.SetData(data, 0);
            return clip;
        }

        private void OnDestroy()
        {
            foreach (AudioClip clip in _cueClips.Values)
            {
                Destroy(clip);
            }
        }
    }
}

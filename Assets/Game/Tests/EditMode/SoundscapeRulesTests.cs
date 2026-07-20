using NUnit.Framework;
using Wayroot.Audio;

namespace Wayroot.Tests.EditMode
{
    public sealed class SoundscapeRulesTests
    {
        [Test]
        public void GetProfile_EachPlayerFacingCueUsesAGentleDistinctProfile()
        {
            SoundscapeProfile gather = SoundscapeRules.GetProfile(SoundscapeCue.Gather);
            SoundscapeProfile defeat = SoundscapeRules.GetProfile(SoundscapeCue.Defeat);
            SoundscapeProfile wayroot = SoundscapeRules.GetProfile(SoundscapeCue.WayrootRestore);

            Assert.That(gather.DurationSeconds, Is.LessThanOrEqualTo(SoundscapeRules.MaximumOneShotDurationSeconds));
            Assert.That(gather.FrequencyHz, Is.Not.EqualTo(defeat.FrequencyHz));
            Assert.That(wayroot.FrequencyHz, Is.GreaterThan(gather.FrequencyHz));
            Assert.That(gather.Volume, Is.LessThanOrEqualTo(SoundscapeRules.MaximumOneShotVolume));
        }

        [Test]
        public void IsAudible_RespectsThePersistedSoundPreference()
        {
            Assert.That(SoundscapeRules.IsAudible(true, SoundscapeCue.MosslingGuide), Is.True);
            Assert.That(SoundscapeRules.IsAudible(false, SoundscapeCue.MosslingGuide), Is.False);
        }

        [Test]
        public void DefaultSoundEnabled_IsTrue()
        {
            Assert.That(SoundscapeRules.DefaultSoundEnabled, Is.True);
        }
    }
}

using NUnit.Framework;
using Wayroot.Gathering;

namespace Wayroot.Tests.EditMode
{
    public sealed class GatheringRulesTests
    {
        [Test]
        public void TryAdvance_CompletesOnlyOnFinalRequiredStep()
        {
            bool first = GatheringRules.TryAdvance(0, 3, out int afterFirst, out bool firstComplete);
            bool final = GatheringRules.TryAdvance(2, 3, out int afterFinal, out bool finalComplete);

            Assert.That(first, Is.True);
            Assert.That(afterFirst, Is.EqualTo(1));
            Assert.That(firstComplete, Is.False);
            Assert.That(final, Is.True);
            Assert.That(afterFinal, Is.EqualTo(3));
            Assert.That(finalComplete, Is.True);
        }

        [Test]
        public void TryAdvance_RejectsInvalidOrAlreadyCompleteProgress()
        {
            Assert.That(GatheringRules.TryAdvance(3, 3, out _, out _), Is.False);
            Assert.That(GatheringRules.TryAdvance(0, 0, out _, out _), Is.False);
        }
    }
}

using NUnit.Framework;
using UnityEngine.Rendering;

namespace Wayroot.Tests.EditMode
{
    public sealed class RenderPipelineSettingsTests
    {
        [Test]
        public void DefaultRenderPipeline_IsAssigned()
        {
            Assert.That(GraphicsSettings.defaultRenderPipeline, Is.Not.Null);
        }
    }
}

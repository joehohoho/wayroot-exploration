using NUnit.Framework;
using Wayroot.Core;

namespace Wayroot.Tests.EditMode
{
    public sealed class ProjectIdentityTests
    {
        [Test]
        public void ProjectIdentity_UsesConfiguredNamespaceRoot()
        {
            Assert.That(ProjectIdentity.NamespaceRoot, Is.EqualTo("Wayroot"));
        }
    }
}

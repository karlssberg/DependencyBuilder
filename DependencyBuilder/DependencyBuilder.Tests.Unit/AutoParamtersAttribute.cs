using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace DependencyBuilder.Tests.Unit
{
    public class AutoArgumentsAttribute : AutoDataAttribute
    {
        public AutoArgumentsAttribute() : base(() => new Fixture()
            .Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
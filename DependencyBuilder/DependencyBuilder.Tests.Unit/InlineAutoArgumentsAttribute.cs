using AutoFixture.Xunit2;

namespace DependencyBuilder.Tests.Unit
{
    public class InlineAutoArgumentsAttribute : InlineAutoDataAttribute
    {
        public InlineAutoArgumentsAttribute(params object[] args) : base(new AutoArgumentsAttribute(), args)
        {
        }
    }
}
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Microsoft.CodeAnalysis;

namespace ClassGeneration
{
    public sealed class RoslynClassBuilder<T> : IBuilder<T, string>
    {
        private readonly IBuilder<T, SyntaxNode> _nodeBuilder;

        public RoslynClassBuilder(IBuilder<T, SyntaxNode> nodeBuilder)
        {
            _nodeBuilder = nodeBuilder;
        }

        public string Build(T obj, Settings settings)
        {
            return _nodeBuilder
                .Build(obj, settings)
                .NormalizeWhitespace()
                .GetText()
                .ToString();
        }
    }
}
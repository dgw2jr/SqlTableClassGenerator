using ClassGeneration.Interfaces;
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

        public string Build(T obj)
        {
            return _nodeBuilder
                .Build(obj)
                .NormalizeWhitespace()
                .GetText()
                .ToString();
        }
    }
}
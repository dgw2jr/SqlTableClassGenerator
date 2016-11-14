using System.Collections.Generic;
using System.Linq;
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Microsoft.CodeAnalysis;

namespace ClassGeneration
{
    public sealed class RoslynClassBuilder<T> : IClassBuilder<T>
    {
        private readonly ISyntaxNodeBuilder<T> _classBuilder;
        private readonly IEnumerable<ISyntaxNodesBuilder<T>> _memberBuilders;

        public RoslynClassBuilder(
            ISyntaxNodeBuilder<T> classBuilder,
            IEnumerable<ISyntaxNodesBuilder<T>> memberBuilders)
        {
            _classBuilder = classBuilder;
            _memberBuilders = memberBuilders;
        }

        public string Build(T obj, Settings settings)
        {
            var members = _memberBuilders.SelectMany(b => b.Build(obj, settings)).ToList();

            return _classBuilder
                .WithMembers(members)
                .Build(obj, settings)
                .NormalizeWhitespace()
                .GetText()
                .ToString();
        }
    }
}
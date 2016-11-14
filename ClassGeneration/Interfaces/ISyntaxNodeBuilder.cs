using System.Collections.Generic;
using ClassGeneration.Properties;
using Microsoft.CodeAnalysis;

namespace ClassGeneration.Interfaces
{
    public interface ISyntaxNodeBuilder<T>
    {
        SyntaxNode Build(T obj, Settings settings);
        ISyntaxNodeBuilder<T> WithMembers(IEnumerable<SyntaxNode> members);
    }
}
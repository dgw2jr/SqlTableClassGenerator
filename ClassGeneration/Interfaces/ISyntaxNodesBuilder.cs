using System.Collections.Generic;
using ClassGeneration.Properties;
using Microsoft.CodeAnalysis;

namespace ClassGeneration.Interfaces
{
    public interface ISyntaxNodesBuilder<T>
    {
        IEnumerable<SyntaxNode> Build(T obj, Settings settings);
    }
}
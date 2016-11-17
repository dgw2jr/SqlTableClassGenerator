using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ClassGeneration.Interfaces
{
    public interface ISyntaxNodesBuilder<in TIn>
    {
        IEnumerable<SyntaxNode> Build(TIn obj);
    }
}
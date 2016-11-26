using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ClassGeneration.Interfaces
{
    public interface IClassMembersBuilder<in TIn>
    {
        IEnumerable<SyntaxNode> Build(TIn obj);
    }
}
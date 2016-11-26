using System.Collections.Generic;
using System.Linq;
using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis;

namespace ClassGeneration
{
    public class NullClassMembersBuilder<T> : IClassMembersBuilder<T>
    {
        public IEnumerable<SyntaxNode> Build(T obj)
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}

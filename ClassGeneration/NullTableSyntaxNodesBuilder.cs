using System.Collections.Generic;
using System.Linq;
using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis;
using Models;

namespace ClassGeneration
{
    public class NullTableSyntaxNodesBuilder : ISyntaxNodesBuilder<Table>
    {
        public IEnumerable<SyntaxNode> Build(Table obj)
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}

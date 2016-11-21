using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using ClassGeneration.Interfaces;
using Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration
{
    public sealed class PropertiesBuilder : ISyntaxNodesBuilder<Table>
    {
        private readonly IBuilder<Column, PropertyDeclarationSyntax> _columnPropertyGenerator;

        public PropertiesBuilder(IBuilder<Column, PropertyDeclarationSyntax> columnPropertyGenerator)
        {
            _columnPropertyGenerator = columnPropertyGenerator;
        }

        public IEnumerable<SyntaxNode> Build(Table table)
        {
            return table.Columns
                .Select(c => _columnPropertyGenerator.Build(c))
                .ToList();
        }
    }
}
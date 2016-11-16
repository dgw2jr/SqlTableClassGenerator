using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration
{
    public sealed class PropertiesBuilder : IBuilder<Table, IEnumerable<SyntaxNode>>
    {
        private readonly IBuilder<Column, PropertyDeclarationSyntax> _columnPropertyGenerator;

        public PropertiesBuilder(IBuilder<Column, PropertyDeclarationSyntax> columnPropertyGenerator)
        {
            _columnPropertyGenerator = columnPropertyGenerator;
        }

        public IEnumerable<SyntaxNode> Build(Table table, Settings settings)
        {
            var props = table.Columns.Select(c =>
                _columnPropertyGenerator.Build(c, settings)).ToList();

            return props;
        }
    }
}
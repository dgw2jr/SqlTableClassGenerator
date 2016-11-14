using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using ClassGeneration.Interfaces;
using ClassGeneration.Models;
using ClassGeneration.Properties;

namespace ClassGeneration
{
    public sealed class PropertiesBuilder : ISyntaxNodesBuilder<Table>
    {
        private readonly IPropertyGenerator<Column> _columnPropertyGenerator;

        public PropertiesBuilder(IPropertyGenerator<Column> columnPropertyGenerator)
        {
            _columnPropertyGenerator = columnPropertyGenerator;
        }

        public IEnumerable<SyntaxNode> Build(Table table, Settings settings)
        {
            var props = table.Columns.Select(c =>
                _columnPropertyGenerator.Generate(c, settings)).ToList();

            return props;
        }
    }
}
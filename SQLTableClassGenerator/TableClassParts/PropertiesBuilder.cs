using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class PropertiesBuilder : IClassMemberBuilderForTable
    {
        private readonly SyntaxGenerator _generator;

        public PropertiesBuilder(SyntaxGenerator generator)
        {
            _generator = generator;
        }

        public IEnumerable<SyntaxNode> Build(TableDef table)
        {
            var props = table.Columns.Select(c =>
                _generator.PropertyDeclaration(c.Field,
                    c.Type.TypeExpression(),
                    ));

            return props;
        }
    }

}
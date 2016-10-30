using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class ConstructorParameterBuilder
    {
        private readonly SyntaxGenerator _generator;

        public ConstructorParameterBuilder(SyntaxGenerator generator)
        {
            _generator = generator;
        }

        public IEnumerable<SyntaxNode> Build(TableDef table)
        {
            var parameters = table.Columns.Aggregate(new List<SyntaxNode>(),
                (seed, curr) =>
                {
                    seed.Add(
                        _generator.ParameterDeclaration(
                            curr.Field,
                            curr.Type.TypeExpression()));
                    return seed;
                });

            return parameters;
        }
    }
}
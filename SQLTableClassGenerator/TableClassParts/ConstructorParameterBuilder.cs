using System.Linq;
using System.Collections.Generic;
using SQLTableClassGenerator.TableElements;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class ConstructorParameterBuilder
    {
        public ParameterSyntax[] Build(TableDef table)
        {
            var parameters = table.Columns.Aggregate(new List<ParameterSyntax>(),
                (seed, curr) =>
                {
                    seed.Add(
                        SyntaxFactory.Parameter(SyntaxFactory.ParseToken(curr.Field.ToCamelCase()))
                            .WithType(SyntaxFactory.ParseTypeName(curr.Type.Name)));
                    return seed;
                });
            
            return parameters.ToArray();
        }
    }
}
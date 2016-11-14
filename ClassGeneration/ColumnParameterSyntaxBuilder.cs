using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using ClassGeneration.Models;
using ClassGeneration.Interfaces;

namespace ClassGeneration
{
    public sealed class ColumnParameterSyntaxBuilder : IParameterSyntaxBuilder<Column>
    {
        public ParameterSyntax[] Build(IEnumerable<Column> columns)
        {
            var parameters = columns.Aggregate(new SyntaxList<ParameterSyntax>(),
                (seed, curr) =>
                    seed.Add(
                        SyntaxFactory.Parameter(SyntaxFactory.ParseToken(curr.Field.ToCamelCase()))
                            .WithType(SyntaxFactory.ParseTypeName(curr.Type.Name))));
            
            return parameters.ToArray();
        }
    }
}
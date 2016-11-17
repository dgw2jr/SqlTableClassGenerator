using System.Collections.Generic;
using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace ClassGeneration
{
    public class ColumnPropertiesAssignmentBlockBuilder : IBuilder<IEnumerable<Column>, SyntaxList<StatementSyntax>>
    {
        public SyntaxList<StatementSyntax> Build(IEnumerable<Column> columns)
        {
            var seed = SyntaxFactory.List<StatementSyntax>();

            foreach (var column in columns)
            {
                seed = seed.Add(
                    SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName(column.Field),
                            SyntaxFactory.IdentifierName(column.Field.ToCamelCase()))));
            }

            return seed;
        }
    }
}
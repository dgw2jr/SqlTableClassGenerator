using System.Linq;
using ClassGeneration.Requests;
using MediatR;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.RequestHandlers
{
    public class GetColumnPropertiesAssignmentBlockRequestHandler : IRequestHandler<GetColumnPropertiesAssignmentBlockRequest, SyntaxList<StatementSyntax>>
    {
        public SyntaxList<StatementSyntax> Handle(GetColumnPropertiesAssignmentBlockRequest message)
        {
            return message.Columns.Aggregate(SyntaxFactory.List<StatementSyntax>(),
                (seed, column) => seed.Add(
                    SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName(column.Field),
                            SyntaxFactory.IdentifierName(column.Field.ToCamelCase())))));
        }
    }
}
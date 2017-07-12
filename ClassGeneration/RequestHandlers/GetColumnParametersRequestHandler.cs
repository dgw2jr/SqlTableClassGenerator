using System.Linq;
using ClassGeneration.Requests;
using MediatR;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.RequestHandlers
{
    public sealed class GetColumnParametersRequestHandler : IRequestHandler<GetColumnParametersRequest, ParameterSyntax[]>
    {
        public ParameterSyntax[] Handle(GetColumnParametersRequest message)
        {
            var parameters = message.Columns.Aggregate(new SyntaxList<ParameterSyntax>(),
                (seed, curr) =>
                    seed.Add(
                        SyntaxFactory.Parameter(SyntaxFactory.ParseToken(curr.Field.ToCamelCase()))
                            .WithType(SyntaxFactory.ParseTypeName(curr.Type.Name))));

            return parameters.ToArray();
        }
    }
}
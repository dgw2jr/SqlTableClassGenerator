using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace ClassGeneration.Requests
{
    public class GetPropertyForColumnRequest : IRequest<PropertyDeclarationSyntax>
    {
        public Column Column { get; set; }
    }
}
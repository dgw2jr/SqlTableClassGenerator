using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.Requests
{
    public class GetPropertyAccessModifiersRequest : IRequest<AccessorDeclarationSyntax[]> { }
}
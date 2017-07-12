using MediatR;
using Microsoft.CodeAnalysis.Editing;

namespace ClassGeneration.Requests
{
    public class GetClassDeclarationModifierRequest : IRequest<DeclarationModifiers> {}
}
using MediatR;
using Microsoft.CodeAnalysis;

namespace ClassGeneration.Requests
{
    public class GetPropertySetterAccessModifierRequest : IRequest<SyntaxToken>{}
}
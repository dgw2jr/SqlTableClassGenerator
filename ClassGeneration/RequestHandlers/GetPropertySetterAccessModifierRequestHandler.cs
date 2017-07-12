using ClassGeneration.Properties;
using ClassGeneration.Requests;
using MediatR;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ClassGeneration.RequestHandlers
{
    public class GetPropertySetterAccessModifierRequestHandler : IRequestHandler<GetPropertySetterAccessModifierRequest, SyntaxToken>
    {
        public SyntaxToken Handle(GetPropertySetterAccessModifierRequest message)
        {
            return SyntaxFactory.Token(Settings.Default.PrivateSetters ? SyntaxKind.PrivateKeyword : SyntaxKind.BadToken);
        }
    }
}
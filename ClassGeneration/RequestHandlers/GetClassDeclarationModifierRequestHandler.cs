using ClassGeneration.Properties;
using ClassGeneration.Requests;
using MediatR;
using Microsoft.CodeAnalysis.Editing;

namespace ClassGeneration.RequestHandlers
{
    public class GetClassDeclarationModifierRequestHandler : IRequestHandler<GetClassDeclarationModifierRequest, DeclarationModifiers>
    {
        public DeclarationModifiers Handle(GetClassDeclarationModifierRequest message)
        {
            return Settings.Default.IsSealed ? DeclarationModifiers.Sealed : DeclarationModifiers.None;
        }
    }
}

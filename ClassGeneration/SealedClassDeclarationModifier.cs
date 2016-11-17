using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis.Editing;

namespace ClassGeneration
{
    public class SealedClassDeclarationModifier : IClassDeclarationModifier
    {
        public DeclarationModifiers GetModifier()
        {
            return DeclarationModifiers.Sealed;
        }
    }
}

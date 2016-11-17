using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis.Editing;

namespace ClassGeneration
{
    public class NullClassDeclarationModifier : IClassDeclarationModifier
    {
        public DeclarationModifiers GetModifier()
        {
            return DeclarationModifiers.None;
        }
    }
}

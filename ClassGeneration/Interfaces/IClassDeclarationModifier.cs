using Microsoft.CodeAnalysis.Editing;

namespace ClassGeneration.Interfaces
{
    public interface IClassDeclarationModifier
    {
        DeclarationModifiers GetModifier();
    }
}
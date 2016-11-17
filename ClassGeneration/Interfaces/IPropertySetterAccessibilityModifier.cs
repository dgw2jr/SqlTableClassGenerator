using Microsoft.CodeAnalysis;

namespace ClassGeneration.Interfaces
{
    public interface IPropertySetterAccessibilityModifier
    {
        SyntaxToken GetToken();
    }
}
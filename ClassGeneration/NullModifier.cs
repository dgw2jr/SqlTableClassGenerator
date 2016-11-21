using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ClassGeneration
{
    public class NullModifier : IPropertySetterAccessibilityModifier
    {
        public SyntaxToken GetToken()
        {
            return SyntaxFactory.Token(SyntaxKind.BadToken);
        }
    }
}
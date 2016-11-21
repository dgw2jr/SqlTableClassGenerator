using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration
{
    public class PropertyGetterAndSetter : IPropertyAccessorDeclarations
    {
        private readonly IPropertySetterAccessibilityModifier _propertySetterModifier;

        public PropertyGetterAndSetter(IPropertySetterAccessibilityModifier propertySetterModifier)
        {
            _propertySetterModifier = propertySetterModifier;
        }

        public AccessorDeclarationSyntax[] GetDeclarations()
        {
            return new[] {
                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .AddModifiers(_propertySetterModifier.GetToken())
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
            };
        }
    }
}
using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace ClassGeneration
{
    public class ColumnPropertyBuilder : IBuilder<Column, PropertyDeclarationSyntax>
    {
        private readonly IPropertySetterAccessibilityModifier _propertySetterModifier;

        public ColumnPropertyBuilder(IPropertySetterAccessibilityModifier propertySetterModifier)
        {
            _propertySetterModifier = propertySetterModifier;
        }

        public PropertyDeclarationSyntax Build(Column c)
        {
            var prop = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(c.Type.Name), c.Field)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .AddModifiers(_propertySetterModifier.GetToken())
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

            return prop;
        }
    }

    public class NullPropertySetterAccessibilityModifier : IPropertySetterAccessibilityModifier
    {
        public SyntaxToken GetToken()
        {
            return SyntaxFactory.Token(SyntaxKind.BadToken);
        }
    }

    public class PrivatePropertySetterAccessibilityModifier : IPropertySetterAccessibilityModifier
    {
        public SyntaxToken GetToken()
        {
            return SyntaxFactory.Token(SyntaxKind.PrivateKeyword);
        }
    }
}
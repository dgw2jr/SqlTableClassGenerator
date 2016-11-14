using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace ClassGeneration
{
    public class ColumnPropertyGenerator : IPropertyGenerator<Column>
    {
        public PropertyDeclarationSyntax Generate(Column c, Settings settings)
        {
            var usePrivateSettersToken = settings.PrivateSetters ? SyntaxFactory.Token(SyntaxKind.PrivateKeyword) : SyntaxFactory.Token(SyntaxKind.BadToken);

            var prop = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(c.Type.Name), c.Field)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                        .AddModifiers(usePrivateSettersToken)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

            return prop;
        }
    }
}

using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration
{
    public sealed class PropertyGetter : IPropertyAccessorDeclarations
    {
        public AccessorDeclarationSyntax[] GetDeclarations()
        {
            return new[] {
                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
            };
        }
    }
}
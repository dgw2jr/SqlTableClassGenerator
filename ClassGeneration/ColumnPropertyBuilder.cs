using ClassGeneration.Interfaces;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace ClassGeneration
{
    public class ColumnPropertyBuilder : IBuilder<Column, PropertyDeclarationSyntax>
    {
        private readonly IPropertyAccessorDeclarations _accessorDeclarations;

        public ColumnPropertyBuilder(IPropertyAccessorDeclarations accessorDeclarations)
        {
            _accessorDeclarations = accessorDeclarations;
        }

        public PropertyDeclarationSyntax Build(Column c)
        {
            return SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(c.Type.Name), c.Field)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(_accessorDeclarations.GetDeclarations());
        }
    }
}
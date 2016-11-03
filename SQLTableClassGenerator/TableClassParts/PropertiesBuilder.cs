using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using SQLTableClassGenerator.TableElements;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class PropertiesBuilder : IClassMemberBuilderForTable
    {
        public IEnumerable<SyntaxNode> Build(TableDef table)
        {
            var props = table.Columns.Select(c =>
                GenerateProperty(c)).ToList();

            return props;
        }

        private PropertyDeclarationSyntax GenerateProperty(ColumnDef c)
        {
            var prop = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(c.Type.Name), c.Field)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

            return prop;
        }
    }

}
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using SQLTableClassGenerator.TableElements;
using SQLTableClassGenerator.Properties;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class ConstructorBuilder : IClassMemberBuilderForTable
    {
        private readonly ConstructorParameterBuilder _ctorParameterBuilder;

        public ConstructorBuilder(ConstructorParameterBuilder ctorParameterBuilder)
        {
            _ctorParameterBuilder = ctorParameterBuilder;
        }

        public IEnumerable<SyntaxNode> Build(TableDef table)
        {
            if(!Settings.Default.GenerateConstructor)
            {
                return new List<SyntaxNode>();
            }

            var parameters = _ctorParameterBuilder.Build(table);

            var ctor = SyntaxFactory.ConstructorDeclaration(table.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters)
                .WithBody(SyntaxFactory.Block(GetBlockStatements(table)));

            return new List<SyntaxNode> { ctor };
        }

        private SyntaxList<StatementSyntax> GetBlockStatements(TableDef table)
        {
            var seed = SyntaxFactory.List<StatementSyntax>();

            foreach(var column in table.Columns)
            {
                seed = seed.Add(
                    SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression, 
                            SyntaxFactory.IdentifierName(column.Field), 
                            SyntaxFactory.IdentifierName(column.Field.ToCamelCase()))));
            }
            
            return seed;
        }
    }
}
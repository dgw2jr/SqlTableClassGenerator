using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ClassGeneration.Interfaces;
using Models;

namespace ClassGeneration
{
    public sealed class ConstructorBuilder : ISyntaxNodesBuilder<Table>
    {
        private readonly IBuilder<IEnumerable<Column>, ParameterSyntax[]> _ctorParameterBuilder;
        private readonly IBuilder<IEnumerable<Column>, SyntaxList<StatementSyntax>> _blockStatementListBuilder;

        public ConstructorBuilder(
            IBuilder<IEnumerable<Column>, ParameterSyntax[]> ctorParameterBuilder,
            IBuilder<IEnumerable<Column>, SyntaxList<StatementSyntax>> blockStatementListBuilder)
        {
            _ctorParameterBuilder = ctorParameterBuilder;
            _blockStatementListBuilder = blockStatementListBuilder;
        }

        public IEnumerable<SyntaxNode> Build(Table table)
        {
            var parameters = _ctorParameterBuilder.Build(table.Columns);

            var ctor = SyntaxFactory.ConstructorDeclaration(table.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters)
                .WithBody(SyntaxFactory.Block(_blockStatementListBuilder.Build(table.Columns)));

            return new List<SyntaxNode> { ctor };
        }
    }
}
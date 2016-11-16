using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration
{
    public sealed class ConstructorBuilder : IBuilder<Table, IEnumerable<SyntaxNode>>
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

        public IEnumerable<SyntaxNode> Build(Table table, Settings settings)
        {
            if (!settings.GenerateConstructor)
            {
                return new List<SyntaxNode>();
            }

            var parameters = _ctorParameterBuilder.Build(table.Columns, settings);

            var ctor = SyntaxFactory.ConstructorDeclaration(table.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters)
                .WithBody(SyntaxFactory.Block(_blockStatementListBuilder.Build(table.Columns, settings)));

            return new List<SyntaxNode> { ctor };
        }
    }
}
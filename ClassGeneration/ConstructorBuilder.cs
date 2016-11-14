using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Models;

namespace ClassGeneration
{
    public sealed class ConstructorBuilder : ISyntaxNodesBuilder<Table>
    {
        private readonly IBlockStatementListGenerator<IEnumerable<Column>> _blockStatementListGenerator;
        private readonly IParameterSyntaxBuilder<Column> _ctorParameterBuilder;

        public ConstructorBuilder(
            IParameterSyntaxBuilder<Column> ctorParameterBuilder,
            IBlockStatementListGenerator<IEnumerable<Column>> blockStatementListGenerator)
        {
            _ctorParameterBuilder = ctorParameterBuilder;
            _blockStatementListGenerator = blockStatementListGenerator;
        }

        public IEnumerable<SyntaxNode> Build(Table table, Settings settings)
        {
            if (!settings.GenerateConstructor)
            {
                return new List<SyntaxNode>();
            }

            var parameters = _ctorParameterBuilder.Build(table.Columns);

            var ctor = SyntaxFactory.ConstructorDeclaration(table.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters)
                .WithBody(SyntaxFactory.Block(_blockStatementListGenerator.Generate(table.Columns)));

            return new List<SyntaxNode> { ctor };
        }
    }
}
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using ClassGeneration.Interfaces;
using ClassGeneration.Models;
using ClassGeneration.Properties;

namespace ClassGeneration
{
    public sealed class TableSyntaxNodeBuilder : ISyntaxNodeBuilder<Table>
    {
        private readonly SyntaxGenerator _generator;
        private IEnumerable<SyntaxNode> _members;

        public TableSyntaxNodeBuilder()
        {
            var _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);
            _generator = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);
        }

        public SyntaxNode Build(Table table, Settings settings)
        {
            var modifiers = settings.IsSealed ? DeclarationModifiers.Sealed : DeclarationModifiers.None;

            var classDeclaration = _generator.ClassDeclaration(
                table.Name, 
                modifiers: modifiers, 
                members: _members,
                accessibility: Accessibility.Public);

            return classDeclaration;
        }

        public ISyntaxNodeBuilder<Table> WithMembers(IEnumerable<SyntaxNode> members)
        {
            _members = members;
            return this;
        }
    }
}
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Models;
using System.Linq;

namespace ClassGeneration
{
    public sealed class ClassDeclarationBuilder<TIn> : IBuilder<TIn, SyntaxNode> where TIn : IHasName
    {
        private readonly SyntaxGenerator _generator;
        private readonly IEnumerable<IBuilder<TIn, IEnumerable<SyntaxNode>>> _memberBuilders;

        public ClassDeclarationBuilder(IEnumerable<IBuilder<TIn, IEnumerable<SyntaxNode>>> memberBuilders)
        {
            var _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);
            _generator = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);

            _memberBuilders = memberBuilders;
        }

        public SyntaxNode Build(TIn obj, Settings settings)
        {
            var modifiers = settings.IsSealed ? DeclarationModifiers.Sealed : DeclarationModifiers.None;
            var members = _memberBuilders.SelectMany(b => b.Build(obj, settings)).ToList();

            var classDeclaration = _generator.ClassDeclaration(
                obj.Name, 
                modifiers: modifiers, 
                members: members,
                accessibility: Accessibility.Public);

            return classDeclaration;
        }
    }
}
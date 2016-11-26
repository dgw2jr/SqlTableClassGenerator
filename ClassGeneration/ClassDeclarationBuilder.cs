using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using ClassGeneration.Interfaces;
using Models;
using System;

namespace ClassGeneration
{
    public sealed class ClassDeclarationBuilder<TIn> : IBuilder<TIn, SyntaxNode> where TIn : IHasName
    {
        private readonly IClassDeclarationModifier _classDeclarationModifier;
        private readonly SyntaxGenerator _generator;
        private readonly IEnumerable<IClassMembersBuilder<TIn>> _memberBuilders;

        public ClassDeclarationBuilder(
            IEnumerable<IClassMembersBuilder<TIn>> memberBuilders,
            IClassDeclarationModifier classDeclarationModifier,
            SyntaxGenerator syntaxGenerator)
        {
            var _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);
            Console.WriteLine(_.Name);

            _generator = syntaxGenerator;

            _memberBuilders = memberBuilders.OrderBy(m => m.GetType().Name);
            _classDeclarationModifier = classDeclarationModifier;
        }

        public SyntaxNode Build(TIn obj)
        {
            var members = _memberBuilders.SelectMany(b => b.Build(obj)).ToList();

            return _generator.ClassDeclaration(
                obj.Name,
                modifiers: _classDeclarationModifier.GetModifier(),
                members: members,
                accessibility: Accessibility.Public);
        }
    }
}
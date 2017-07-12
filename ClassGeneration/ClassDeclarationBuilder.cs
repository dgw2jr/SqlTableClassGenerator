using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using ClassGeneration.Interfaces;
using Models;
using System;
using ClassGeneration.Requests;
using MediatR;

namespace ClassGeneration
{
    public sealed class ClassDeclarationBuilder : IBuilder<Table, SyntaxNode>
    {
        private readonly SyntaxGenerator _generator;
        private readonly IMediator _mediator;

        public ClassDeclarationBuilder(SyntaxGenerator syntaxGenerator,
            IMediator mediator)
        {
            var _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);
            Console.WriteLine(_.Name);

            _generator = syntaxGenerator;
            _mediator = mediator;
        }

        public SyntaxNode Build(Table table)
        {
            var members = new List<SyntaxNode>();
            members.AddRange(_mediator.Send(new GetConstructorBlockRequest { Table = table }).Result);
            members.AddRange(table.Columns
                .Select(c => _mediator.Send(new GetPropertyForColumnRequest { Column = c }).Result)
                .ToList());

            return _generator.ClassDeclaration(
                table.Name,
                modifiers: _mediator.Send(new GetClassDeclarationModifierRequest()).Result,
                members: members,
                accessibility: Accessibility.Public);
        }
    }
}
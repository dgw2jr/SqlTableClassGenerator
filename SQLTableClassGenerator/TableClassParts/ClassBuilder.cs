using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using SQLTableClassGenerator.TableElements;
using SQLTableClassGenerator.Properties;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class ClassBuilder
    {
        private readonly SyntaxGenerator _generator;
        private IEnumerable<SyntaxNode> _members;

        public ClassBuilder(SyntaxGenerator generator)
        {
            _generator = generator;
        }

        public SyntaxNode Build(TableDef table)
        {
            var modifiers = Settings.Default.IsSealed ? DeclarationModifiers.Sealed : DeclarationModifiers.None;

            var classDeclaration = _generator.ClassDeclaration(
                table.Name, 
                modifiers: modifiers, 
                members: _members,
                accessibility: Accessibility.Public);

            return classDeclaration;
        }

        public ClassBuilder WithMembers(IEnumerable<SyntaxNode> members)
        {
            _members = members;
            return this;
        }
    }

}
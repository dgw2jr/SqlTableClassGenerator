using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using SQLTableClassGenerator.TableElements;
using SQLTableClassGenerator.Properties;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class ConstructorBuilder : IClassMemberBuilderForTable
    {
        private readonly ConstructorParameterBuilder _ctorParameterBuilder;
        private readonly SyntaxGenerator _generator;

        public ConstructorBuilder(SyntaxGenerator generator, ConstructorParameterBuilder ctorParameterBuilder)
        {
            _generator = generator;
            _ctorParameterBuilder = ctorParameterBuilder;
        }

        public IEnumerable<SyntaxNode> Build(TableDef table)
        {
            if(!Settings.Default.GenerateConstructor)
            {
                return new List<SyntaxNode>();
            }

            var ctor = _generator.ConstructorDeclaration(
                table.Name, 
                _ctorParameterBuilder.Build(table), 
                Accessibility.Public);

            return new List<SyntaxNode> { ctor };
        }
    }
}
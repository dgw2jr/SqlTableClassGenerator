using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using SQLTableClassGenerator.TableClassParts.Interfaces;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public sealed class RoslynTableClassBuilder : ITableClassBuilder
    {
        private readonly ClassBuilder _classBuilder;
        private readonly IEnumerable<IClassMemberBuilderForTable> _memberBuilders;

        public RoslynTableClassBuilder(
            ClassBuilder classBuilder,
            IEnumerable<IClassMemberBuilderForTable> memberBuilders)
        {
            _classBuilder = classBuilder;
            _memberBuilders = memberBuilders;
        }

        public string Build(TableDef table)
        {
            var members = _memberBuilders.SelectMany(b => b.Build(table)).ToList();

            return _classBuilder
                .WithMembers(members)
                .Build(table)
                .NormalizeWhitespace()
                .GetText()
                .ToString();
        }
    }
}
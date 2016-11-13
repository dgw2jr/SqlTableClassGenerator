using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts.Interfaces
{
    public interface IClassMemberBuilderForTable
    {
        IEnumerable<SyntaxNode> Build(TableDef table);
    }
}
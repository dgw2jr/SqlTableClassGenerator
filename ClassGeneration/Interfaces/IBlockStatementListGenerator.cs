using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.Interfaces
{
    public interface IBlockStatementListGenerator<T>
    {
        SyntaxList<StatementSyntax> Generate(T obj);
    }
}
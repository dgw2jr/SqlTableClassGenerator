using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.Interfaces
{
    public interface IPropertyAccessorDeclarations
    {
        AccessorDeclarationSyntax[] GetDeclarations();
    }
}
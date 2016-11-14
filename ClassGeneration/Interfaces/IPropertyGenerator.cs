using ClassGeneration.Properties;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.Interfaces
{
    public interface IPropertyGenerator<T>
    {
        PropertyDeclarationSyntax Generate(T obj, Settings settings);
    }
}
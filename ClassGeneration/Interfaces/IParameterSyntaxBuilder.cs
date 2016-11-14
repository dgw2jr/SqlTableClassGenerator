using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.Interfaces
{
    public interface IParameterSyntaxBuilder<in TIn>
    {
        ParameterSyntax[] Build(IEnumerable<TIn> obj);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace SQLTableClassGenerator
{
    internal static class Extensions
    {
        internal static IEnumerable<T> Switch<TOn, TOff, T>(this IEnumerable<T> list, bool predicate)
        {
            return list.Where(item => item.GetType() != (predicate ? typeof(TOff) : typeof(TOn)));
        }

        internal static SyntaxNode TypeExpression(this Type type)
        {
            var generator = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);
            var mscorlib = MetadataReference.CreateFromFile(typeof(Object).Assembly.Location);
            var c = CSharpCompilation.Create("a", references: new[] { mscorlib });

            return generator.TypeExpression(c.GetTypeByMetadataName(type.FullName));
        }

        internal static string ToCamelCase(this string the_string)
        {
            if (the_string.Length <= 1)
                return the_string.ToLower();

            var first = the_string.ToCharArray(0, 1)[0].ToString();
            return first.ToLower() + new string(the_string.ToCharArray(1, the_string.Length - 1));
        }
    }
}

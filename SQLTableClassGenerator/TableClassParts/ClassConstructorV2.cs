using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{

    internal class ClassConstructorV2 : ITableClassPart
    {
        private readonly AdhocWorkspace _workspace;

        public ClassConstructorV2(AdhocWorkspace workspace)
        {
            _workspace = workspace;
        }

        public string GetString(TableDef table)
        {
            var generator = SyntaxGenerator.GetGenerator(_workspace, LanguageNames.CSharp);
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var c = CSharpCompilation.Create("a", references: new [] { mscorlib });

            var parameters = table.Columns.Aggregate(new List<SyntaxNode>(),
                (seed, curr) =>
                {
                    seed.Add(
                        generator.ParameterDeclaration(
                            curr.Field,
                            generator.TypeExpression(
                                c.Assembly.GetTypeByMetadataName(curr.Type.FullName))));
                    return seed;
                });

            var ctor = generator.ConstructorDeclaration(table.Name, parameters, Accessibility.Public);

            return ctor.GetText().ToString();
        }
    }

}
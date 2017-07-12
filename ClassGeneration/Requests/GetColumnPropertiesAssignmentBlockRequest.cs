using System.Collections.Generic;
using MediatR;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace ClassGeneration.Requests
{
    public class GetColumnPropertiesAssignmentBlockRequest : IRequest<SyntaxList<StatementSyntax>>
    {
        public IEnumerable<Column> Columns { get; set; }
    }
}
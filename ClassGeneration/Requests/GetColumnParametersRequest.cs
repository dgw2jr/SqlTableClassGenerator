using System.Collections.Generic;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace ClassGeneration.Requests
{
    public class GetColumnParametersRequest : IRequest<ParameterSyntax[]>
    {
        public IEnumerable<Column> Columns { get; set; }
    }
}
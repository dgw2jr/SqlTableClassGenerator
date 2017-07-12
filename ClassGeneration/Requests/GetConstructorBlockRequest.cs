using System.Collections.Generic;
using MediatR;
using Microsoft.CodeAnalysis;
using Models;

namespace ClassGeneration.Requests
{
    public class GetConstructorBlockRequest : IRequest<IEnumerable<SyntaxNode>>
    {
        public Table Table { get; set; }
    }
}
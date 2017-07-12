using System.Collections.Generic;
using System.Linq;
using ClassGeneration.Properties;
using ClassGeneration.Requests;
using MediatR;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ClassGeneration.RequestHandlers
{
    public sealed class GetConstructorBlockRequestHandler : IRequestHandler<GetConstructorBlockRequest, IEnumerable<SyntaxNode>>
    {
        private readonly IMediator _mediator;

        public GetConstructorBlockRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IEnumerable<SyntaxNode> Handle(GetConstructorBlockRequest request)
        {
            if (!Settings.Default.GenerateConstructor)
            {
                return Enumerable.Empty<SyntaxNode>();
            }

            var parameters = _mediator.Send(new GetColumnParametersRequest { Columns = request.Table.Columns }).Result;

            var ctor = SyntaxFactory.ConstructorDeclaration(request.Table.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters)
                .WithBody(SyntaxFactory.Block(_mediator.Send(new GetColumnPropertiesAssignmentBlockRequest { Columns = request.Table.Columns }).Result));

            return new List<SyntaxNode> { ctor };
        }
    }
}
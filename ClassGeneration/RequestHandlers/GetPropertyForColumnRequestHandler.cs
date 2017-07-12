using ClassGeneration.Requests;
using MediatR;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.RequestHandlers
{
    public class GetPropertyForColumnRequestHandler : IRequestHandler<GetPropertyForColumnRequest, PropertyDeclarationSyntax>
    {
        private readonly IMediator _mediator;

        public GetPropertyForColumnRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public PropertyDeclarationSyntax Handle(GetPropertyForColumnRequest message)
        {
            return SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(message.Column.Type.Name), message.Column.Field)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(_mediator.Send(new GetPropertyAccessModifiersRequest()).Result);
        }
    }
}
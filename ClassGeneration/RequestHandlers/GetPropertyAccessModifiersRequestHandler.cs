using ClassGeneration.Properties;
using ClassGeneration.Requests;
using MediatR;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClassGeneration.RequestHandlers
{
    public class GetPropertyAccessModifiersRequestHandler : IRequestHandler<GetPropertyAccessModifiersRequest, AccessorDeclarationSyntax[]>
    {
        private readonly IMediator _mediator;

        public GetPropertyAccessModifiersRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public AccessorDeclarationSyntax[] Handle(GetPropertyAccessModifiersRequest message)
        {
            if (Settings.Default.Immutable)
            {
                return new[] {
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                };
            }

            return new[] {
                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .AddModifiers(_mediator.Send(new GetPropertySetterAccessModifierRequest()).Result)
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
            };
        }
    }
}
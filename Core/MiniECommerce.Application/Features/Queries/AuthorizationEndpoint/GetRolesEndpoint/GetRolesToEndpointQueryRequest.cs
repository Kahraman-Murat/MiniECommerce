using MediatR;

namespace MiniECommerce.Application.Features.Queries.AuthorizationEndpoint.GetRolesEndpoints
{
    public class GetRolesToEndpointQueryRequest : IRequest<GetRolesToEndpointQueryResponse>
    {
        public string Code { get; set; }
        public string Menu { get; set; }
    }
}
using HotChocolate.Authorization;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;


namespace SecurityGateApv.WebApi.Query
{
    public class QueryExample
    {
        [UseOffsetPaging(MaxPageSize = int.MaxValue, IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<GetVisitNoDetailRes>> GetVisit([Service] IVisitService _visitService, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext == null || !httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                throw new Exception("Authorization header is missing.");
            }
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return (await _visitService.GetAllVisitGraphQl(int.MaxValue,1, token)).Value;
        }
        public async Task<GetVisitRes> GetVisitById([Service] IVisitService _visitService, int visitId)
        {
            return (await _visitService.GetVisitDetailByVisitId(visitId)).Value;
        }

        //[Authorize(Roles = new[] { "Admin" })]
        [UseOffsetPaging(MaxPageSize = int.MaxValue, IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<GetVisitorSessionGraphQLRes>> GetVisitorSession([Service] IVisitorSessionService _visitorSessionService, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext == null || !httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                throw new Exception("Authorization header is missing.");
            }

            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _visitorSessionService.GetAllVisitorSessionGraphQL(1, int.MaxValue, token);

            if (result.IsFailure)
            {
                throw new Exception(result.Error.Message);
            }
            return result.Value;
        }
    }
}

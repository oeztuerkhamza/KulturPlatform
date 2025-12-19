using KulturPlatform.Application.Dtos.TeaEventDto;
using MediatR;

namespace KulturPlatform.Application.Queries.TeaEvent
{
    public sealed record GetAllTeaEventQuery() : IRequest<IEnumerable<TeaEventDto>>;
}
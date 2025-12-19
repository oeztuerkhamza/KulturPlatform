using KulturPlatform.Application.Dtos.ImprintDto;
using MediatR;

namespace KulturPlatform.Application.Queries.Imprint;

public record GetImprintQuery() : IRequest<ImprintDto?>;
using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsSummaryQueryHandler : IRequestHandler<GetAboutUsSummaryQuery, AboutUsSummaryDto>
{
    private readonly IAboutUsQuoteRepository _quoteRepository;
    private readonly IAboutUsWhoWeAreRepository _whoWeAreRepository;
    private readonly IAboutUsGoalsRepository _goalsRepository;
    private readonly IMapper _mapper;

    public GetAboutUsSummaryQueryHandler(
        IAboutUsQuoteRepository quoteRepository,
        IAboutUsWhoWeAreRepository whoWeAreRepository,
        IAboutUsGoalsRepository goalsRepository,
        IMapper mapper)
    {
        _quoteRepository = quoteRepository;
        _whoWeAreRepository = whoWeAreRepository;
        _goalsRepository = goalsRepository;
        _mapper = mapper;
    }

    public async Task<AboutUsSummaryDto> Handle(GetAboutUsSummaryQuery request, CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetAsync(cancellationToken);
        var whoWeAre = await _whoWeAreRepository.GetAsync(cancellationToken);
        var goals = await _goalsRepository.GetAsync(cancellationToken);

        return new AboutUsSummaryDto
        {
            Quote = quote != null ? _mapper.Map<AboutUsQuoteDto>(quote) : null,
            WhoWeAre = whoWeAre != null ? _mapper.Map<AboutUsWhoWeAreDto>(whoWeAre) : null,
            Goals = goals != null ? _mapper.Map<AboutUsGoalsDto>(goals) : null
        };
    }
}

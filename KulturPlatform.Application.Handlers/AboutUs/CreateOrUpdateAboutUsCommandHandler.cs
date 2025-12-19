using AutoMapper;
using KulturPlatform.Application.Commands.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Handlers.AboutUs
{
    public class CreateOrUpdateAboutUsCommandHandler : IRequestHandler<CreateOrUpdateAboutUsCommand>
    {
        private readonly IAboutUsRepository _repository;
        private readonly IMapper _mapper;

        public CreateOrUpdateAboutUsCommandHandler(IAboutUsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateOrUpdateAboutUsCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByKeyAsync(request.Key, cancellationToken);

            var sections = request.Dto.Sections?.Select(s =>
                NamedSection.Create(s.Key, SectionContent.Create(s.Content.Heading, s.Content.BodyTurkish, s.Content.BodyGerman))
            ).ToList() ?? new List<NamedSection>();

            if (existing == null)
            {
                var aboutUs = AboutUs.Create(request.Key, request.Dto.Quote, request.Dto.QuoteAuthor, sections);
                await _repository.AddAsync(aboutUs, cancellationToken);
                return;
            }

            existing.Update(request.Dto.Quote, request.Dto.QuoteAuthor, sections);
            await _repository.UpdateAsync(existing, cancellationToken);
        }
    }
}

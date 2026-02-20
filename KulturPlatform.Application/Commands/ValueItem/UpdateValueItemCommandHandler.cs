using KulturPlatform.Application.Interfaces.ValueItem;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ValueItem
{
    public class UpdateValueItemCommandHandler
        : IRequestHandler<UpdateValueItemCommand, Unit>
    {
        private readonly IValueItemWriteRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateValueItemCommandHandler(
            IValueItemWriteRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            UpdateValueItemCommand request,
            CancellationToken cancellationToken)
        {
            // Aggregate load via WriteRepository
            var valueItem = await _writeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (valueItem == null)
                throw new KeyNotFoundException($"ValueItem not found. Id: {request.Id}");

            // Core content update
            valueItem.UpdateContent(
                Title.Create(request.TitleTr),
                Title.Create(request.TitleDe),
                Title.Create(request.SubtitleTr),
                Title.Create(request.SubtitleDe),
                new Description(request.IntroTr),
                new Description(request.IntroDe)
            );

            // Sections update
            var sections = BuildSections(request);
            valueItem.UpdateSections(sections);

            // Repository update (async)
            await _writeRepository.UpdateAsync(valueItem, cancellationToken);

            // UnitOfWork ile değişiklikleri kaydet
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        // Request → Domain Section mapping
        private static List<Section> BuildSections(UpdateValueItemCommand request)
        {
            var sections = new List<Section>();

            AddSection(sections, request.NameAndPurpose);
            AddSection(sections, request.Why);
            AddSection(sections, request.Who);
            AddSection(sections, request.How);

            return sections;
        }

        private static void AddSection(
            ICollection<Section> sections,
            SectionCommand? command)
        {
            if (command == null) return;

            var items = command.Items?
                .Select(i => new SectionItem(
                    Title.Create(i.TitleTr),
                    Title.Create(i.TitleDe),
                    i.Icon
                ))
                .ToList() ?? new List<SectionItem>();

            sections.Add(new Section(
                Title.Create(command.HeadingTr),
                Title.Create(command.HeadingDe),
                new Description(command.BodyTr),
                new Description(command.BodyDe),
                items
            ));
        }
    }
}

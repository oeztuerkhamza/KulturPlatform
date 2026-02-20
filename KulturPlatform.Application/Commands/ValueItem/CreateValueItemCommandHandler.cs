using KulturPlatform.Application.Interfaces.ValueItem;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ValueItem
{
    public class CreateValueItemCommandHandler
        : IRequestHandler<CreateValueItemCommand, Guid>
    {
        private readonly IValueItemWriteRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateValueItemCommandHandler(
            IValueItemWriteRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(
            CreateValueItemCommand request,
            CancellationToken cancellationToken)
        {
            // Sections map
            var sections = BuildSections(request);

            // Aggregate creation
            var valueItem = Domain.Commons.Aggregates.ValueItem.CreateNew(
                Title.Create(request.TitleTr),
                Title.Create(request.TitleDe),
                Title.Create(request.SubtitleTr),
                Title.Create(request.SubtitleDe),
                new Description(request.IntroTr),
                new Description(request.IntroDe),
                new DisplayOrder(0) // Default, UI’dan gelmiyorsa
            );

            // Sections ekle
            if (sections.Any())
                valueItem.UpdateSections(sections);

            // Repository’ye ekle
            await _writeRepository.AddAsync(valueItem, cancellationToken);
            // UnitOfWork ile değişiklikleri kaydet
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return valueItem.Id;
        }

        // SectionCommand → Section map
        private static List<Section> BuildSections(CreateValueItemCommand request)
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

using KulturPlatform.Application.Commands.Newsletter;
using KulturPlatform.Application.Interfaces.Newsletter;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Newsletter
{
    public class CreateNewsletterCampaignCommandHandler : IRequestHandler<CreateNewsletterCampaignCommand, Guid>
    {
        private readonly INewsletterCampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewsletterCampaignCommandHandler(
            INewsletterCampaignRepository campaignRepository,
            IUnitOfWork unitOfWork)
        {
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateNewsletterCampaignCommand request, CancellationToken cancellationToken)
        {
            // TODO: Get current user ID from claims
            var createdBy = Guid.Empty; // Replace with actual user ID

            var campaign = NewsletterCampaign.Create(
                Title.Create(request.Subject),
                new Description(request.ContentTr),
                new Description(request.ContentDe),
                createdBy,
                request.HeaderImageUrl,
                request.ScheduledAt);

            await _campaignRepository.AddAsync(campaign, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return campaign.Id;
        }
    }

    public class SendNewsletterCampaignCommandHandler : IRequestHandler<SendNewsletterCampaignCommand, (int successful, int failed)>
    {
        private readonly INewsletterService _newsletterService;

        public SendNewsletterCampaignCommandHandler(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public async Task<(int successful, int failed)> Handle(SendNewsletterCampaignCommand request, CancellationToken cancellationToken)
        {
            return await _newsletterService.SendCampaignAsync(request.CampaignId, cancellationToken);
        }
    }

    public class SendTestNewsletterCommandHandler : IRequestHandler<SendTestNewsletterCommand>
    {
        private readonly INewsletterService _newsletterService;

        public SendTestNewsletterCommandHandler(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public async Task Handle(SendTestNewsletterCommand request, CancellationToken cancellationToken)
        {
            await _newsletterService.SendTestEmailAsync(request.CampaignId, request.TestEmail, cancellationToken);
        }
    }

    public class DeleteNewsletterCampaignCommandHandler : IRequestHandler<DeleteNewsletterCampaignCommand>
    {
        private readonly INewsletterCampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNewsletterCampaignCommandHandler(
            INewsletterCampaignRepository campaignRepository,
            IUnitOfWork unitOfWork)
        {
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteNewsletterCampaignCommand request, CancellationToken cancellationToken)
        {
            await _campaignRepository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

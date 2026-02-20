using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteActivityCommandHandler(
            IActivityRepository activityRepository,
            IUnitOfWork unitOfWork)
        {
            _activityRepository = activityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _activityRepository.GetByIdAsync(request.Id, cancellationToken);
            if (activity == null)
                throw new KeyNotFoundException($"Activity with Id {request.Id} not found.");

            await _activityRepository.DeleteAsync(activity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

using KulturPlatform.Application.Interfaces.Activity;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly IActivityRepository _activityRepository;

        public DeleteActivityCommandHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _activityRepository.GetByIdAsync(request.Id, cancellationToken);
            if (activity == null)
                throw new KeyNotFoundException($"Activity with Id {request.Id} not found.");

            await _activityRepository.DeleteAsync(activity, cancellationToken);
        }
    }
}

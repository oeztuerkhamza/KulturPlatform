using KulturPlatform.Application.Interfaces.Satzung;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Queries.Satzung
{
    public class LöscheCreateSatzungHandler
    {
        private readonly ISatzungRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LöscheCreateSatzungHandler(ISatzungRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

    }

}

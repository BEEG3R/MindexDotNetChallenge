using challenge.Models;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService {
        private readonly ICompensationRepository compensationRepository;

        public CompensationService(ICompensationRepository compensationRepository) {
            this.compensationRepository = compensationRepository;
        }

        public Compensation Create(Compensation compensation) {
            if (compensation != null) {
                this.compensationRepository.Add(compensation);
                this.compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetByEmployeeId(string id) {
            if (string.IsNullOrEmpty(id)) {
                return null;
            }

            return this.compensationRepository.GetByEmployeeId(id);
        }
    }
}

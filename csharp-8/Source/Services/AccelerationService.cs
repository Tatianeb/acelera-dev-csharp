using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private readonly CodenationContext Context;
        public AccelerationService(CodenationContext context)
        {
            Context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId) =>
            Context.Accelerations
            .Where(x => x.Candidates.Any(y => y.CompanyId == companyId))
            .ToList();

        public Acceleration FindById(int id) =>
            Context.Accelerations
            .First(x => x.Id == id);

        public Acceleration Save(Acceleration acceleration)
        {
            if (acceleration.Id <= 0) Context.Accelerations.Add(acceleration);
            else Context.Accelerations.Update(acceleration);
            Context.SaveChanges();
            return acceleration;
        }
    }
}

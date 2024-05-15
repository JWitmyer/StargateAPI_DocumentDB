using MediatR;
using StargateAPI_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StargateAPI_BL 
{
    public class GetAllAstronautsDutyCommand : IRequest<List<AstronautDuty>>
    {

    }
    public class GetAllAstronautsDutyCommandHandler : IRequestHandler<GetAllAstronautsDutyCommand, List<AstronautDuty>>
    {
        readonly IRepository<AstronautDuty> _repo;
        public GetAllAstronautsDutyCommandHandler(IRepository<AstronautDuty> repo)
        {
            _repo = repo;
        }

        public async Task<List<AstronautDuty>> Handle(GetAllAstronautsDutyCommand command, CancellationToken cancellationToken)
        {
            return await _repo.GetAllEntitiesAsync<AstronautDuty>();
        }

    }
}


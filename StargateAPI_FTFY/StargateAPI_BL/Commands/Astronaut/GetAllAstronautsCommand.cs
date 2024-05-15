using MediatR;
using StargateAPI_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StargateAPI_BL
{
    public class GetAllAstronautsCommand : IRequest<List<Astronaut>>
    {

    }
    public class GetAllAstronautsCommandHandler : IRequestHandler<GetAllAstronautsCommand, List<Astronaut>>
    {
        readonly IRepository<Astronaut> _repo;
        public GetAllAstronautsCommandHandler(IRepository<Astronaut> repo)
        {
            _repo = repo;
        }

        public async Task<List<Astronaut>> Handle(GetAllAstronautsCommand command, CancellationToken cancellationToken) 
        {
            return await _repo.GetAllEntitiesAsync<Astronaut>();
        }

    }
}


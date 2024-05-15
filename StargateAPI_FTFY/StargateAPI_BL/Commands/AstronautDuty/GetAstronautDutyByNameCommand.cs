using MediatR;
using StargateAPI_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StargateAPI_BL 
{
    public class GetAstronautDutyByNameCommand : IRequest<Astronaut>
    {
        public string name {  get; set; }
    }
    public class GetAstronautDutyByNameCommandHandler : IRequestHandler<GetAstronautDutyByNameCommand, Astronaut>
    {
        readonly IRepository<Astronaut> _repo;
        public GetAstronautDutyByNameCommandHandler(IRepository<Astronaut> repo)
        {
            _repo = repo;
        }
        public async Task<Astronaut> Handle(GetAstronautDutyByNameCommand command, CancellationToken cancellationToken)
        {
            return await _repo.GetSingleEntityAsync<Astronaut>(x=> x.Name == command.name);
        }

    }
}


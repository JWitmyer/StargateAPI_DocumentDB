using MediatR;
using StargateAPI_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StargateAPI_BL 
{
    public class GetAstronautByNameCommand : IRequest<Astronaut>
    {
        public string name {  get; set; }
    }
    public class GetAstronautByNameCommandHandler : IRequestHandler<GetAstronautByNameCommand, Astronaut>
    {
        readonly IRepository<Astronaut> _repo;
        public GetAstronautByNameCommandHandler(IRepository<Astronaut> repo)
        {
            _repo = repo;
        }
        public async Task<Astronaut> Handle(GetAstronautByNameCommand command, CancellationToken cancellationToken)
        {
            return await _repo.GetSingleEntityAsync<Astronaut>(x=> x.Name == command.name);
        }

    }
}


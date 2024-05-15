using StargateAPI_DAL;
using MediatR;


namespace StargateAPI_BL
{
    public class CreateAstronautDutyCommand : IRequest
    {
        public AstronautDuty? astronautDuty { get; set; }
    }
    public class CreateAstronautDutyCommandHandler : IRequestHandler<CreateAstronautDutyCommand>
    {
        readonly IRepository<AstronautDuty> _repo;


        public CreateAstronautDutyCommandHandler(IRepository<AstronautDuty> repo)
        {
            _repo = repo;


        }
        public async Task Handle(CreateAstronautDutyCommand request, CancellationToken cancellationToken)
        {


            // Check for nulls or roslyn gonna get you
            if (request.astronautDuty == null)
            {
                throw new Exception("Astronaut Duty is null");

            }

            request.astronautDuty.PartitionKey = "e6f9300a-bb0a-4c92-ae19-9252fb687680"; //At this point could just be the string AstronautDuties Or AstronautDuty

            await _repo.AddAsync(request.astronautDuty);

            // A failure (e)  will bubble up. Also it is possible to return an id here but for time sake I'm just going with this.

        }
    }
}

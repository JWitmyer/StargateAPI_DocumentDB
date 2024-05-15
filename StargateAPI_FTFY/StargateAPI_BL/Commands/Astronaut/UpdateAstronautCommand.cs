using StargateAPI_DAL;
using MediatR;


namespace StargateAPI_BL
{
    public class UpdateAstronautCommand : IRequest
    {
        public Astronaut? astronaut { get; set; }
    }
    public class UpdateAstronautCommandHandler : IRequestHandler<UpdateAstronautCommand>
    {
        readonly IRepository<Astronaut> _repo;

        public UpdateAstronautCommandHandler(IRepository<Astronaut> repo)
        {
            _repo = repo;
        }
        public async Task Handle(UpdateAstronautCommand request, CancellationToken cancellationToken)
        {

            // Check for nulls or roslyn gonna get you 
            if (request.astronaut == null)
            {
                throw new Exception("Astronaut is null");
            }

            //Because this is reused there should be a better pattern out there for it. Doint this way makes me think the complexity will be too high in the long run. -JSW
            if (!request.astronaut.AstronautDuties.Any())// Could filter this out at the api layer validation stage.
            {
                throw new Exception("Astronaut does not have an astronaut duty assigned.");
            }

            if (request.astronaut.AstronautDuties.Count() > 1)// Could filter this out at the api layer validation stage.
            {
                throw new Exception("Astronaut can only have one active assignment");
            }

            var astronautDuty = request.astronaut.AstronautDuties.FirstOrDefault();
            if (astronautDuty != null && astronautDuty.DutyEndDate != null)
            {
                throw new Exception("Astronauts active assignment has an end date. Should be null");
            }

            request.astronaut.PartitionKey = "f60710cc-084b-4fd0-9291-d8f37d682121"; //At this point could just be the word Astronauts/ Also there has to be a better way of supplying this kind of thing

            await _repo.UpdateAsync(request.astronaut);

            // A failure (e)  will bubble up. Also it is possible to return an id here but for time sake I'm just going with this.

        }
    }
}



using Moq;
using StargateAPI_BL;
using StargateAPI_DAL;

namespace StargateAPI_Tests
{
    public class CreateAstronautDutyCommandTests  
    {
        public readonly IRepository<AstronautDuty> _repo;

        public CreateAstronautDutyCommandTests()
        {
            _repo = new Mock<IRepository<AstronautDuty>>().Object;

        }

        [Fact]
        public async void CreateAstronautDutyCommand_Handle_Fail_Pass()
        {
            var handler = new CreateAstronautDutyCommandHandler(_repo);
            AstronautDuty astronautTestDat = new AstronautDuty
            {
                DutyEndDate = null,
                DutyStartDate = DateTime.Now.AddDays(-100),
                DutyTitle = "RETIRED"
            };
            var exception = await Record.ExceptionAsync(() => handler.Handle(new CreateAstronautDutyCommand { astronautDuty = astronautTestDat }, CancellationToken.None));
            Assert.Null(exception);
        }

        [Fact]
        public async void CreateAstronautDutyCommand_Handle_Fail_AstronautDutyNull()
        {
            var handler = new CreateAstronautDutyCommandHandler(_repo);
            AstronautDuty astronautTestDat = null;
            var exception = await Record.ExceptionAsync(() => handler.Handle(new CreateAstronautDutyCommand { astronautDuty = astronautTestDat }, CancellationToken.None));
            Assert.Equal("Astronaut Duty is null", exception.Message);
        }
    }
    
}

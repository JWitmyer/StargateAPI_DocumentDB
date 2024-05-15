﻿
using Moq;
using NuGet.Frameworks;
using StargateAPI_BL;
using StargateAPI_DAL;
using System;
using Xunit.Sdk;

namespace StargateAPI_Tests { 
    public class CreateAstronautCommandTests  
    {
        public readonly IRepository<Astronaut> _repo;
        public CreateAstronautCommandTests()
        {
            _repo = new Mock<IRepository<Astronaut>>().Object;
 

        }

        [Fact]
        public async void CreateAstronautCommand_Handle_Pass()
        {
            var handler = new CreateAstronautCommandHandler(_repo);
            var astronautTestDat = new Astronaut
            {
                Name = "Buzz Aldrin",
                CareerEndDate = DateTime.Now,
                CurrentDutyTitle = "RETIRED",
                CurrentRank = "honorary brigadier general",
                AstronautDuties = new List<AstronautDuty>
                {
                    new AstronautDuty
                    {
                        DutyEndDate = null,
                        DutyStartDate = DateTime.Now,
                        DutyTitle = "RETIRED"
                    }
                }
            };
            var exception = await Record.ExceptionAsync(() => handler.Handle(new CreateAstronautCommand { astronaut = astronautTestDat }, CancellationToken.None));
            Assert.Null(exception);


        }
        [Fact]
        public async void CreateAstronautCommand_Handle_Fail_NoDutyEndDate()
        {
            var handler = new CreateAstronautCommandHandler(_repo);
            var astronautTestDat = new Astronaut
            {
                Name = "Buzz Aldrin",
                CareerEndDate = DateTime.Now,
                CurrentDutyTitle = "RETIRED",
                CurrentRank = "honorary brigadier general",
                AstronautDuties = new List<AstronautDuty>
                {
                    new AstronautDuty
                    {
                        DutyEndDate = DateTime.Now,
                        DutyStartDate = DateTime.Now,
                        DutyTitle = "RETIRED"
                    }
                }
            };
            var exception = await Record.ExceptionAsync(() => handler.Handle(new CreateAstronautCommand { astronaut = astronautTestDat }, CancellationToken.None));
            Assert.Equal("Astronauts active assignment has an end date. Should be null", exception.Message);
        }

        [Fact]
        public async void CreateAstronautCommand_Handle_Fail_TooManyDuties()
        {
            var handler = new CreateAstronautCommandHandler(_repo);
            var astronautTestDat = new Astronaut
            {
                Name = "Buzz Aldrin",
                CareerEndDate = DateTime.Now,
                CurrentDutyTitle = "RETIRED",
                CurrentRank = "honorary brigadier general",
                AstronautDuties = new List<AstronautDuty>
                {
                    new AstronautDuty
                    {
                        DutyEndDate = DateTime.Now,
                        DutyStartDate = DateTime.Now.AddDays(-100),
                        DutyTitle = "RETIRED"
                    },
                    new AstronautDuty
                    {
                        DutyEndDate = DateTime.Now,
                        DutyStartDate = DateTime.Now,
                        DutyTitle = "SpaceMan"
                    }
                }
            };
            var exception = await Record.ExceptionAsync(() => handler.Handle(new CreateAstronautCommand { astronaut = astronautTestDat }, CancellationToken.None));
            Assert.Equal("Astronaut can only have one active assignment", exception.Message);
        }

        [Fact]
        public async void CreateAstronautCommand_Handle_Fail_NoDuties()
        {
            var handler = new CreateAstronautCommandHandler(_repo);
            var astronautTestDat = new Astronaut
            {
                Name = "Buzz Aldrin",
                CareerEndDate = DateTime.Now,
                CurrentDutyTitle = "RETIRED",
                CurrentRank = "honorary brigadier general",
                AstronautDuties = new List<AstronautDuty>
                {
                    
                }
            };
            var exception = await Record.ExceptionAsync(() => handler.Handle(new CreateAstronautCommand { astronaut = astronautTestDat }, CancellationToken.None));
            Assert.Equal("Astronaut does not have an astronaut duty assigned.", exception.Message);
        }

        [Fact]
        public async void CreateAstronautCommand_Handle_Fail_AstronautNull()
        {
            var handler = new CreateAstronautCommandHandler(_repo);
            Astronaut astronautTestDat = null;
            var exception = await Record.ExceptionAsync(() => handler.Handle(new CreateAstronautCommand { astronaut = astronautTestDat }, CancellationToken.None));
            Assert.Equal("Astronaut is null", exception.Message);
        }
    }
}

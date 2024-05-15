using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StargateAPI_BL;
using StargateAPI_DAL;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StargateAPI
{
    public class Http_Triggers
    {
        private readonly IConfiguration _config;
        private readonly IMediator _mediatr;
        private readonly string _api;
        private readonly string _apiKey;
        public Http_Triggers(IConfiguration config, IMediator mediatr)
        {
            _config = config;// could check null here
            _mediatr = mediatr;
        }

        [FunctionName("GetAllAstronauts")]
        public async Task<IActionResult> GetAllAstronauts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Astronaut")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation($"Entry {nameof(GetAllAstronauts)}");
                var astronauts = await _mediatr.Send(new GetAllAstronautsCommand { });

                return new OkObjectResult(astronauts);

            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500

            }
        }

        [FunctionName("GetAstronautByName")]
        public async Task<IActionResult> GetAstronautByName(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Astronaut/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            try
            {
                log.LogInformation($"Entry Into {nameof(GetAstronautByName)}");

                var astronaut = await _mediatr.Send(new GetAstronautByNameCommand()
                {
                    name = name
                });

                return new OkObjectResult(astronaut);
            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500

            }
        }

        [FunctionName("CreateAstronaut")]
        public async Task<IActionResult> CreateAstronaut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Astronaut")] HttpRequest req,            
            ILogger log)
        {
            try
            {

                log.LogInformation($"Entry Into {nameof(CreateAstronaut)}");
                //Would do some validation on the json around here. 

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                Astronaut astronaut = JsonSerializer.Deserialize<Astronaut>(requestBody);

                await _mediatr.Send(new CreateAstronautCommand()
                {
                  astronaut = astronaut,
                });
            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500

            }

            return new OkResult();
        }

        [FunctionName("UpdateAstronaut")]
        public async Task<IActionResult> UpdateAstronaut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Astronaut")] HttpRequest req,            
            ILogger log)
        {
            try
            {
                log.LogInformation($"Entry Into {nameof(UpdateAstronaut)}");


                //Would do some validation on the json around here. (Fluent Validation plz)

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                Astronaut astronaut = JsonSerializer.Deserialize<Astronaut>(requestBody);

                await _mediatr.Send(new UpdateAstronautCommand { astronaut = astronaut });
            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500
            }

            return new OkResult();
        }

        [FunctionName("UpdateAstronautByName")]
        public async Task<IActionResult> UpdateAstronautByName(// This is probably unnessasary. 
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Astronaut/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            try
            {
                log.LogInformation($"Entry Into {nameof(CreateAstronaut)}");

                //Would do some validation on the json around here. (Fluent Validation plz)

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                Astronaut astronaut = JsonSerializer.Deserialize<Astronaut>(requestBody);

                await _mediatr.Send(new UpdateAstronautCommand { astronaut = astronaut });

            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500

            }

            return new OkResult();
        }


        [FunctionName("GetAllAstronautDuty")]
        public async Task<IActionResult> GetAllAstronautDuty(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "AstronautDuty")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation($"Entry Into {nameof(GetAllAstronautDuty)}");

                 
                var astronautsDuty = await _mediatr.Send(new GetAllAstronautsDutyCommand { });

                return new OkObjectResult(astronautsDuty);
            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500

            }
        }

        [FunctionName("GetAstronautDutyByName")]
        public async Task<IActionResult> GetAstronautDutyByName(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "AstronautDuty/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            try
            {
                log.LogInformation($"Entry Into {nameof(GetAstronautDutyByName)}");

                var astronautsDuty = await _mediatr.Send(new GetAstronautDutyByNameCommand()
                {
                    name = name
                });

                return new OkObjectResult(astronautsDuty);


            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500
            }
        }

        [FunctionName("CreateAstronautDuty")]
        public async Task<IActionResult> CreateAstronautDuty(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AstronautDuty")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation($"Entry Into {nameof(CreateAstronautDuty)}");
                //Would do some validation on the json around here. 

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                AstronautDuty astronautDuty = JsonSerializer.Deserialize<AstronautDuty>(requestBody);

                await _mediatr.Send(new CreateAstronautDutyCommand()
                {
                    astronautDuty = astronautDuty
                });
            }
            catch (Exception e) // At first we'll rely on the bubbling of exceptions would be nice to have custom exceptions -JSW
            {
                log.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);// could reuturn a 500 here. Effectively if it's in managed code it isn't a 500

            }

            return new OkResult();
        }
    }
}

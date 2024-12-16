using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Infrastructure;
using SimpleWebAPI.Models;

namespace SimpleWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BusController : Controller
    {
        private readonly BusRepository busRepository;
        public BusController()
        {
            /**
            * Created the current bus repository as a singleton because as far as the runtime environment cares, when I used a regular dictionary in this class it emptied it every time.
            * My guess is that it recreates this controller every time an HTTP command is received.
            * Therefore, used a singleton so that memory will be saved between the HTTP commands.
            */

            /** TODO: Switch this between <see cref="PostgresSQLRepository"/> And <see cref="RedisBusRepository"> */
            busRepository = RedisBusRepository.GetInstance();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status400BadRequest)]
        public ActionResult<Bus> Get(string id)
        {
            if (busRepository.Contains(id))
            {
                return Ok(busRepository.Get(id));
            }

            return BadRequest("Bus is missing");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status400BadRequest)]
        public ActionResult DeleteBus(string id)
        {
            busRepository.Delete(id);

            return NoContent();
        }

        [HttpPost("createEmptyBus")]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status400BadRequest)]
        public ActionResult<Bus> createDefaultBus([FromBody] BasicBusData basicBusData)
        {
            if (basicBusData == null)
            {
                return BadRequest("data is missing");
            }

            if (busRepository.Contains(basicBusData.ID))
            {
                return NotFound($"Bus with Id {basicBusData.ID} already exists");
            }

            Bus bus = new Bus(basicBusData.ID, basicBusData.driverName, basicBusData.Color, 0);

            busRepository.Update(bus);
            return CreatedAtAction(nameof(Get), new { id = bus.ID }, bus);
         }

        public class BasicBusData(string id, string driverName, string color) {
            private string id = id;
            public string ID {get {return id;} }
            public string driverName {get; set;} = driverName;
            public string Color {get; set;} = color;
        }

        [HttpPost("{id}/add-passenger")]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Bus), StatusCodes.Status400BadRequest)]
        public ActionResult<Bus> addPassengerToBus(string id)
        {
            // TODO: add 1 passenger to a bus if it exists, otherwise return NotFound Response with some message.

            if (!busRepository.Contains(id))
            {
                return NotFound($"Bus with Id {id} not found");
            }

            Bus bus = busRepository.Get(id);
            bus.PassengersCapacity++;

            busRepository.Update(bus);

            return Ok(bus);
        }
    }
}

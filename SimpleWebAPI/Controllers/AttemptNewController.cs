using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Models;

namespace SimpleWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AttemptNewController : Controller
    {
        private readonly DataRepository dataRepository;
        public AttemptNewController() 
        {
            dataRepository = DataRepository.GetInstance();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Data), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data), StatusCodes.Status400BadRequest)]
        public ActionResult<Data> Get(string id)
        {
            if (dataRepository.Contains(id))
            {
                return Ok(dataRepository.Get(id));
            }

            return BadRequest("Data is missing");
        }

        [HttpPost]
        [ProducesResponseType(typeof(Data), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Data> CreateData([FromBody] Data data)
        {
            if (data == null)
            {
                return BadRequest("Data is missing");
            }
        
            dataRepository.Add(data.ID, data);
            return CreatedAtAction(nameof(Get), new { id = data.ID }, data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Data), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data), StatusCodes.Status400BadRequest)]
        public ActionResult<Data> UpdateData(string id, [FromBody] Data data)
        {
            if (data == null)
            {
                return BadRequest("Data is missing");
            }
        
           
        
            if (!dataRepository.Contains(id))
            {
                return NotFound($"Data with Id {id} not found");
            }

            dataRepository.Add(id, data);
    
            return Ok(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Data), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data), StatusCodes.Status400BadRequest)]
        public ActionResult DeleteData(string id)
        {
            dataRepository.Remove(id);
        
            return NoContent();
        }
    }
}

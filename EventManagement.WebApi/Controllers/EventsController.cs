using EventManagement.Dal;
using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebApi.Controllers
{
    public class EventsController : Controller
    {
        private readonly ICommonRepository<Event> _repository;
        public EventsController(ICommonRepository<Event> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Event>> Get()
        {
            var events = _repository.GetAll();
            if (events.Count == 0)
                return NotFound();
            return Ok(events);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Event>> GetDetails(int id)
        {
            var eventItem = _repository.GetDetails(id);
            return eventItem == null ? NotFound() : Ok(eventItem);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Create(Event eventItem)
        {
            _repository.Insert(eventItem);
            var result = _repository.SaveChanges();
            if (result > 0)
                return CreatedAtAction("GetDetails", new { id = eventItem.Eventid }, eventItem);
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult Update(Event eventItem)
        {
            _repository.Update(eventItem);
            var result = _repository.SaveChanges();
            if (result > 0)
                return NoContent();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<Event> Delete(int id)
        {
            var eventItem = _repository.GetDetails(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            else
            {
                _repository.Delete(eventItem);
                _repository.SaveChanges();
                return NoContent();
            }
        }
    }
}

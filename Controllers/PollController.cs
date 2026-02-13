using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB_Demo.Models;
using MongoDB_Demo.Repository;

namespace MongoDB_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPolls()
        {
            var polls = await _pollRepository.GetAllPolls();
            return Ok(polls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPollById([FromRoute] string id)
        {
            var poll = await _pollRepository.GetPollById(id);
            if (poll == null)
            {
                return NotFound();
            }
            return Ok(poll);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll([FromBody] Poll poll)
        {
            await _pollRepository.CreatePoll(poll);
            return CreatedAtAction(nameof(GetPollById), new { id = poll.id }, poll);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoll([FromRoute] string id, [FromBody] Poll poll)
        {
            var existingPoll = await _pollRepository.GetPollById(id);
            if (existingPoll == null)
            {
                return NotFound();
            }
            await _pollRepository.UpdatePoll(id, poll);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoll([FromRoute] string id)
        {
            var existingPoll = await _pollRepository.GetPollById(id);
            if (existingPoll == null)
            {
                return NotFound();
            }
            await _pollRepository.DeletePoll(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB_Demo.Models;
using MongoDB_Demo.Repository;

namespace MongoDB_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {

        private readonly IOptionRepository _optionRepository;
        private readonly IPollRepository _pollRepository;

        public OptionController(IOptionRepository optionRepository, IPollRepository pollRepository)
        {
            _optionRepository = optionRepository;
            _pollRepository = pollRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOptions()
        {
            var options = await _optionRepository.GetAllOptions();
            return Ok(options);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOptionById([FromRoute] string id)
        {
            var option = await _optionRepository.GetOptionById(id);
            if (option == null)
            {
                return NotFound();
            }
            return Ok(option);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOption([FromBody] Option option)
        {
            var poll = await _pollRepository.GetPollById(option.PollId!);
            if (poll == null)
            {
                return NotFound($"Poll with id {option.PollId!} not found.");
            }
            await _optionRepository.CreateOption(option);
            return CreatedAtAction(nameof(GetOptionById), new { id = option.Id }, option);
        }

        [HttpPut]
        public async Task<IActionResult> VoteOption([FromRoute] string id)
        {
            var option = await _optionRepository.GetOptionById(id);
            if (option == null)
            {
                return NotFound();
            }
            option.Votes += 1;
            await _optionRepository.UpdateOption(id, option);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOption([FromRoute] string id, [FromBody] Option option)
        {
            var existingOption = await _optionRepository.GetOptionById(id);
            if (existingOption == null)
            {
                return NotFound();
            }
            await _optionRepository.UpdateOption(id, option);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption([FromRoute] string id)
        {
            var existingOption = await _optionRepository.GetOptionById(id);
            if (existingOption == null)
            {
                return NotFound();
            }
            await _optionRepository.DeleteOption(id);
            return NoContent();

        }
    }
}

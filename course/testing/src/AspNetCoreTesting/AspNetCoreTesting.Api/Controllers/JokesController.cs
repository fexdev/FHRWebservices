﻿using AspNetCoreTesting.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreTesting.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IJokeRepository _jokeRepository;

        public JokesController(IJokeRepository jokeRepository)
        {
            _jokeRepository = jokeRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Joke joke = await _jokeRepository.GetById(id);
            if (joke == null)
            {
                return NotFound();
            }

            return Ok(joke);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew([FromBody] Joke joke)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Joke result = await _jokeRepository.Add(joke);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Joke joke)
        {
            bool exists = _jokeRepository.GetById(id) != null;

            if (!exists)
            {
                return BadRequest();
            }

            Joke result = await _jokeRepository.Update(joke);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _jokeRepository.Delete(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdate(string id, [FromBody] JsonPatchDocument<Joke> doc)
        {
            Joke existing = await _jokeRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }

            doc.ApplyTo(existing);
            Joke result = await _jokeRepository.Update(existing);

            return Ok(result);
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomJoke()
        {
            return Ok(await _jokeRepository.GetRandomJoke());
        }
    }
}

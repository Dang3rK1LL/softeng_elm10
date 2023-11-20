using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elm10.JokeModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace elm10.Controllers
{
    [Route("api/jokes")]
    [ApiController]
    public class JokeController : ControllerBase
    {
        // GET: api/jokes
        [HttpGet]
        public IActionResult Get()
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            return Ok(context.Jokes.ToList());
        }

        // GET api/jokes/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            var keresettVicc = (from x in context.Jokes
                                where x.JokeSk == id
                                select x).FirstOrDefault();
            return Ok(keresettVicc);
        }

        // POST api/jokes
        [HttpPost]
        public void Post([FromBody] Joke újVicc)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            context.Jokes.Add(újVicc);
            context.SaveChanges();
        }

        // PUT api/jokes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Joke joke)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            var meglevoVicc = (from x in context.Jokes
                               where x.JokeSk == id
                               select x).FirstOrDefault();
            if (meglevoVicc == null)
                return NotFound($"Nincs #{id} azonosítóval vicc");
            else
            {
                meglevoVicc.JokeText = joke.JokeText;
                meglevoVicc.UpVotes = joke.UpVotes;
                meglevoVicc.DownVotes = joke.DownVotes;
                context.Jokes.Update(meglevoVicc);
                context.SaveChanges();
                return Ok(meglevoVicc);
            }
        }

        // DELETE api/jokes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            FunnyDatabaseContext context = new FunnyDatabaseContext();
            var törlendőVicc = (from x in context.Jokes
                                where x.JokeSk == id
                                select x).FirstOrDefault();
            context.Remove(törlendőVicc);
            context.SaveChanges();
        }
    }
}

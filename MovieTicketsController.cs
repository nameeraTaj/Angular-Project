using Fullstack.API.Data;
using Fullstack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MovieTicketsController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public MovieTicketsController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllMovieTickets()
        {
            var MovieTickets = await _fullStackDbContext.MovieTickets.ToListAsync();

            return Ok(MovieTickets);


        }
        [HttpPost]

        public async Task<IActionResult> AddMovieTickets([FromBody] MovieTicket movieTicketRequest)
        {
            movieTicketRequest.MovieId = Guid.NewGuid();

            await _fullStackDbContext.MovieTickets.AddAsync(movieTicketRequest);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(movieTicketRequest);
        
        }
        [HttpGet]
        [Route("{MovieId:guid}")]
        [ActionName("MovieTickets")]
        public async Task<IActionResult> GetMovieTickets([FromRoute] Guid MovieId)
        {
            var MovieTickets = await _fullStackDbContext.MovieTickets.FirstOrDefaultAsync(x => x.MovieId == MovieId);
            if (MovieTickets != null)
            {
                return Ok(MovieTickets);
            }
            return NotFound();
        }
        [HttpPut]
        [Route("{MovieId:guid}")]
        public async Task<IActionResult> UpdateMovieTickets([FromRoute] Guid MovieId, MovieTicket updateMovieTicketsRequest)
        {
            var MovieTicket = await _fullStackDbContext.MovieTickets.FindAsync(MovieId);
            if (MovieTicket == null)
            {
                return NotFound();
            }
            MovieTicket.Name = updateMovieTicketsRequest.Name;
            MovieTicket.PhoneNumber = updateMovieTicketsRequest.PhoneNumber;
            MovieTicket.MovieName = updateMovieTicketsRequest.MovieName;
            MovieTicket.Price = updateMovieTicketsRequest.Price;
            MovieTicket.SeatNumber = updateMovieTicketsRequest.SeatNumber;
            MovieTicket.AvailableTickets = updateMovieTicketsRequest.AvailableTickets;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(MovieTicket);
        }

        [HttpDelete]
        [Route("{MovieId:guid}")]
        public async Task<IActionResult> DeleteMovieTickets([FromRoute] Guid MovieId)
        {
            var MovieTickets = await _fullStackDbContext.MovieTickets.FindAsync(MovieId);
            if (MovieTickets == null)
            {
                return NotFound();
            }
            _fullStackDbContext.MovieTickets.Remove(MovieTickets);

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(MovieTickets);
        }

    }
}


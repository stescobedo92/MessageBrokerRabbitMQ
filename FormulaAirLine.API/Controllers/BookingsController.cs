using Microsoft.AspNetCore.Mvc;
using FormulaAirLine.API.Services;
using FormulaAirLine.API.Models;

namespace FormulaAirLine.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;
    private readonly IMessageProducer _messageProducer;

    //In-memory db
    public static readonly List<Booking> _bookings = new(); 

    public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult CreatingBooking(Booking newBooking)
    {
        if(!ModelState.IsValid) return BadRequest();

        _bookings.Add(newBooking);

        _messageProducer.SendingMessage<Booking>(newBooking);

        return Ok();
    }
}
using LabWork9.Contexts;
using LabWork9.Models;
using LabWork9.Services;

using AppDbContext context = new();

VisitorService visitorService = new(context);
TicketService ticketService = new(context);

//Task 2

var visitors = await visitorService.GetAsync();
var tickets = await ticketService.GetAsync();

foreach (var ticket in tickets)
    Console.WriteLine($"[{ticket.TicketId}] - {ticket.Row} ряд, {ticket.Seat}место. Принадлежит пользователю [{ticket.Visitor?.Name}]");

Console.WriteLine();

foreach (var visitor in visitors)
    Console.WriteLine($"{visitor.Name} - {visitor.Phone}");

//Task 3

Visitor newVisitor = new()
{
    Name = "Колян",
    Phone = "89526673424",
    BirthDate = DateTime.Now,
    Email = "kolya.naumov@gmail.com"
};

Ticket newTicket = new()
{
    SessionId = 3,
    VisitorId = 2,
    Row = 2,
    Seat = 8
};

await visitorService.AddAsync(newVisitor);
//await ticketService.AddAsync(newTicket);

//Task 4

Visitor updateVisitor = new()
{
    VisitorId = 11,
    Phone = "89242567833",
    BirthDate = DateTime.Now,
    Email = "misterEgor@gmail.com"
};

await visitorService.UpdateAsync(updateVisitor);
//await ticketService.UpdateAsync(newTicket);

//Task 5

await visitorService.DeleteAsync(20);
//await ticketService.DeleteAsync(12);

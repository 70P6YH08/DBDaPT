using LabWork10.Contexts;
using LabWork10.Filters;
using LabWork10.Pagination;
using LabWork10.Services;
using LabWork10.Sorts;


AppDbContext context = new();

FilmService filmService = new(context);
VisitorService visitorService = new(context);
TicketService ticketService = new(context);
GenreService genreService = new(context);

Paginate paginate = new()
{
    CurrentPage = 1,
    PageSize = 5,
};

Sort sort = new()
{
    isDescending = false,
    ColumnName = "Name",
};

FilmFilter filmFilter = new()
{
    NamePart = "п",
};


// Task 2
var tickets = await ticketService.GetAsync(paginate: paginate);
Console.WriteLine("Билеты:");
foreach (var ticket in tickets)
    Console.WriteLine($"[{ticket.SessionId}][{ticket.TicketId}] - {ticket.Row} ряд. {ticket.Seat} место.");

Console.WriteLine();

// Task 3
var visitors = await visitorService.GetAsync(sort: sort);
Console.WriteLine("Посетители:");
foreach (var visitor in visitors)
    Console.WriteLine($"{visitor.Name}, {visitor.Phone}");

Console.WriteLine();

// Task 4
var films = await filmService.GetAsync(filmFilter: filmFilter);
Console.WriteLine("Фильм:");
foreach (var film in films)
    Console.WriteLine($"{film.Name} {film.AgeLimit}+");

Console.WriteLine();

//Task 5

//DTO Ticket
var visitorsDto = await visitorService.GetDtoAsync();
Console.WriteLine("Билеты Dto:");
foreach (var visitorDto in visitorsDto)
    Console.WriteLine($"{visitorDto?.Phone} билетов: {visitorDto?.TicketsAmount}шт.");

Console.WriteLine();

//DTo FilmGenre
var filmGenreDtos = await filmService.GetFilmGenreDtoAsync();
Console.WriteLine("Жанры фильмов Dto:");
foreach (var filmGenreDto in filmGenreDtos)
{
    Console.Write($"[{filmGenreDto.FilmId}] - {filmGenreDto.FilmName} жанры: ");
    foreach (var genre in filmGenreDto.Genres)
        Console.Write($"{genre} ");
    Console.WriteLine();
}

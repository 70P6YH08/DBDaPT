using LabWork12.Contexts;
using LabWork12.Services;
using LabWork12.Sorts;


using CinemaDbContext context = new();

FilmService filmService = new(context);
SessionService sessionService = new(context);
TicketService ticketService = new(context);
VisitorService visitorService = new(context);

Sort sort = new()
{
    ColumnName = "Name",
    isDescending = false
};

// Task 1
var films = await filmService.GetAllOrderedAsync(sort);

foreach (var film in films)
    Console.WriteLine($"{film.Name} {film.Duration} минут {film.AgeLimit}+");

Console.WriteLine();

// Task 2
films = await filmService.GetByNameAndReleaseYearAsync("Самый лучший фильм", 2012);
foreach (var film in films)
    Console.WriteLine($"{film.Name} - {film.ReleaseYear} год.");

Console.WriteLine();

// Task 3
var result = await sessionService.IncreasePriceAsync(100, 8);
Console.WriteLine($"Изменено {result} строк");

Console.WriteLine();

// Task 4
var filmGenres = await filmService.GetFilmGenresByIdAsync(3);

foreach (var filmGenre in filmGenres)
    Console.WriteLine(filmGenre);

Console.WriteLine();

// Task 5
var sessionTimes = await ticketService.GetSessionDateByTicketIdAsync(10);
Console.WriteLine(sessionTimes);

Console.WriteLine();

// Task 6
char firstChar = 'д';
char secondChar = 'к';
films = await filmService.GetFilmStartWithRangeAsync(firstChar, secondChar);
foreach (var film in films)
    Console.WriteLine($"{film.Name} {film.Duration} минут {film.AgeLimit}+");

Console.WriteLine();

var minPrice = await sessionService.GetMinFilmPriceAsync(3);
var maxPrice = await sessionService.GetMaxFilmPriceAsync(3);
var avgPrice = await sessionService.GetAverageFilmPriceAsync(3);

Console.WriteLine($"min - {minPrice}, max - {maxPrice}, avg - {avgPrice}");

Console.WriteLine();

// Task 7
string number = "71234567899";
var ticket = await ticketService.GetTicketByPhone(number);
ticket.ForEach(t => Console.WriteLine($"{number} [{t.TicketId}] {t.Row} ряд, {t.Seat} место"));

Console.WriteLine();

// Task 8 
number = "79550776521";
var newVisitorId = await visitorService.AddVisitor(number);
Console.WriteLine($"id нового пользователя - {newVisitorId}");

Console.WriteLine();

//Task 9

var sessions = await sessionService.GetSessionsByFilmIdAsync(1);
foreach (var session in sessions)
    Console.WriteLine($"[{session.FilmId}] {session.SessionId} - {session.Price} руб");

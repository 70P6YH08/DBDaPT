using LabWork8;
using LabWork8.Models;
using LabWork8.Repository;
using System.Data;

//Task 1
DatabaseContext databaseContext = new("mssql", "ispp3106", "ispp3106", "3106");
using IDbConnection connection = databaseContext.CreateConnection();

connection.Open();

VisitorRepository visitorRepository = new(databaseContext);
GenreRepository genreRepository = new(databaseContext);

//Task 3
Visitor? visitor = await visitorRepository.GetByIdAsync(5);
Genre? genre = await genreRepository.GetByIdAsync(2);
Console.WriteLine($"Почта пользователя: {visitor.Email}\nЖанр: {genre.Name}");

var visitors = await visitorRepository.GetAllAsync();
var genres = await genreRepository.GetAllAsync();

//Task 4
Visitor newVisitor = new()
{
    Name = "Венедикт",
    BirthDate = DateTime.Now,
    Email = "voobschechetko@gmail.com",
    Phone = "89342567273"
};

Genre newGenre = new() { Name = "Документальный фильм" };

Console.WriteLine($"id нового пользователя - {await visitorRepository.AddAsync(newVisitor)}");
Console.WriteLine($"id нового жанра - {await genreRepository.AddAsync(newGenre)}");

//Task 5
await genreRepository.DeleteAsync(10);
await visitorRepository.DeleteAsync(18);

visitor.Name = "Егорик";
visitor.Email = "egorikkrut@mail.ru";

await visitorRepository.UpdateAsync(visitor);

genre.Name = "Вестерн";

await genreRepository.UpdateAsync(genre);
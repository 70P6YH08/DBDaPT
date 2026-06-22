namespace LabWork7
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Task 1
            Console.WriteLine(DataAccessLayer.ConnectionString);

            DataAccessLayer.ChangeConnectionSettings("mssql", "ispp3106", "ispp3106", "3106");
            Console.WriteLine(DataAccessLayer.ConnectionString);

            if (DataAccessLayer.TryConnection())
                Console.WriteLine("Подключение успешно");

            //Task 2
            Console.WriteLine($"Изменено строк: {await DataAccessLayer.UpdateRowsCommandAsync("UPDATE Game SET Price=1100 WHERE GameId = 11")}");
            Console.WriteLine(await DataAccessLayer.GetRowsCommandAsync("SELECT * FROM Game WHERE GameId = 10"));

            //Task 3
            await DataAccessLayer.ChangeSessionPriceAsync(350, 3);

            //Task 4 
            await DataAccessLayer.UploadFilmPosterAsync(3, "C:\\Temp\\ispp31\\DBDaPT\\LabWorks\\LabWork7\\real_guys.png");

            await DataAccessLayer.DownloadFilmPosterAsync(3, "C:\\Temp\\ispp31\\DBDaPT\\LabWorks\\LabWork7\\real_guys.png");

            //Task 5
            await DataAccessLayer.GetFilmsAsync();
        }
    }
}

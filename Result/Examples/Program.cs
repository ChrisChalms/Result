using System.Diagnostics;
using DB = CC.Result.Examples.ExampleStorageRepo;

namespace CC.Result.Examples
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Result.SetLogger(LogException);

            do
            {
                ReadInput()
                    .Bind(ParseInput)
                    .Map(Math.Abs)
                    .Map(GetMessage)
                    .SuccessInvoke(Console.WriteLine);
            } while(ShouldContinue());
        }

        static IResult<string> ReadInput()
        {
            Console.Write("Enter a game Id: ");
            return Result.Wrap(Console.ReadLine);
        }

        static IResult<int> ParseInput(string input) =>
            Result.Wrap(() => int.Parse(input), e => new Exception("Invalid input", e));

        static string GetMessage(int gameId) => SafeGetGame(gameId)
            .Filter(g => g.IsReleased)
            .Fold((_, game) => $"Game Id {game.Id}, Title: {game.Title}", "Game not found");

        static IResult<Game> SafeGetGame(int gameId) => Result.Wrap(() => DB.GetGameById(gameId));


        static bool ShouldContinue()
        {
            Console.WriteLine("\nPress <Esc> to exit, any other key to continue...\n");
            return Console.ReadKey(true).Key != ConsoleKey.Escape;
        }

        static void LogException(Exception e)
        {
            Console.WriteLine(e.Message);
            Debug.WriteLine(e);
        }
    }
}

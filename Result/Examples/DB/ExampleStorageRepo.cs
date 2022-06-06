namespace CC.Result.Examples
{
    public static class ExampleStorageRepo
    {
        static readonly List<Game> _gameList = new()
        {
            new Game{Id = 1, Title = "Deus Ex", IsReleased = true},
            new Game{Id = 2, Title = "Deus Ex: Invisible War", IsReleased = true },
            new Game{Id = 3, Title = "Deus Ex: Human Revolution", IsReleased = true },
            new Game{Id = 4, Title = "Deus Ex: Mankind Divided", IsReleased = true },
            new Game{Id = 5, Title = "Unreleased Masterpiece", IsReleased = false }
        };

        public static Game GetGameById(int id) => _gameList.FirstOrDefault(g => g.Id == id);
    }
}

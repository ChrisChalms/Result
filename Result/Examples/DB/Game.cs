namespace CC.Result.Examples
{
    public record Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsReleased { get; set; }
    }
}

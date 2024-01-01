namespace HackerRankAPI.Models
{
    public class HackerRankStory
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? By { get; set; }
        public DateTime Time { get; set; }
        public int? Score { get; set; }
        public int[] Kids { get; set; }
    }
}

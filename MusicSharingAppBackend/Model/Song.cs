namespace MusicSharingAppBackend.Model
{
    public class Song
    {
        public Song(string title, string duration, string description = "")
        {
            Title = title;
            Duration = new Duration(duration);
            Description = description;
        }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public Duration Duration { get; set; }
    }
}

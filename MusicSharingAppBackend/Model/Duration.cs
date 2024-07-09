namespace MusicSharingAppBackend.Model
{
    public class Duration
    {
        public Duration(string value)
        {
            _value = value;
            if(value != null)
            {
                seconds = int.Parse(value.Split(':')[0])*60 + int.Parse(value.Split(':')[1]);
            }
        }

        private string _value;
        private int seconds = 0;

        public int getTimeInSeconds()
        {
            return seconds;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}

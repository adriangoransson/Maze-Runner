public class Utils
{
    public static string SecondsToString(int seconds)
    {
        int minutes = seconds / 60;
        seconds = seconds % 60;

        string min = minutes > 9 ? minutes.ToString() : "0" + minutes;
        string sec = seconds > 9 ? seconds.ToString() : "0" + seconds;

        return min + ":" + sec;
    }
}
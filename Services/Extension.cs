namespace Services;

public static class Extension
{
    public static string ReplaceImageFormat(this string str)
            => str.Replace(".jpg", "")
                  .Replace(".JPG", "")
                  .Replace(".jpeg", "")
                  .Replace(".JPEG", "")
                  .Replace(".png", "")
                  .Replace(".PNG", "");
}

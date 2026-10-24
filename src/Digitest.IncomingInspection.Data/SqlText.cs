namespace Digitest.IncomingInspection.Data;


internal static class SqlText
{
    public static string? Contains(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var escaped = value.Trim()
            .Replace(@"\", @"\\")
            .Replace("%", @"\%")
            .Replace("_", @"\_")
            .Replace("[", @"\[");

        return "%" + escaped + "%";
    }
}

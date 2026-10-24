namespace Digitest.IncomingInspection.Core.Validation;

/// <summary>
/// Bir doğrulama hatası.
/// </summary>
/// <param name="Field">
/// Hatalı alanın adı. Form alanlarında özellik adıdır (ör. "LotNo"),
/// satırlarda "Items[7].InspectionType" biçimindedir.
/// </param>
/// <param name="Message">Kullanıcıya gösterilecek mesaj.</param>
public sealed record ValidationError(string Field, string Message)
{
    public const string ItemsPrefix = "Items";

    public static string ItemField(int rowNo, string property) => $"{ItemsPrefix}[{rowNo}].{property}";
}

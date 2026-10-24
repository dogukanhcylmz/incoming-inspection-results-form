namespace Digitest.IncomingInspection.Core.Validation;

///Veritabanındaki NVARCHAR 
public static class FieldLimits
{
    public const int ProjectNo = 50;
    public const int ProductName = 200;
    public const int LotNo = 100;
    public const int InventoryCode = 50;
    public const int QuantityUnit = 20;
    public const int TechnicalDrawingNo = 100;
    public const int TechnicalDrawingRev = 20;
    public const int Other = 500;

    public const int WaybillNo = 50;
    public const int PurchaseOrderNo = 50;
    public const int InventoryGroup = 100;
    public const int SupplierName = 200;

    public const int NcrText = 1000;
    public const int Notes = 1000;
    public const int InspectorName = 100;

    public const int InspectionType = 100;
    public const int AcceptCriteria = 200;
    public const int Applied = 200;
    public const int Statement = 500;

    public const int DocumentNo = 30;
    public const int DocRevision = 10;
}

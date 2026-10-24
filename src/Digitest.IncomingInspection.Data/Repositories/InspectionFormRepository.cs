using Dapper;
using Digitest.IncomingInspection.Core;
using Digitest.IncomingInspection.Core.Abstractions;
using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Data.Repositories;

/// InspectionForm ve InspectionItem tabloları için veri erişim 
public sealed class InspectionFormRepository : IInspectionFormRepository
{
    private const string FormColumns = @"
        Id, FormNo,
        ProjectNo, ProductName, LotNo, InspectionDate, InventoryCode, IncomingQuantity, QuantityUnit,
        TechnicalDrawingNo, TechnicalDrawingRev, Other,
        WaybillNo, PurchaseOrderNo, ArrivalDate, InventoryGroup, SupplierName,
        NcrText, Notes, InspectorName, OverallResult,
        DocumentNo, DocPublishDate, DocRevision, DocRevisionDate,
        CreatedAt, UpdatedAt";

    private const string ItemColumns = @"
        Id, FormId, RowNo, IsFixed, InspectionTypeTr, InspectionTypeEn,
        AcceptCriteria, Applied, Decision, Statement";

    private const string InsertItemSql = @"
        INSERT INTO dbo.InspectionItem
            (FormId, RowNo, IsFixed, InspectionTypeTr, InspectionTypeEn, AcceptCriteria, Applied, Decision, Statement)
        VALUES
            (@FormId, @RowNo, @IsFixed, @InspectionTypeTr, @InspectionTypeEn, @AcceptCriteria, @Applied, @Decision, @Statement);";

    private readonly SqlConnectionFactory _connectionFactory;

    public InspectionFormRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<IReadOnlyList<InspectionFormListItem>> SearchAsync(
        InspectionFormFilter filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        const string sql = @"
            SELECT TOP (@MaxRows)
                Id, FormNo, InspectionDate, SupplierName, ProductName, LotNo, PurchaseOrderNo,
                IncomingQuantity, QuantityUnit, InspectorName, OverallResult, CreatedAt, UpdatedAt
            FROM dbo.InspectionForm
            WHERE (@DateFrom IS NULL OR InspectionDate >= @DateFrom)
              AND (@DateTo   IS NULL OR InspectionDate <= @DateTo)
              AND (@FormNo   IS NULL OR FormNo       LIKE @FormNo   ESCAPE '\')
              AND (@Supplier IS NULL OR SupplierName LIKE @Supplier ESCAPE '\')
              AND (@Product  IS NULL OR ProductName  LIKE @Product  ESCAPE '\')
              AND (@LotNo    IS NULL OR LotNo        LIKE @LotNo    ESCAPE '\')
              AND (@Result   IS NULL OR OverallResult = @Result)
            ORDER BY InspectionDate DESC, Id DESC;";

        var parameters = new
        {
            MaxRows = Math.Clamp(filter.MaxRows, 1, 100_000),
            DateFrom = filter.DateFrom?.Date,
            DateTo = filter.DateTo?.Date,
            FormNo = SqlText.Contains(filter.FormNo),
            Supplier = SqlText.Contains(filter.SupplierName),
            Product = SqlText.Contains(filter.ProductName),
            LotNo = SqlText.Contains(filter.LotNo),
            Result = filter.Result.HasValue ? (byte?)filter.Result.Value : null
        };

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        var rows = await connection.QueryAsync<InspectionFormListItem>(
            new CommandDefinition(sql, parameters, cancellationToken: cancellationToken)).ConfigureAwait(false);

        return rows.AsList();
    }

    public async Task<InspectionForm?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var sql = $@"
            SELECT {FormColumns} FROM dbo.InspectionForm WHERE Id = @Id;
            SELECT {ItemColumns} FROM dbo.InspectionItem WHERE FormId = @Id ORDER BY RowNo;";

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        using var multi = await connection.QueryMultipleAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)).ConfigureAwait(false);

        var form = await multi.ReadSingleOrDefaultAsync<InspectionForm>().ConfigureAwait(false);
        if (form is null)
            return null;

        form.Items = (await multi.ReadAsync<InspectionItem>().ConfigureAwait(false)).AsList();
        return form;
    }

    public async Task<int> InsertAsync(InspectionForm form, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(form);

        const string nextNumberSql = @"
            DECLARE @Seq INT;

            UPDATE dbo.FormNumberCounter WITH (UPDLOCK, HOLDLOCK)
               SET @Seq = LastNo = LastNo + 1
             WHERE [Year] = @Year;

            IF @@ROWCOUNT = 0
            BEGIN
                INSERT INTO dbo.FormNumberCounter ([Year], LastNo) VALUES (@Year, 1);
                SET @Seq = 1;
            END;

            SELECT @Seq;";

        const string insertFormSql = @"
            INSERT INTO dbo.InspectionForm
            (
                FormNo,
                ProjectNo, ProductName, LotNo, InspectionDate, InventoryCode, IncomingQuantity, QuantityUnit,
                TechnicalDrawingNo, TechnicalDrawingRev, Other,
                WaybillNo, PurchaseOrderNo, ArrivalDate, InventoryGroup, SupplierName,
                NcrText, Notes, InspectorName, OverallResult,
                DocumentNo, DocPublishDate, DocRevision, DocRevisionDate
            )
            OUTPUT INSERTED.Id
            VALUES
            (
                @FormNo,
                @ProjectNo, @ProductName, @LotNo, @InspectionDate, @InventoryCode, @IncomingQuantity, @QuantityUnit,
                @TechnicalDrawingNo, @TechnicalDrawingRev, @Other,
                @WaybillNo, @PurchaseOrderNo, @ArrivalDate, @InventoryGroup, @SupplierName,
                @NcrText, @Notes, @InspectorName, @OverallResult,
                @DocumentNo, @DocPublishDate, @DocRevision, @DocRevisionDate
            );";

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var year = DateTime.Today.Year;
            var sequence = await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                nextNumberSql, new { Year = year }, transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);

            form.FormNo = $"{FormConstants.FormNoPrefix}-{year}-{sequence:0000}";

            var id = await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                insertFormSql, form, transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);

            await InsertItemsAsync(connection, transaction, id, form.Items, cancellationToken).ConfigureAwait(false);

            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            return id;
        }
        catch
        {
            await transaction.RollbackAsync(CancellationToken.None).ConfigureAwait(false);
            form.FormNo = string.Empty;
            throw;
        }
    }

    public async Task UpdateAsync(InspectionForm form, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(form);

        const string updateFormSql = @"
            UPDATE dbo.InspectionForm
               SET ProjectNo           = @ProjectNo,
                   ProductName         = @ProductName,
                   LotNo               = @LotNo,
                   InspectionDate      = @InspectionDate,
                   InventoryCode       = @InventoryCode,
                   IncomingQuantity    = @IncomingQuantity,
                   QuantityUnit        = @QuantityUnit,
                   TechnicalDrawingNo  = @TechnicalDrawingNo,
                   TechnicalDrawingRev = @TechnicalDrawingRev,
                   Other               = @Other,
                   WaybillNo           = @WaybillNo,
                   PurchaseOrderNo     = @PurchaseOrderNo,
                   ArrivalDate         = @ArrivalDate,
                   InventoryGroup      = @InventoryGroup,
                   SupplierName        = @SupplierName,
                   NcrText             = @NcrText,
                   Notes               = @Notes,
                   InspectorName       = @InspectorName,
                   OverallResult       = @OverallResult,
                   DocumentNo          = @DocumentNo,
                   DocPublishDate      = @DocPublishDate,
                   DocRevision         = @DocRevision,
                   DocRevisionDate     = @DocRevisionDate,
                   UpdatedAt           = SYSDATETIME()
             WHERE Id = @Id;";

        const string deleteItemsSql = "DELETE FROM dbo.InspectionItem WHERE FormId = @FormId;";

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var affected = await connection.ExecuteAsync(new CommandDefinition(
                updateFormSql, form, transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);

            if (affected == 0)
                throw new InvalidOperationException("Güncellenecek kayıt bulunamadı. Kayıt silinmiş olabilir.");

            await connection.ExecuteAsync(new CommandDefinition(
                deleteItemsSql, new { FormId = form.Id }, transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);

            await InsertItemsAsync(connection, transaction, form.Id, form.Items, cancellationToken).ConfigureAwait(false);

            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            await transaction.RollbackAsync(CancellationToken.None).ConfigureAwait(false);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM dbo.InspectionForm WHERE Id = @Id;"; 

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        var affected = await connection.ExecuteAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken)).ConfigureAwait(false);

        return affected > 0;
    }

    public async Task<AutoCompleteData> GetAutoCompleteDataAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT DISTINCT TOP (500) SupplierName   FROM dbo.InspectionForm WHERE SupplierName   <> N''      ORDER BY SupplierName;
            SELECT DISTINCT TOP (500) InventoryGroup FROM dbo.InspectionForm WHERE InventoryGroup IS NOT NULL ORDER BY InventoryGroup;
            SELECT DISTINCT TOP (100) QuantityUnit   FROM dbo.InspectionForm WHERE QuantityUnit   IS NOT NULL ORDER BY QuantityUnit;
            SELECT DISTINCT TOP (100) InspectorName  FROM dbo.InspectionForm WHERE InspectorName  IS NOT NULL ORDER BY InspectorName;";

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        using var multi = await connection.QueryMultipleAsync(
            new CommandDefinition(sql, cancellationToken: cancellationToken)).ConfigureAwait(false);

        var suppliers = (await multi.ReadAsync<string>().ConfigureAwait(false)).AsList();
        var groups = (await multi.ReadAsync<string>().ConfigureAwait(false)).AsList();
        var units = (await multi.ReadAsync<string>().ConfigureAwait(false)).AsList();
        var inspectors = (await multi.ReadAsync<string>().ConfigureAwait(false)).AsList();

        return new AutoCompleteData
        {
            Suppliers = suppliers,
            InventoryGroups = groups,
            QuantityUnits = units,
            Inspectors = inspectors
        };
    }

    private static async Task InsertItemsAsync(
        System.Data.Common.DbConnection connection,
        System.Data.Common.DbTransaction transaction,
        int formId,
        IEnumerable<InspectionItem> items,
        CancellationToken cancellationToken)
    {
        var list = items.OrderBy(i => i.RowNo).ToList();
        if (list.Count == 0)
            return;

        foreach (var item in list)
            item.FormId = formId;

        await connection.ExecuteAsync(new CommandDefinition(
            InsertItemSql, list, transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);
    }
}

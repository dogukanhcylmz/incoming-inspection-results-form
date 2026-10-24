using Dapper;
using Digitest.IncomingInspection.Core.Abstractions;
using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Data.Repositories;

///yeni formda hazır gelen sabit satırlar
public sealed class InspectionTemplateRepository : IInspectionTemplateRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public InspectionTemplateRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<IReadOnlyList<InspectionTemplateItem>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT RowNo, InspectionTypeTr, InspectionTypeEn, AcceptCriteria, IsActive
            FROM dbo.InspectionTemplateItem
            WHERE IsActive = 1
            ORDER BY RowNo;";

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        var rows = await connection.QueryAsync<InspectionTemplateItem>(
            new CommandDefinition(sql, cancellationToken: cancellationToken)).ConfigureAwait(false);

        return rows.AsList();
    }
}

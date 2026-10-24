using Dapper;
using Digitest.IncomingInspection.Core.Abstractions;
using Digitest.IncomingInspection.Core.Models;

namespace Digitest.IncomingInspection.Data.Repositories;

/// Doküman No, Yayın Tarihi, Revizyon, Revizyon Tarihi
public sealed class DocumentInfoRepository : IDocumentInfoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public DocumentInfoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<FormDocumentInfo> GetAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT DocumentNo, PublishDate, Revision, RevisionDate
            FROM dbo.FormDocumentInfo
            WHERE Id = 1;";

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        var info = await connection.QuerySingleOrDefaultAsync<FormDocumentInfo>(
            new CommandDefinition(sql, cancellationToken: cancellationToken)).ConfigureAwait(false);

        return info ?? new FormDocumentInfo();
    }

    public async Task SaveAsync(FormDocumentInfo info, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(info);

        const string sql = @"
            UPDATE dbo.FormDocumentInfo
               SET DocumentNo   = @DocumentNo,
                   PublishDate  = @PublishDate,
                   Revision     = @Revision,
                   RevisionDate = @RevisionDate,
                   UpdatedAt    = SYSDATETIME()
             WHERE Id = 1;

            IF @@ROWCOUNT = 0
                INSERT INTO dbo.FormDocumentInfo (Id, DocumentNo, PublishDate, Revision, RevisionDate)
                VALUES (1, @DocumentNo, @PublishDate, @Revision, @RevisionDate);";

        var parameters = new
        {
            DocumentNo = info.DocumentNo.Trim(),
            PublishDate = info.PublishDate.Date,
            Revision = info.Revision.Trim(),
            RevisionDate = info.RevisionDate?.Date
        };

        await using var connection = await _connectionFactory.OpenAsync(cancellationToken).ConfigureAwait(false);
        await connection.ExecuteAsync(new CommandDefinition(sql, parameters, cancellationToken: cancellationToken)).ConfigureAwait(false);
    }
}

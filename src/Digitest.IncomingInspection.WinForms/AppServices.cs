using Digitest.IncomingInspection.Core.Abstractions;
using Digitest.IncomingInspection.Core.Services;
using Digitest.IncomingInspection.Data;
using Digitest.IncomingInspection.Data.Repositories;
using Digitest.IncomingInspection.Reporting;

namespace Digitest.IncomingInspection.WinForms;

/// Uygulamanın bağımlılıklarını tek yerde kurar.
/// Ekranlar ihtiyaç duydukları servislere bu nesne üzerinden ulaşır.
internal sealed class AppServices
{
    public AppServices(AppSettings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));

        var connectionFactory = new SqlConnectionFactory(settings.ConnectionString);
        ConnectionDescription = connectionFactory.Description;

        Forms = new InspectionFormRepository(connectionFactory);
        var templates = new InspectionTemplateRepository(connectionFactory);

        FormService = new InspectionFormService(Forms, templates);
        DatabaseInitializer = new DatabaseInitializer(settings.ConnectionString);
        PdfExporter = new InspectionFormPdfExporter(new InspectionFormPdfGenerator(), settings.PdfOutputFolder);
    }

    public AppSettings Settings { get; }

    public string ConnectionDescription { get; }

    public IInspectionFormRepository Forms { get; }

    public InspectionFormService FormService { get; }

    public DatabaseInitializer DatabaseInitializer { get; }

    public InspectionFormPdfExporter PdfExporter { get; }
}

using System.Diagnostics.CodeAnalysis;
using Digitest.IncomingInspection.Core.Models;
using Digitest.IncomingInspection.Core.Validation;

namespace Digitest.IncomingInspection.Core.Services;

public sealed class SaveResult
{
    private SaveResult(InspectionForm? form, IReadOnlyList<ValidationError> errors)
    {
        Form = form;
        Errors = errors;
    }

    public InspectionForm? Form { get; }

    public IReadOnlyList<ValidationError> Errors { get; }

    [MemberNotNullWhen(true, nameof(Form))]
    public bool Success => Form is not null;

    public static SaveResult Ok(InspectionForm form) => new(form, Array.Empty<ValidationError>());

    public static SaveResult Invalid(IReadOnlyList<ValidationError> errors) => new(null, errors);
}
/// başarılıysa veritabanından yeniden okunmuş form değilse doğrulama hataları

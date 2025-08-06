using System;
using Application.Common;
using Domain.SearchableRepository;
using Flunt.Validations;
using MediatR;
using Shared.Core.Messaging;

namespace Application.Handlers.QueryHandlers.Studies.ListStudies;

public class ListStudiesInput : PaginatedListInput, IRequest<CommandResult>
{
    public string? AcNumber { get; set; }
    public string? InitialDate { get; set; }
    public string? FinalDate { get; set; }
    public string? StudyDescription { get; set; }
    public string? PatientId { get; set; }
    public string? Modality { get; set; }
    public string? PatientBirthdate { get; set; }
    public string? PatientName { get; set; }

    public ListStudiesInput(
       int page, int perPage, string search, string sort, SearchOrder dir,
       string? acNumber, string? initialDate, string? finalDate, string? studyDescription,
       string? patientId, string? modality, string? patientBirthdate, string? patientName
   ) : base(page, perPage, search, sort, dir)
    { }

    // Para não dar problema no binding na controller, no [FromRoute] no método List
    public ListStudiesInput() : base(1, 15, "", "", SearchOrder.Asc)
    { }

    public void Validate()
    {
        // Inserir validações caso necessário
        //AddNotifications(new Contract());
    }
}

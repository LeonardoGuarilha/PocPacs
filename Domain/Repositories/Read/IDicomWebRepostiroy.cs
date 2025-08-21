using Domain.Dicom;
using Domain.Entities.Image;
using Domain.Entities.Series;
using Domain.Entities.Study;
using Shared.Core.Result;

namespace Domain.Repositories.Read;

public interface IDicomWebRepostiroy
{
    Result<List<Study>> FindStudy(List<DicomField> dataset);
    Result<List<Series>> FindSeries(List<DicomField> dataset);
    Result<List<Image>> FindInstances(List<DicomField> dataset);
    Result<List<Image>> FindInstancesThumb(List<DicomField> dataset);
    Result<Image> FindInstance(List<DicomField> dataset);
}

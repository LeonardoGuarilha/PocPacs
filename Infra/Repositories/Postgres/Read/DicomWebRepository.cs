using Domain.Dicom;
using Domain.Entities.Image;
using Domain.Entities.Series;
using Domain.Entities.Study;
using Domain.Repositories.Read;

namespace Infra.Repositories.Postgres.Read;

public class DicomWebRepository : IDicomWebRepostiroy
{
    public Image FindInstance(List<DicomField> dataset)
    {
        throw new NotImplementedException();
    }

    public List<Image> FindInstances(List<DicomField> dataset)
    {
        throw new NotImplementedException();
    }

    public List<Image> FindInstancesThumb(List<DicomField> dataset)
    {
        throw new NotImplementedException();
    }

    public List<Series> FindSeries(List<DicomField> dataset)
    {
        throw new NotImplementedException();
    }

    public List<Study> FindStudy(List<DicomField> dataset)
    {
        throw new NotImplementedException();
    }
}

using System;
using Domain.Dicom;
using Domain.Entities.Image;
using Domain.Entities.Series;
using Domain.Entities.Study;

namespace Domain.Repositories.Read;

public interface IDicomWebRepostiroy
{
    List<Study> FindStudy(List<DicomField> dataset);
    List<Series> FindSeries(List<DicomField> dataset);
    List<Image> FindInstances(List<DicomField> dataset);
    List<Image> FindInstancesThumb(List<DicomField> dataset);
    Image FindInstance(List<DicomField> dataset);
}

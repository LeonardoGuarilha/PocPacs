using Domain.ValueObjects;
using Flunt.Validations;
using Shared.Core.DomainObjects;

namespace Domain.Entities.Image;

public class Image : EntityBase
{

    public string ImageUid { get; private set; }
    public StudyUidVO StudyUidFKey { get; private set; }

    public Image(string imageUid, StudyUidVO studyUidFkey)
    {
        ImageUid = imageUid;
        StudyUidFKey = studyUidFkey;

       AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty("ImageUid", ImageUid, "Id da imagem deve estar preenchido")
        );
    }

}

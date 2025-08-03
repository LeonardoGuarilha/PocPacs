using Domain.ValueObjects;
using Shared.Core.Messaging;

namespace Domain.Notifications;

public class ImageKeySetNotification : Event
{
    public string ImageUid { get; set; }
    public StudyUidVO StudyUid { get; set; }

    public ImageKeySetNotification(string imageUid, StudyUidVO studyUid)
    {
        ImageUid = imageUid;
        StudyUid = studyUid;
    }
}

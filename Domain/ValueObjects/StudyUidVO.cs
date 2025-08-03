using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.ValueObjects;

public class StudyUidVO : Notifiable
{
    public string StudyUid { get; set; }

    public StudyUidVO(string studyUid)
    {
        StudyUid = studyUid;

        AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty("StudyUid", StudyUid, "StudyUid não pode ser nul;")
        );
    }

}

using System;

namespace Domain.Dicom;

public class DicomField
{
    public readonly string Name;
    public readonly object Value;
    public readonly string VR;
    public readonly int Group;
    public readonly int Element;

    public DicomField(string name, object value, string vr = "", int group = 0, int element = 0)
    {
        Name = name;
        Value = value;
        VR = vr;
        Group = group;
        Element = element;
    }
}

using System;
using System.Text;
using Domain.Dicom;
using Infra.Enum;
using Infra.Repositories;

namespace Infra.Extensions;

public static class ListDicomFieldExtensions
{
    private static Dictionary<string, Func<DicomField, string>> GetMapping(QueryLevel level)
    {
        switch (level)
        {
            case QueryLevel.Study:
                return TableMappings.StudyTable;
            case QueryLevel.Series:
                return TableMappings.SeriesTable;
            case QueryLevel.Instance:
                return TableMappings.InstanceTable;
            default:
                throw new Exception("Nível de pesquisa não definido.");
        }
    }
    public static string GetConditions(this List<DicomField> fields, QueryLevel level)
    {
        var conditions = new StringBuilder();
        var mappings = GetMapping(level);


        Console.ForegroundColor = ConsoleColor.Magenta;
        //   Console.WriteLine("Parametros: ", Console.ForegroundColor);



        foreach (var field in fields)
        {
            Console.WriteLine(field.Name, Console.ForegroundColor);
            Console.WriteLine(!String.IsNullOrEmpty(field.Value?.ToString()) ? field.Value?.ToString() : "Não informado", Console.ForegroundColor);

            if (mappings.Count(tableItem => tableItem.Key == field.Name) > 0
                    && !String.IsNullOrEmpty(field.Value?.ToString()))
            {
                conditions.AppendFormat(mappings.First(tableItem => tableItem.Key == field.Name).Value(field));
            }
        }


        if (String.IsNullOrEmpty(conditions.ToString().Trim()))
        {
            switch (level)
            {
                case QueryLevel.Study:
                    return " AND STUDY_UID = '-1' ";
                case QueryLevel.Series:
                    return " AND SERIES_UID = '-1' ";
                case QueryLevel.Instance:
                    return " AND INSTANCEUID = '-1' ";
                default:
                    throw new Exception("Nível de pesquisa não definido.");
            }


        }
        return conditions.ToString();
    }
}

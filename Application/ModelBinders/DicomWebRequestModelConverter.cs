using System;
using Domain.Dicom;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.ModelBinders;

public class DicomWebRequestModelConverter : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        List<DicomField> fields = new List<DicomField>();

        if (bindingContext.HttpContext.Request.Query.Keys.Count > 0)
        {
            foreach (var key in bindingContext.HttpContext.Request.Query.Keys)
            {
                switch (key)
                {
                    case "fuzzymatching":
                    case "limit":
                    case "offset":
                    case "includefield":
                        {
                            //sem uso no momento
                        }
                        break;
                    default:
                        {
                            fields.Add(new DicomField(key, bindingContext.HttpContext.Request.Query[key][0]));
                        }
                        break;
                }
            }
        }

        bindingContext.Model = fields;
        bindingContext.Result = ModelBindingResult.Success(fields);

        return Task.CompletedTask;
    }
}

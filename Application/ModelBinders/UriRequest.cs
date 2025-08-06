using System;
using Application.Models.Interface;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.ModelBinders;

public class UriRequest : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
			if (bindingContext.ModelType != typeof(IWadoUriRequest))
			{
				bindingContext.Result = ModelBindingResult.Failed();
				return Task.CompletedTask;
			}

            if (new UriRequestConverter().TryParse(bindingContext.HttpContext.Request, out var result))
			{
				bindingContext.Model = result;
				bindingContext.Result = ModelBindingResult.Success(result);
			}
			else
			{
				bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Cannot convert value to Location");
				bindingContext.Result = ModelBindingResult.Failed();
			}
			return Task.CompletedTask;
    }
}

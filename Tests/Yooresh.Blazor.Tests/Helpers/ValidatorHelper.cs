﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yooresh.Village.Models;

namespace Yooresh.Village.Tests.Helpers;

public static class ValidationHelper
{
    public static string GetErrorMessage<TModel>(string propertyName, Type? attributeType = null) where TModel : ModelBase
    {
        if (attributeType == null)
            attributeType = typeof(RequiredAttribute);

        var property = typeof(TModel).GetProperty(propertyName);
        var attr = property?.GetCustomAttributes(attributeType).FirstOrDefault() as ValidationAttribute;       
        return attr?.ErrorMessage ?? "";
    }
}

using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public static class EnumHelper
{
    public static List<ComboDto> ToDtoList<TEnum>(params TEnum[] excludedValues) where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Where(e => !excludedValues.Contains(e))
                   .Select(e => new ComboDto
                   {
                       Id = Convert.ToInt32(e),
                       Description = GetDisplayName(e)
                   })
                   .ToList();
    }

    private static string GetDisplayName<TEnum>(TEnum value) where TEnum : Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
    }
}

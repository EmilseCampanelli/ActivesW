using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public static class EnumHelper
{
    public static List<ComboDto> ToDtoList<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Select(e => new ComboDto
                   {
                       Id = Convert.ToInt32(e), // o ((int)(object)e).ToString() si preferís el valor numérico
                       Descripcion = GetDisplayName(e)
                   })
                   .ToList();
    }

    private static string GetDisplayName<TEnum>(TEnum value) where TEnum : Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
    }
}

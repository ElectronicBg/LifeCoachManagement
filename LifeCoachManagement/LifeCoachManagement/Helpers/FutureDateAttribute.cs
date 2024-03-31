﻿using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime date)
        {
            return date > DateTime.Now;
        }
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format("Полето {0} трябва да бъде в бъдеще.", name);
    }
}

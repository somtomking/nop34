﻿using System;
using System.Globalization;
using FluentValidation;
using Nop.Admin.Models.Directory;
using Nop.Services.Localization;

namespace Nop.Admin.Validators.Directory
{
    public class CurrencyValidator : AbstractValidator<CurrencyModel>
    {
        public CurrencyValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Currencies.Fields.Name.Required"))
                .Length(1, 50).WithMessage(localizationService.GetResource("Admin.Configuration.Currencies.Fields.Name.Range"));
            RuleFor(x => x.CurrencyCode)
                .NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Currencies.Fields.CurrencyCode.Required"))
                .Length(1, 5).WithMessage(localizationService.GetResource("Admin.Configuration.Currencies.Fields.CurrencyCode.Range"));
            RuleFor(x => x.Rate)
                .GreaterThan(0).WithMessage(localizationService.GetResource("Admin.Configuration.Currencies.Fields.Rate.Range"));
            RuleFor(x => x.CustomFormatting)
                .Length(0, 50).WithMessage(localizationService.GetResource("Admin.Configuration.Currencies.Fields.CustomFormatting.Validation"));
            RuleFor(x => x.DisplayLocale)
                .Must(x =>
                {
                    try
                    {
                        if (String.IsNullOrEmpty(x))
                            return true;
                        var culture = new CultureInfo(x);
                        return culture != null;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage(localizationService.GetResource("Admin.Configuration.Currencies.Fields.DisplayLocale.Validation"));
        }
    }
}
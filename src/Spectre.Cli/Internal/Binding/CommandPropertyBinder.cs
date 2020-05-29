using System;
using Spectre.Cli.Exceptions;

namespace Spectre.Cli.Internal.Binding
{
    internal static class CommandPropertyBinder
    {
        public static CommandSettings CreateSettings(CommandValueLookup lookup, Type settingsType, ITypeResolver resolver)
        {
            var settings = CreateSettings(resolver, settingsType);

            foreach (var (parameter, value) in lookup)
            {
                parameter.Property.SetValue(settings, value);
            }

            // Validate the settings.
            var validationResult = settings.Validate();
            if (!validationResult.Successful)
            {
                throw RuntimeException.ValidationFailed(validationResult);
            }

            return settings;
        }

        private static CommandSettings CreateSettings(ITypeResolver resolver, Type settingsType)
        {
            if (resolver.Resolve(settingsType) is CommandSettings settings)
            {
                return settings;
            }

            throw ParseException.CouldNotCreateSettings(settingsType);
        }
    }
}

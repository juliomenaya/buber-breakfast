using ErrorOr;

using BuberBreakfast.Models;

namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        // Here we need to represent all errors that we are expecting.
        // For example a request a Breakfast that does not exist.
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found"
        );
        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be at least {Models.Breakfast.MinNameLength}" +
                         $"characters long and at most {Models.Breakfast.MaxNameLength}"
        );
    }
}
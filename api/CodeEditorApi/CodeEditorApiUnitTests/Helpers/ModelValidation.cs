using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodeEditorApiUnitTests.Helpers
{
    public static class ModelValidation
    {
        // Will return true if all properties on model are valid
        public static bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults.Count() == 0;
        }

        // Will return true if the specific proptery does not have an error, or false if it does have an error
        public static bool ValidateModel(object model, string property)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults.Where(x => x.MemberNames.Contains(property)).Count() == 0;
        }
    }
}

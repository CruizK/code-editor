namespace CodeEditorApi.Helpers
{
    public static class RegExp
    {
        /**
         * At least eight characters, but no more than twenty.
         * At least one lowercase letter.
         * At least one uppercase letter.
         * At least one number.
         * At least one special character: !, @, #, $, %, ^, &, *
         * Cannot be same as email.
         */
        public const string PasswordExpression = @"^(?!placeholder$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,20}$";

        /**
        * No *, &, !, @, _, or -
        * Does not start with a number.
        */
        public const string CourseTitleExpression = @"^(?![0-9])[^\*&!@_-]+$";
    }
}

using System;

namespace SeoAnalysis.Core.Exceptions
{
    public class ValidationException : Exception
    {

        #region " - - - - - - Constructors - - - - - - "

        public ValidationException(string message) : base(message)
        {
        }

        #endregion //Constructors

    } //ValidationException
}

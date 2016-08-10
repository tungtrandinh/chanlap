﻿using System;
using System.Collections.Generic;

namespace ChanLap.Common
{
    /// <summary>
    /// The custom exception for validation errors
    /// </summary>
    public class ChanLapValidationErrorsException
        : Exception
    {
        #region Properties

        IEnumerable<string> _validationErrors;
        /// <summary>
        /// Get or set the validation errors messages
        /// </summary>
        public IEnumerable<string> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of Application validation errors exception
        /// </summary>
        /// <param name="validationErrors">The collection of validation errors</param>
        public ChanLapValidationErrorsException(IEnumerable<string> validationErrors)
            : base("Invalid type, expected is RegisterTypesMapConfigurationElement")
        {
            _validationErrors = validationErrors;
        }

        #endregion
    }
}

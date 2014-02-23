using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Owlsure.Model
{
    /// <summary>
    /// Represents a counterparty. This class
    /// has built-in validation logic. It is wrapped
    /// by the CounterpartyViewModel class, which enables it to
    /// be easily displayed and edited by a WPF user interface.
    /// </summary>
    public class Counterparty : NotificationObject, IDataErrorInfo
    {
        #region Creation

        public static Counterparty CreateNewCounterparty()
        {
            return new Counterparty();
        }

        protected Counterparty() 
        {
        }

        #endregion

        #region State Properties

        /// <summary>
        /// Gets or Sets the counterparty Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the counterparty Name
        /// </summary>
        public string Name { get; set; }

        private string code;
        /// <summary>
        /// Gets or Sets the counterparty Code
        /// </summary>
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                RaisePropertyChanged("Code");
            }
        }

        #endregion

        #region IDataErrorInfo Members
        
        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return GetValidationError(propertyName); }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        static readonly string[] ValidatedProperties =
        {
            "Id",
            "Code",
            "Name"
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Code":
                    error = this.ValidateCode();
                    break;

                case "Name":
                    error = this.ValidateName();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on Counterparty: " + propertyName);
                    break;
            }

            return error;
        }

        private string ValidateCode()
        {
            if (IsStringMissing(this.Code))
            {
                return Strings.Counterparty_Error_MissingCode;
            }
            else if (IsCodeDuplicated(this.Code))
            {
                return Strings.Counterparty_Duplicate_Code;
            }
            else
            {
                return null;
            }
        }

        private string ValidateName()
        {
            if (IsStringMissing(this.Name))
            {
                return Strings.Counterparty_Error_MissingName;
            }
            else
            {
                return null;
            }
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        static bool IsCodeDuplicated(string value)
        {
            return false;
        }

        #endregion

    }
}

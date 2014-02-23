using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Owlsure.Model
{
    public class Usage : NotificationObject, IDataErrorInfo
    {
        const double upperLimit = 10.0E6;

        #region Creation

        public static Usage CreateNewUsage()
        {
            return new Usage();
        }

        protected Usage()
        {
        }

        #endregion

        #region State Properties

        /// <summary>
        /// Gets or Sets the usage Id
        /// </summary>
        int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        /// <summary>
        /// Gets or Sets the ExposureDate
        /// </summary>
        DateTime exposureDate;
        public DateTime ExposureDate 
        {
            get
            {
                return exposureDate;
            }
            set
            {
                exposureDate = value;
                RaisePropertyChanged("ExposureDate");
            }
        }

        /// <summary>
        /// Gets or Sets the exposure
        /// </summary>
        double exposure;
        public double Exposure 
        {
            get
            {
                return exposure;
            }
            set
            {
                exposure = value;
                RaisePropertyChanged("Exposure");
                RaisePropertyChanged("IsBreach");
            }
        }

        /// <summary>
        /// Gets or Sets the TradingLine
        /// </summary>
        string tradingLine;
        public string TradingLine 
        {
            get
            {
                return tradingLine;
            }
            set
            {
                tradingLine = value;
                RaisePropertyChanged("TradingLine");
            }
        }

        /// <summary>
        /// Gets exposure breach or not
        /// </summary>
        public bool IsBreach
        {
            get
            {
                return exposure > upperLimit;
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
            "ExposureDate",
            "Exposure",
            "TradingLine"
        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case "Id":
                    error = this.ValidateId();
                    break;

                case "Exposure":
                    error = this.ValidateExposure();
                    break;

                case "ExposureDate":
                    error = this.ValidateExposureDate();
                    break;

                case "TradingLine":
                    error = this.ValidateTradingLine();
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on Usage: " + propertyName);
                    break;
            }

            return error;
        }

        private string ValidateId()
        {
            return null;
        }

        private string ValidateExposure()
        {
            if (this.Exposure > upperLimit)
            {
                return "This exposure is too much!";
            }
            else if (this.Exposure < 0)
            {
                return "The exposure cannot be negative!";
            }
            else
            {
                return null;
            }
        }

        private string ValidateTradingLine()
        {
            return null;
        }

        private string ValidateExposureDate()
        {
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        #endregion

    }
}

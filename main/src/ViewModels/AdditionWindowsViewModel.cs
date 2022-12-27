/**
* @file
* @brief The class for the View-Model of the AdditionWindow.
*/

using System;


namespace SimpleWeightManager.ViewModels
{
    /// <summary>
    /// The class for the View-Model of the AdditionWindow.
    /// </summary>
    public class AdditionWindowViewModel : AbstractViewModel
    {
        /// <summary>
        /// The class for the View-Model of the AdditionWindow.
        /// </summary>
        public AdditionWindowViewModel()
        {
            this.TargetDate = System.DateTime.Today;
            this.Height            = "0.0";
            this.Weight            = "0.0";
            this.Weight2Aim        = "0.0";
            this.BodyFatPercentage = "0";
            this.Notes             = "";
        }

        /// <value>The target date which the user wants to register.</value>
        private DateTime targetDate;

        /// <value>The target date which the user wants to register.</value>
        public DateTime TargetDate
        {
            get
            {
                return targetDate;
            }
            set
            {
                this.targetDate = value;
                this.NotifyPropertyChanged( "TargetDate" );
            }
        }

        /// <value>The height which the user is.</value>
        private string height;

        /// <value>The height which the user is.</value>
        public string Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
                this.NotifyPropertyChanged( "Height" );
            }
        }

        /// <value>The weight which the user has.</value>
        private string weight;

        /// <value>The weight which the user has.</value>
        public string Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
                this.NotifyPropertyChanged( "weight" );
            }
        }

        /// <value>The body fat percentage which the user has.</value>
        private string bodyFatPercentage;

        /// <value>The body fat percentage which the user has.</value>
        public string BodyFatPercentage
        {
            get
            {
                return this.bodyFatPercentage;
            }
            set
            {
                this.bodyFatPercentage = value;
                this.NotifyPropertyChanged( "BodyFatPercentage" );
            }
        }

        /// <value>The weight which the user aims to.</value>
        private string weight2aim;

        /// <value>The weight which the user aims to.</value>
        public string Weight2Aim
        {
            get
            {
                return this.weight2aim;
            }
            set
            {
                this.weight2aim = value;
                this.NotifyPropertyChanged( "Weight2Aim" );
            }
        }

        /// <value>The notes.</value>
        private string notes;

        /// <value>The notes.</value>
        public string Notes
        {
            get
            {
                return this.notes;
            }
            set
            {
                this.notes = value;
                this.NotifyPropertyChanged( "Notes" );
            }
        }
    }
}

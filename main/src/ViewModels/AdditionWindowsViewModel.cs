using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleWeightManager
{
    namespace ViewModels
    {
        public class AdditionWindowViewModel : AbstractViewModel
        {
            public AdditionWindowViewModel()
            {
                this.TargetDate = System.DateTime.Today;
                this.Height            = "0.0";
                this.Weight            = "0.0";
                this.Weight2Aim        = "0.0";
                this.BodyFatPercentage = "0";
                this.Notes             = "";
            }

            private DateTime targetDate;
            public DateTime TargetDate
            {
                get
                {
                    return targetDate;
                }
                set
                {
                    this.targetDate = value;
                    // 通知する
                    this.NotifyPropertyChanged( "TargetDate" );
                }
            }

            private string height;
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

            private string weight;
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

            private string bodyFatPercentage;
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

            private string weight2aim;
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

            private string notes;
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
}

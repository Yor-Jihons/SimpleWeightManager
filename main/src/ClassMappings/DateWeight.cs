using System.Data.Common;
using System.Text;
using System;

namespace SimpleWeightManager
{
    namespace ClassMappings
    {
        public class DateWeight : IComparable
        {
            /// <summary>
            /// To create the object of this class, with the factory pattern.
            /// </summary>
            /// <param name="date">An object of the class DateTime.</param>
            /// <param name="height">The height data.</param>
            /// <param name="weight">The weight data.</param>
            /// <param name="weight2aim">The weight the user aims.</param>
            /// <returns>An object of this class.</returns>
            public static DateWeight Create( System.DateTime date, string height, string weight, string weight2aim )
            {
                var res = new DateWeight();
                res.Year       = date.Year;
                res.Month      = date.Month;
                res.Day        = date.Day;
                res.Height     = height;
                res.Weight     = weight;
                res.Weight2Aim = weight2aim;
            return res;
            }

            public static DateWeight CreateAsDummy()
            {
                var res = new DateWeight();
                res.Year       = 0;
                res.Month      = 0;
                res.Day        = 0;
                res.Height     = "0.0";
                res.Weight     = "0.0";
                res.Weight2Aim = "0.0";
            return res;
            }

            /// <summary>
            /// To calculate the BMI.
            /// </summary>
            /// <param name="dateWeight">The object of this class.</param>
            /// <returns>The string as a BMI text.</returns>
            public static string CalcBMI( ClassMappings.DateWeight dateWeight )
            {
                if( dateWeight == null ) return "";

                // 計算式: BMI値 = 体重(kg) ÷ { 身長(m) × 身長(m) }
                // 日本医師会HPより
                double weight = Double.Parse( dateWeight.Weight );
                double height = Double.Parse( dateWeight.Height ) / (double)100.0;
                double bmi = weight / (height * height);
            return bmi.ToString( "F2" );
            }

            /// <summary>
            /// To Calculate the best weight.
            /// </summary>
            /// <param name="dateWeight">An object of this class.</param>
            /// <returns></returns>
            public static string CalcBestWeight( ClassMappings.DateWeight dateWeight )
            {
                // 計算式: 適正体重(kg) = 身長(m) × 身長(m) × 22
                // 日本医師会HPより
                double height = Double.Parse( dateWeight.Height ) / (double)100.0;
                double bestWeight = height * height * 22;
                var builder = new StringBuilder();
                builder.Append( bestWeight.ToString( "F2" ) );
                builder.Append( "kg" );
            return builder.ToString();
            }

            /// <summary>
            /// Constructor.
            /// </summary>
            private DateWeight()
            {

            }

            public override string ToString()
            {
                var text = this.ToDateString( DateWeightDateType.ForDataCard ) + "\n" + this.ToHeightString() + "\n" + this.ToWeightString();
            return text;
            }

            /// <summary>
            /// To make the date string.
            /// </summary>
            /// <param name="type">The data of the enum DateWeightDateType.</param>
            /// <returns>The date string.</returns>
            public string ToDateString( ClassMappings.DateWeightDateType type )
            {
                var d = new System.DateTime( this.Year, this.Month, this.Day );
                string format = (type == DateWeightDateType.ForGraph ? "M/d" : "yyyy/M/d dddd");
            return d.ToString( format );
            }

            /// <summary>
            /// To compare other the object of this class.
            /// </summary>
            /// <param name="obj">The object of this class.</param>
            /// <returns></returns>
            public int CompareTo( object obj )
            {
                if( obj == null ) return 1;

                var target = (obj as DateWeight);

                int y = DateWeight.Compare( this.Year, target.Year );
                if( y != 0 ) return y;

                int m = DateWeight.Compare( this.Month, target.Month );
                if( m != 0 ) return m;

                int d = DateWeight.Compare( this.Day, target.Day );
            return d;
            }

            public string ToHeightString()
            {
                const string UNIT = "cm";
                if( this.Height.Equals( string.Empty ) ) return ("0.0" + UNIT);
            return (this.Height + UNIT);
            }

            public string ToWeightString()
            {
                const string UNIT = "kg";
                if( this.Weight.Equals( string.Empty ) ) return ("0.0" + UNIT);
            return (this.Weight + UNIT);
            }

            public string DifferenceFromGoal()
            {
                double w1 = Double.Parse( this.Weight2Aim );
                double w2 = Double.Parse( this.Weight );
                int ans = w1.CompareTo( w2 );
                var builder = new StringBuilder();
                if( w1 == 0.0 || ans == 0 ){
                    builder.Append( "おめでとうございます！ 目標達成です！" );
                    return builder.ToString();
                }

                builder.Append( "目標まであと " );
                if( ans > 0 ) // w1の方が大きい場合は「＋」をつける
                {
                    builder.Append( "＋" );
                }
                builder.Append( (w1 - w2).ToString() );
                builder.Append( "kg！" );
            return builder.ToString();
            }

            private static int Compare( int n, int m )
            {
                if( n > m ) return  1;
                if( n < m ) return -1;
            return 0;
            }

            [System.Xml.Serialization.XmlElement("years")]
            public int Year{ get; set; } = 0;

            [System.Xml.Serialization.XmlElement("month")]
            public int Month{ get; set; } = 0;

            [System.Xml.Serialization.XmlElement("day")]
            public int Day{ get; set; } = 0;

            [System.Xml.Serialization.XmlElement("height")]
            public string Height{ get; set; } = "0.0";

            [System.Xml.Serialization.XmlElement("weight")]
            public string Weight{ get; set; } = "0.0";

            [System.Xml.Serialization.XmlElement("weight2aim")]
            public string Weight2Aim{ get; set; } = "0.0";
        }
    }
}
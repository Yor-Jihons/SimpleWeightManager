/**
* @file
* @brief The class to manage the class DateWeightInfo.
*/

using System.Text;
using System;

namespace SimpleWeightManager.ClassMappings
{
    /// <summary>
    /// The class to manage the class DateWeightInfo.
    /// </summary>
    public class DateWeightManager
    {
        /// <summary>
        /// Create the object of this class (as a factory).
        /// </summary>
        /// <param name="filepath">The file path.</param>
        /// <returns>The object of this class.</returns>
        public static DateWeightManager Create( string filepath )
        {
            if( !System.IO.File.Exists( filepath ) )
            {
                return null;
            }
            var infos = DateWeightInfo.Load( filepath );
        return new DateWeightManager( infos, filepath );
        }

        /// <summary>
        /// Create the object of this class, as a new object, which has no data.
        /// </summary>
        /// <param name="filepath">The file path.</param>
        /// <returns>The object of this class, as a new object, which has no data.</returns>
        public static DateWeightManager CreateAsNew( string filepath )
        {
            var info = DateWeightInfo.CreateAsNew();
        return new DateWeightManager( info, filepath );
        }

        /// <summary>
        /// Constructor. This is defined to hide.
        /// </summary>
        /// <param name="infos">The object of the class DateWeightInfo.</param>
        /// <param name="filepath">The file path.</param>
        private DateWeightManager( DateWeightInfo infos, string filepath )
        {
            this.infos    = infos;
            this.filepath = filepath;
        }

        /// <summary>
        /// Cut the Dummy data. This object sometimes has some dummy data.
        /// </summary>
        public void CutDummyData()
        {
            for( int i = 0; i < this.infos.DateWeights.Count; i++ )
            {
                System.Windows.MessageBox.Show( infos.DateWeights[i].ToString() );
            }
        }

        /// <summary>
        /// Fectch the data, from the XML.
        /// </summary>
        /// <returns>The data, from the XML, which is to show to the graph.</returns>
        public GraphElements.GraphElement Fetch()
        {
            if( this.infos.DateWeights.Count <= 0 ) return null;

            int n = this.infos.DateWeights.Count;

            var weights            = new double[n];
            var bodyFatPercentages = new double[n];
            var ticks              = new double[n];
            var xticks             = new string[n];

            int i = 0;
            foreach( var d in this.infos.DateWeights )
            {
                bodyFatPercentages[i] = DateWeightManager.ConvertBodyFatPercentageString2Int( d.BodyFatPercentage );
                weights[i]            = DateWeightManager.ConvertWeightString2Int( d.Weight );
                ticks[i]              = i;
                xticks[i]             = d.ToDateString( ClassMappings.DateWeightDateType.ForGraph, "" );
                i++;
            }
        return new GraphElements.GraphElement( bodyFatPercentages, weights, ticks, xticks );
        }

        /// <summary>
        /// Fectch the latest Weight.
        /// </summary>
        /// <returns>The lastest Weight.</returns>
        public DateWeight FetchLatestWeight()
        {
            DateWeight newWeight = null;
            if( this.infos.DateWeights.Count >= 1 )
                newWeight  = this.infos.DateWeights[ this.infos.DateWeights.Count - 1 ];
        return newWeight;
        }

        /// <summary>
        /// Fetch the previous Weight.
        /// </summary>
        /// <returns>The previous Weight.</returns>
        public DateWeight FetchPrevWeight()
        {
            DateWeight prevWeight = null;
            if( this.infos.DateWeights.Count >= 2 )
                prevWeight  = this.infos.DateWeights[ this.infos.DateWeights.Count - 2 ];
        return prevWeight;
        }

        /// <summary>
        /// Whether this object is empty or not.
        /// </summary>
        /// <returns>Returns false if this object has some data, otherwise returns true.</returns>
        public bool IsEmpty()
        {
            return (this.infos.DateWeights.Count == 0 ? true : false);
        }

        /// <summary>
        /// Save the data, which this object manages, to the XML file.
        /// </summary>
        public void Save()
        {
            DateWeightInfo.Save( this.infos, this.filepath );
        }

        /// <summary>
        /// Whether this object has the data or not.
        /// </summary>
        /// <param name="dateWeight">The target date.</param>
        /// <param name="pos">Push the index, which is placed in the list of data, if this object has.</param>
        /// <returns>Returns true if this object has, otherwise returns false.</returns>
        public bool Has( DateWeight dateWeight, out int pos )
        {
            var comparer = new DateWeightComparer();
            pos = this.infos.DateWeights.BinarySearch( dateWeight, comparer );
        return ( pos >= 0 ? true : false);
        }

        public void Add( DateWeight dateWeight )
        {
            this.infos.DateWeights.Add( dateWeight );
            this.infos.DateWeights.Sort( (a, b) => a.CompareTo(b) );
        }

        public bool Edit( int pos, DateWeight dateWeight )
        {
            if( pos <= -1 || pos >= this.infos.DateWeights.Count ) return false;
            this.infos.DateWeights[ pos ] = dateWeight;
        return true;
        }

        public void Clear()
        {
            this.infos.DateWeights.Clear();
        }

        public int Count()
        {
            return this.infos.DateWeights.Count;
        }

        public string ToAimString()
        {
            if( this.infos.DateWeights.Count <= 0 ) return "";

            string wa = this.infos.DateWeights[ this.infos.DateWeights.Count - 1 ].Weight2Aim;
            if( wa.Equals( string.Empty ) || wa.Equals( "0.0" ) ) return "";

            var builder = new StringBuilder();
            builder.Append( "目標体重: " );
            builder.Append( wa );
            builder.Append( "kg" );
        return builder.ToString();
        }

        public string ToMessageString()
        {
            if( this.infos.DateWeights.Count <= 0 ) return "";
            var d = this.infos.DateWeights[ this.infos.DateWeights.Count - 1 ];
        return d.DifferenceFromGoal();
        }

        private static double ConvertWeightString2Int( string weight )
        {
            return Double.Parse( weight );
        }

        private static double ConvertBodyFatPercentageString2Int( string weight )
        {
            return Double.Parse( weight );
        }

        public static bool CreateBool4IsBodyFatPercentageShowed( DateWeightManager manager )
        {
            return (manager.infos.IsBodyFatPercentageShowed == 0 ? false : true);
        }

        public static int CreateInt4BodyFatPercentageShowed( bool state )
        {
            return (state ? 1 : 0);
        }

        public bool IsBodyFatPercentageShowed
        {
            get
            {
                return (this.infos.IsBodyFatPercentageShowed == 0 ? false : true);
            }
            set
            {
                this.infos.IsBodyFatPercentageShowed = (value ? 1 : 0);
            }
        }

        private DateWeightInfo infos;
        private string filepath;
    }
}

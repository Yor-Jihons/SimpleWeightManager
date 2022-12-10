using System.Text;
using System.ComponentModel;
using System;

namespace SimpleWeightManager
{
    namespace ClassMappings
    {
        public class DateWeightManager
        {
            public static DateWeightManager Create( string filepath )
            {
                if( !System.IO.File.Exists( filepath ) )
                {
                    return null;
                }
                var infos = DateWeightInfo.Load( filepath );
            return new DateWeightManager( infos, filepath );
            }

            public static DateWeightManager CreateAsNew( string filepath )
            {
                var info = DateWeightInfo.CreateAsNew();
            return new DateWeightManager( info, filepath );
            }

            private DateWeightManager( DateWeightInfo infos, string filepath )
            {
                this.infos    = infos;
                this.filepath = filepath;
            }

            public void CutDummyData()
            {
                for( int i = 0; i < this.infos.DateWeights.Count; i++ )
                {
                    System.Windows.MessageBox.Show( infos.DateWeights[i].ToString() );
                }
            }

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

            public DateWeight FetchLatestWeight()
            {
                DateWeight newWeight = null;
                if( this.infos.DateWeights.Count >= 1 )
                    newWeight  = this.infos.DateWeights[ this.infos.DateWeights.Count - 1 ];
            return newWeight;
            }

            public DateWeight FetchPrevWeight()
            {
                DateWeight prevWeight = null;
                if( this.infos.DateWeights.Count >= 2 )
                    prevWeight  = this.infos.DateWeights[ this.infos.DateWeights.Count - 2 ];
            return prevWeight;
            }

            public bool IsEmpty()
            {
                return (this.infos.DateWeights.Count == 0 ? true : false);
            }

            public void Save()
            {
                DateWeightInfo.Save( this.infos, this.filepath );
            }

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
                // TODO: チェックして処理
                return Double.Parse( weight );
            }

            private static double ConvertBodyFatPercentageString2Int( string weight )
            {
                // TODO: チェックして処理
                return Double.Parse( weight );
            }

            private DateWeightInfo infos;
            private string filepath;
        }
    }
}

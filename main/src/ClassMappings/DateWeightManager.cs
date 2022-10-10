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

            public void Fetch( double[] xs, double[] ys, string[] xticks )
            {
                if( this.infos.DateWeights.Count <= 0 ) return;

                int i = 0;
                foreach( var d in this.infos.DateWeights )
                {
                    xs[i]     = i;
                    ys[i]     = DateWeightManager.ConvertWeightString2Int( d.Weight );
                    xticks[i] = d.ToDateString( ClassMappings.DateWeightDateType.ForGraph );
                    i++;
                }
            }

            public void FetchWeights( out DateWeight newWeight, out DateWeight prevWeight )
            {
                if( this.infos.DateWeights.Count >= 1 )
                    newWeight  = this.infos.DateWeights[ this.infos.DateWeights.Count - 1 ];
                else
                    newWeight  = null;

                if( this.infos.DateWeights.Count >= 2 )
                    prevWeight  = this.infos.DateWeights[ this.infos.DateWeights.Count - 2 ];
                else
                    prevWeight  = null;
            }

            public bool IsEmpty()
            {
                return (this.infos.DateWeights.Count == 0 ? true : false);
            }

            public void Save()
            {
                DateWeightInfo.Save( this.infos, this.filepath );
            }

            public void Add( DateWeight dateWeight )
            {
                this.infos.DateWeights.Add( dateWeight );
                this.infos.DateWeights.Sort( (a, b) => b.CompareTo(a) );
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

            private DateWeightInfo infos;
            private string filepath;
        }
    }
}

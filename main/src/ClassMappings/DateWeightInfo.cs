using System;

namespace SimpleWeightManager
{
    namespace ClassMappings
    {
        [System.Xml.Serialization.XmlRoot("dateweights")]
        public class DateWeightInfo
        {
            public static DateWeightInfo Load( string filepath )
            {
                var fs = new System.IO.FileStream( filepath, System.IO.FileMode.Open );
                var serializer = new System.Xml.Serialization.XmlSerializer( typeof(ClassMappings.DateWeightInfo) );
                var res = (ClassMappings.DateWeightInfo)serializer.Deserialize( fs );
                fs.Close();
            return res;
            }

            public static DateWeightInfo CreateAsNew()
            {
                var datewight = new DateWeightInfo();
                datewight.DateWeights = new System.Collections.Generic.List<DateWeight>();
                datewight.Notes = "";
            return datewight;
            }

            public static void Save( ClassMappings.DateWeightInfo infos, string filepath )
            {
                var fs = new System.IO.FileStream( filepath, System.IO.FileMode.Create );
                var serializer = new System.Xml.Serialization.XmlSerializer( typeof(ClassMappings.DateWeightInfo) );
                serializer.Serialize( fs, infos );
                fs.Close();
            }

            private DateWeightInfo()
            {

            }

            [System.Xml.Serialization.XmlElement("isbodyfatpercentageshowed")]
            public int IsBodyFatPercentageShowed{ get; set; } = 1;

            [System.Xml.Serialization.XmlElement("datewight")]
            public System.Collections.Generic.List<DateWeight> DateWeights{ get; set;}

            [System.Xml.Serialization.XmlElement("notes")]
            public string Notes{ get; set; }
        }
    }
}
/**
* @file
* @brief The class to contain the data, with the XML-file. (main)
*/

namespace SimpleWeightManager.ClassMappings
{
    /// <summary>
    /// The class to contain the data, with the XML-file. (main)
    /// </summary>
    [System.Xml.Serialization.XmlRoot("dateweights")]
    public class DateWeightInfo
    {
        /// <summary>
        /// Create the object of this class (as a factory).
        /// </summary>
        /// <param name="filepath">The file path.</param>
        /// <returns>The object of this class.</returns>
        public static DateWeightInfo Load( string filepath )
        {
            var fs = new System.IO.FileStream( filepath, System.IO.FileMode.Open );
            var serializer = new System.Xml.Serialization.XmlSerializer( typeof(ClassMappings.DateWeightInfo) );
            var res = (ClassMappings.DateWeightInfo)serializer.Deserialize( fs );
            fs.Close();
        return res;
        }

        /// <summary>
        /// Create the object of this class, as a new object, which has no data.
        /// </summary>
        /// <returns>The object of this class, as a new object, which has no data.</returns>
        public static DateWeightInfo CreateAsNew()
        {
            var datewight = new DateWeightInfo();
            datewight.DateWeights = new System.Collections.Generic.List<DateWeight>();
            datewight.Notes = "";
        return datewight;
        }

        /// <summary>
        /// Save the object of this class as a file.
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="filepath"></param>
        public static void Save( ClassMappings.DateWeightInfo infos, string filepath )
        {
            var fs = new System.IO.FileStream( filepath, System.IO.FileMode.Create );
            var serializer = new System.Xml.Serialization.XmlSerializer( typeof(ClassMappings.DateWeightInfo) );
            serializer.Serialize( fs, infos );
            fs.Close();
        }

        /// <summary>
        /// Constructor. This is defined to hide.
        /// </summary>
        private DateWeightInfo()
        {

        }

        /// <value>Whether body-fat-percentage show or not.</value>
        [System.Xml.Serialization.XmlElement("isbodyfatpercentageshowed")]
        public int IsBodyFatPercentageShowed{ get; set; } = 1;

        /// <value>The list of the class DateWeight which this program manages.</value>
        [System.Xml.Serialization.XmlElement("datewight")]
        public System.Collections.Generic.List<DateWeight> DateWeights{ get; set;}

        /// <value>The notes.</value>
        [System.Xml.Serialization.XmlElement("notes")]
        public string Notes{ get; set; }
    }
}
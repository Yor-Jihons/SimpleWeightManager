namespace SimpleWeightManager
{
    namespace ClassMappings
    {
        /// <summary>
        /// The class in order to compare the objects of the class DateWeight, for the method DateWeightManager.Has.
        /// </summary>
        public class DateWeightComparer : System.Collections.Generic.IComparer<DateWeight>
        {
            /// <summary>
            /// To compare the objects of the class DateWeight.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare( DateWeight x, DateWeight y )
            {
                return x.CompareTo( y );
            }
        }
    }
}

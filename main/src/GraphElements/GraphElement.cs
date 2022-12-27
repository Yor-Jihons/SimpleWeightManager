/**
* @file
* @brief The class to contain the data which is for the graph.
*/

namespace SimpleWeightManager.GraphElements
{
    /// <summary>
    /// The class to contain the data which is for the graph.
    /// </summary>
    public class GraphElement
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bodyFatPercentages">The body fat percentages.</param>
        /// <param name="weights">The weights.</param>
        /// <param name="ticks">The ticks.</param>
        /// <param name="xticks">The xticks.</param>
        public GraphElement( double[] bodyFatPercentages, double[] weights, double[] ticks, string[] xticks )
        {
            this.BodyFatPercentages  = bodyFatPercentages;
            this.Weights             = weights;
            this.Ticks               = ticks;
            this.Xticks              = xticks;
        }

        /// <value>The body fat percentages.</value>
        public double[] BodyFatPercentages{ get; private set; }

        /// <value>The weights.</value>
        public double[] Weights{ get; private set; }

        /// <value>The ticks..</value>
        public double[] Ticks{ get; private set; }

        /// <value>The xticks..</value>
        public string[] Xticks{ get; private set; }
    }
}
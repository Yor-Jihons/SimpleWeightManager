namespace SimpleWeightManager
{
    namespace GraphElements
    {
        public class GraphElement
        {
            public GraphElement( double[] bodyFatPercentages, double[] weights, double[] ticks, string[] xticks )
            {
                this.BodyFatPercentages  = bodyFatPercentages;
                this.Weights             = weights;
                this.Ticks               = ticks;
                this.Xticks              = xticks;
            }

            public double[] BodyFatPercentages{ get; private set; }
            public double[] Weights{ get; private set; }
            public double[] Ticks{ get; private set; }
            public string[] Xticks{ get; private set; }
        }
    }
}
namespace SimpleWeightManager
{
    namespace GraphElements
    {
        public class GraphElement
        {
            public GraphElement( double[] xs, double[] ys, string[] xticks )
            {
                this.Xs     = xs;
                this.Ys     = ys;
                this.Xticks = xticks;
            }

            public double[] Xs{ get; private set; }
            public double[] Ys{ get; private set; }
            public string[] Xticks{ get; private set; }
        }
    }
}
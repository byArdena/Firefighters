namespace FireSystem.Electricity
{
    public class Electricity
    {
        private readonly ElectricityPoint[] Points;
        
        public Electricity(ElectricityPoint[] points)
        {
            Points = points;
        }

        public void TurnOff()
        {
            foreach (ElectricityPoint point in Points)
                point.SetInteraction(true); 
        }
    }
}
namespace HealthSystem
{
    public class StandardHealth : IHealth
    {
        private readonly float Value;
        
        public StandardHealth(float value)
        {
            Value = value;
        }
        
        public float GetHealth()
        {
            return Value;
        }
    }
}
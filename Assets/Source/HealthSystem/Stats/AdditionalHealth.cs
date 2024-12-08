namespace HealthSystem
{
    public class AdditionalHealth : IHealth
    {
        private readonly IHealth Health;
        private readonly float Value;

        public AdditionalHealth(IHealth health, float value)
        {
            Health = health;
            Value = value;
        }
        
        public float GetHealth()
        {
            return Health.GetHealth() + Value;
        }
    }
}
namespace HealthSystem
{
    public class Reducer : IDamageable
    {
        private readonly IDamageable Damageable;
        private readonly float Reduce;

        public Reducer(IDamageable damageable, float reduce)
        {
            Damageable = damageable;
            Reduce = reduce;
        }
        
        public float CalculateDamage(float damage)
        {
            return Damageable.CalculateDamage(damage) * Reduce;
        }
    }
}
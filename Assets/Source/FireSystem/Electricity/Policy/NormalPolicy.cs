namespace FireSystem.Electricity
{
    public class NormalPolicy : IElectricityPolicy
    {
        public bool CanInteract()
        {
            return true;
        }
    }
}
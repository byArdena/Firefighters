namespace FireSystem.Electricity
{
    public class ElectricityPresenter
    {
        private readonly Electricity Model;
        private readonly ElectricitySwitcher Switcher;
        
        public ElectricityPresenter(Electricity model, ElectricitySwitcher switcher)
        {
            Model = model;
            Switcher = switcher;
        }

        public void Enable()
        {
            Switcher.TurningOff += OnTurningOff;
        }

        public void Disable()
        {
            Switcher.TurningOff -= OnTurningOff;
        }

        private void OnTurningOff()
        {
            Model.TurnOff();
        }
    }
}
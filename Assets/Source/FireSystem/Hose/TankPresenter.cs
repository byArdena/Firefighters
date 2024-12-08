namespace FireSystem
{
    public class TankPresenter
    {
        private readonly Tank Model;
        private readonly ParticlePlayer Hose;
        private readonly TankFiller Filler;
        private readonly TankBar Bar;
        private readonly GroupSwitcher Button;

        public TankPresenter(Tank model, ParticlePlayer hose, TankFiller filler, TankBar bar, GroupSwitcher button)
        {
            Model = model;
            Hose = hose;
            Filler = filler;
            Bar = bar;
            Button = button;
        }

        public void Enable()
        {
            Model.Filled += OnFilled;
            Model.Emptied += OnEmptied;
            
            Hose.Played += OnPlayed;
            Filler.GoingFill += OnGoingFill;
        }

        public void Disable()
        {
            Model.Filled -= OnFilled;
            Model.Emptied -= OnEmptied;
            
            Hose.Played -= OnPlayed;
            Filler.GoingFill -= OnGoingFill;
        }

        private void OnPlayed()
        {
            Model.ThrowOut();
            Bar.Decrease();
        }

        private void OnFilled()
        {
            Hose.AllowPlay();
            Bar.Maximize();
        }
        
        private void OnEmptied()
        {
            Hose.DisallowPlay();
            Bar.Flash();
            Button.Hide();
        }

        private void OnGoingFill()
        {
            Model.FillUp(LiquidType.Water);
            Button.Show();
        }
    }
}
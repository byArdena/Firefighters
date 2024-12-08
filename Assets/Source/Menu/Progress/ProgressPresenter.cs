using System;
using Saves;
using Sounds;
using TMPro;

namespace Menu
{
    public class ProgressPresenter
    {
        private readonly Progress Model;
        private readonly IProgressionBar[] Bars;
        private readonly SoundChanger SoundChanger;
        private readonly TextMeshProUGUI Money;
        private readonly TextMeshProUGUI Level;
        private readonly TextMeshProUGUI Energy;

        public ProgressPresenter(Progress model, IProgressionBar[] bars, SoundChanger soundChanger,
            TextMeshProUGUI money, TextMeshProUGUI level, TextMeshProUGUI energy)
        {
            Model = model;
            Bars = bars;
            SoundChanger = soundChanger;
            Money = money;
            Level = level;
            Energy = energy;
        }

        public void Enable()
        {
            Model.Loaded += OnLoaded;
            Model.MoneyChanged += OnMoneyChanged;
            Model.EnergyReceived += OnEnergyReceived;
            Model.LevelIncreased += OnLevelIncreased;
            
            foreach (IProgressionBar bar in Bars)
                bar.Bought += OnBought;
            
            SoundChanger.MasterChanged += OnMasterChanged;
            SoundChanger.MusicChanged += OnMusicChanged;
        }

        public void Disable()
        {
            Model.Loaded -= OnLoaded;
            Model.MoneyChanged -= OnMoneyChanged;
            Model.EnergyReceived -= OnEnergyReceived;
            Model.LevelIncreased -= OnLevelIncreased;
            
            foreach (IProgressionBar bar in Bars)
                bar.Bought -= OnBought;
            
            SoundChanger.MasterChanged -= OnMasterChanged;
            SoundChanger.MusicChanged -= OnMusicChanged;

            Model.Save();
        }

        private void OnLoaded(IReadOnlyCharacteristics characteristics)
        {
            Bars[(int)PurchaseNames.Speed].Initialize(characteristics.Speed);
            Bars[(int)PurchaseNames.Health].Initialize(characteristics.Health);
            Bars[(int)PurchaseNames.Resistance].Initialize(characteristics.Resistance);
            Bars[(int)PurchaseNames.Water].Initialize(characteristics.Water);
            
            SoundChanger.Load(characteristics.MasterVolume, characteristics.MusicVolume);
            OnLevelIncreased(characteristics.PassedLevelsCount);
            OnEnergyReceived(characteristics.EnergyCount, characteristics.MaxEnergyCount);
            OnMoneyChanged(characteristics.MoneyCount);
        }

        private void OnMoneyChanged(int moneyCount)
        {
            foreach (IProgressionBar bar in Bars)
                bar.CompareMoney(moneyCount);

            Money.SetText(moneyCount.ToString());
        }

        private void OnEnergyReceived(int current, int max)
        {
            Energy.SetText($"{current}/{max}");
        }

        private void OnLevelIncreased(int levelCount)
        {
            Level.SetText(levelCount.ToString());
        }
        
        private void OnBought(int spendCount, PurchaseNames progression, object value)
        {
            switch (progression)
            {
                case PurchaseNames.Speed:
                    Model.SetSpeed(value);
                    break;

                case PurchaseNames.Health:
                    Model.SetHealth(value);
                    break;
                
                case PurchaseNames.Resistance:
                    Model.SetResistance(value);
                    break;

                case PurchaseNames.Water:
                    Model.SetWater(value);
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(progression), progression, null);
            }

            Model.ChangeMoney(-spendCount);
        }
        
        private void OnMasterChanged(float volume)
        {
            Model.ChangeMasterVolume(volume);
        }
        
        private void OnMusicChanged(float volume)
        {
            Model.ChangeMusicVolume(volume);
        }
    }
}
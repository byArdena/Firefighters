using Menu;
using TaskBars;

namespace Level
{
    public class GamePresenter
    {
        private readonly Progress Model;
        private readonly Game Game;
        private readonly TaskBoard TaskBoard;

        public GamePresenter(Progress model, Game game, TaskBoard taskBoard)
        {
            Model = model;
            Game = game;
            TaskBoard = taskBoard;
        }

        public void Enable()
        {
            Game.GettingReward += OnGettingReward;
            TaskBoard.Won += OnWinning;
            TaskBoard.Lost += OnLosing;
        }

        public void Disable()
        {
            Game.GettingReward -= OnGettingReward;
            TaskBoard.Won -= OnWinning;
            TaskBoard.Lost -= OnLosing;
            
            Model.Save();
        }
        
        private void OnWinning()
        {
            Game.Win();
        }
        
        private void OnLosing()
        {
            Game.Lose();
        }

        private void OnGettingReward(int value)
        {
            Model.ChangeMoney(value);
            Model.IncreaseLevel();
        }
    }
}
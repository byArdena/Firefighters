namespace TaskBars
{
    public interface IProgress
    {
        public void Report(int currentProgress, int maxProgress);

        public void Win();

        public void Lose();
    }
}
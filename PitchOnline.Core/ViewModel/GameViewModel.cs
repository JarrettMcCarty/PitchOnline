using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;

namespace PitchOnline.Core
{
    public class GameViewModel : BaseViewModel
    {
        private int score = 0;
        public int Score 
        {
            get { return score; }
            set { score = value; OnPropertyChanged(nameof(Score)); }
        }
        private int moves = 0;
        public int Moves
        {
            get { return moves; }
            set { moves = value; OnPropertyChanged(nameof(Moves)); }
        }

        private bool gameWon = false;
        public bool GameWon
        {
            get { return gameWon; }
            set { gameWon = value; OnPropertyChanged(nameof(GameWon)); }
        }
        private TimeSpan elapsedTime;
        public TimeSpan ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; OnPropertyChanged(nameof(ElapsedTime)); }
        }
        public ICommand RightClickCardCommand { get; set; }
        public ICommand LeftClickCardCommand { get; set; }
        public ICommand DealGameCommand { get; set; }
        public ICommand ReturnToHomeCommand { get; set; }


        public GameViewModel()
        {

            RightClickCardCommand = new RelayParameterizedCommand(RightClickCard);
            LeftClickCardCommand = new RelayCommand(LeftClickCard);
            ReturnToHomeCommand = new RelayCommand(ReturnToHome);
            DealGameCommand = new RelayCommand(DealGame);
        }

        public virtual void ReturnToHome() { }

        public virtual void LeftClickCard() { }

        public virtual void RightClickCard(object parameter) { }

        public virtual void DealGame()
        {
            //  Stop the timer and reset the game data.
            StopTimer();
            ElapsedTime = TimeSpan.FromSeconds(0);
            Moves = 0;
            Score = 0;
            GameWon = false;
        }


        public void StartTimer()
        {
            lastTick = DateTime.Now;
            timer.Start();
        }


        public void StopTimer()
        {
            timer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //  Get the time, update the elapsed time, record the last tick.
            DateTime timeNow = DateTime.Now;
            ElapsedTime += timeNow - lastTick;
            lastTick = timeNow;
        }

        protected void FireGameWonEvent()
        {
            IsGameWon?.Invoke();
        }

        private Timer timer = new Timer();

        private DateTime lastTick;

        public event Action IsGameWon;
    }
}

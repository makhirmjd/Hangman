using System.ComponentModel;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        #region UI Properties
        public string Spotlight 
        { 
            get => spotlight;
            set
            {
                spotlight = value;
                OnPropertyChanged();
            } 
        }
        public List<char> Letters 
        { 
            get => letters;
            set 
            {
                letters = value;
                OnPropertyChanged();
            } 
        }
        public string Message 
        { 
            get => message;
            set 
            {
                message = value;
                OnPropertyChanged();
            }  
        }
        public string GameStatus 
        { 
            get => gameStatus;
            set
            {
                gameStatus = value;
                OnPropertyChanged();
            } 
        }

        public string CurrentImage 
        { 
            get => currentImage;
            set
            {
                currentImage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Fields
        private List<string> words = [
            "python",
            "javascript",
            "maui",
            "csharp",
            "mongodb",
            "sql",
            "xaml",
            "word",
            "excel",
            "powerpoint",
            "code",
            "hotreload",
            "snippets"
            ];
        private string answer = string.Empty;
        private string spotlight = string.Empty;
        private List<char> guessed = [];
        private List<char> letters = [.. "abcdefghijklmnopqrstuvwxyz"];
        private string message = string.Empty;
        private int mistakes = 0;
        private const int MaxWrong = 6;
        private string gameStatus = $"Errors: 0 of {MaxWrong}";
        private string currentImage = "img0.jpg";
        #endregion

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            PickWord();
            CalculateWord(answer, guessed);
        }

        #region Game Engine
        private void PickWord()
        {
            answer = words[Random.Shared.Next(words.Count)];
            Debug.WriteLine(answer);
        }

        private void CalculateWord(string answer, List<char> guessed)
        {
            char[] temp = [.. answer.Select(x => guessed.IndexOf(x) >= 0 ? x : '_')];
            Spotlight = string.Join(' ', temp);
        }

        private void HandleGuess(char letter)
        {
            if (guessed.IndexOf(letter) == -1)
            {
                guessed.Add(letter);
            }

            if (answer.IndexOf(letter) >= 0)
            {
                CalculateWord(answer, guessed);
                CheckIfGameWon();
            } 
            else if (answer.IndexOf(letter) == -1)
            {
                ++mistakes;
                UpdateStatus();
                CheckIfGameLost();
                int imageNumber = mistakes % (MaxWrong + 1);
                CurrentImage = $"img{imageNumber}.jpg";
            }
        }

        private void CheckIfGameWon()
        {
            if (Spotlight.Replace(" ", "") == answer)
            {
                Message = "You Won!!";
            }
        }

        private void CheckIfGameLost()
        {
            if (mistakes >= MaxWrong)
            {
                Message = $"You Lost!! The word was: {answer}";
            }
        }

        private void UpdateStatus()
        {
            GameStatus = $"Errors: {mistakes} of {MaxWrong}";
        }
        #endregion

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string letter = button.Text;
                button.IsEnabled = false;
                HandleGuess(letter[0]);
            }
        }
    }
}

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
        private List<char> letters = [];
        private string message = string.Empty;
        #endregion

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            PickWord();
            CalculateWord(answer, guessed);
            Letters.AddRange("abcdefghijklmnopqrstuvwxyz");
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
        }

        private void CheckIfGameWon()
        {
            if (Spotlight.Replace(" ", "") == answer)
            {
                Message = "You win!";
            }
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

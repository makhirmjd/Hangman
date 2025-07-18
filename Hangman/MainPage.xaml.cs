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
        #endregion
    }
}

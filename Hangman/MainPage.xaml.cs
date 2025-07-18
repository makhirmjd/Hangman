using System.ComponentModel;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
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
        #endregion

        public MainPage()
        {
            InitializeComponent();
            PickWord();
        }

        #region Game Engine
        private void PickWord()
        {
            answer = words[Random.Shared.Next(words.Count)];
            Debug.WriteLine(answer);
        }
        #endregion
    }
}

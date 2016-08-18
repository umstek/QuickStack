using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace QuickStack
{
    [Export(typeof(IShell))]
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public IObservableCollection<string> Languages { get; set; } =
            new BindableCollection<string>(new List<string>
            {
                "VB 14",
                "C# 6",
                "F#",
                "Java 8",
                "Node/Harmony",
                "Node/Vanilla",
                "Python 2.7.x",
                "Python 3.5.x",
                "Haskell",
                "Ruby",
                "Rust"
            });

        public string SelectedLanguage { get; set; } = "C# 6";
        public bool CanRun() => false;

        public void Run()
        {
            Console.WriteLine(@"Run");
        }
    }
}
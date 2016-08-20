using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using QuickStack.ViewModels.Flyouts;

namespace QuickStack.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        public IObservableCollection<FlyoutViewModelBase> Flyouts { get; set; } =
            new BindableCollection<FlyoutViewModelBase>() { };

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
        public bool CanRun() => true;

        public void Run()
        {
            Console.WriteLine(@"Run");
        }

        public void CreateTerminal()
        {
            Flyouts.Add(new TerminalFlyoutViewModel());
        }
    }
}
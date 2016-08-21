using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Caliburn.Micro;
using QuickStack.ViewModels.Flyouts;

namespace QuickStack.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        private int _selectedTerminal = -1;

        public IObservableCollection<FlyoutViewModelBase> Flyouts { get; set; } =
            new BindableCollection<FlyoutViewModelBase>();

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

        public IObservableCollection<int> Terminals { get; set; } = new BindableCollection<int>();

        public int SelectedTerminal
        {
            get { return _selectedTerminal; }
            set
            {
                if (value == _selectedTerminal) return;
                Debug.WriteLine(value);

                _selectedTerminal = value;
                NotifyOfPropertyChange(nameof(SelectedTerminal));
                ActivateTerminal(SelectedTerminal);
            }
        }

        public bool CanRun() => true;

        public void Run()
        {
            Console.WriteLine(@"Run");
        }

        public void CreateTerminal()
        {
            Flyouts.Add(new TerminalFlyoutViewModel());
            Terminals.Add(Terminals.Count);
            SelectedTerminal = Terminals.Count - 1;
        }

        public void ActivateTerminal(int index)
        {
            if (index >= 0 && index < Terminals.Count)
            {
                Flyouts.OfType<TerminalFlyoutViewModel>().ToList().ForEach(f => f.IsOpen = false);
                Flyouts.OfType<TerminalFlyoutViewModel>().ElementAt(index).IsOpen = true;
            }
        }
    }
}
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace QuickStack.ViewModels.Flyouts
{
    public class TerminalFlyoutViewModel : FlyoutViewModelBase
    {
        private readonly Process _terminal;

        public TerminalFlyoutViewModel()
        {
            Header = "";
            Position = Position.Bottom;
            FlyoutTheme = FlyoutTheme.Accent;
            IsOpen = false;
            var startInfo = new ProcessStartInfo(@"cmd.exe") // BUG Doesn't work with anything except cmd.exe
            {
                UseShellExecute = false,
                WorkingDirectory = @".",
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };
            _terminal = new Process {EnableRaisingEvents = true, StartInfo = startInfo};
            _terminal.OutputDataReceived += TerminalOnOutputDataReceived;
            _terminal.ErrorDataReceived += TerminalOnErrorDataReceived;
            _terminal.Start();
            _terminal.BeginOutputReadLine();
            _terminal.BeginErrorReadLine();
        }

        // ReSharper disable once CollectionNeverQueried.Global
        public ObservableCollection<string> Listing { get; } = new BindableCollection<string>();
        public string Input { get; set; } = "";

        private void TerminalOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            Listing.Add(dataReceivedEventArgs.Data);
        }

        private void TerminalOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            Listing.Add(dataReceivedEventArgs.Data);
        }

        public async void InputEnter(KeyEventArgs args) // TODO Move to new class which specializes in handling CLIs.
        {
            if (args.Key != Key.Enter) return;

            Debug.WriteLine(Input);
            await _terminal.StandardInput.WriteLineAsync(Input);
            await _terminal.StandardInput.FlushAsync();
            Input = "";
            NotifyOfPropertyChange(nameof(Input));
        }
    }
}
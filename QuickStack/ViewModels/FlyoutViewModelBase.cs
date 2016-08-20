using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace QuickStack.ViewModels
{
    public abstract class FlyoutViewModelBase : PropertyChangedBase
    {
        public string Header { get; set; } = "Fly-out";
        public bool IsOpen { get; set; } = false;
        public Position Position { get; set; } = Position.Left;
        public FlyoutTheme FlyoutTheme { get; set; } = FlyoutTheme.Adapt;

    }
}
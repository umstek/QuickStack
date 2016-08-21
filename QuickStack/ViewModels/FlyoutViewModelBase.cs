using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace QuickStack.ViewModels
{
    public abstract class FlyoutViewModelBase : PropertyChangedBase
    {
        private FlyoutTheme _flyoutTheme = FlyoutTheme.Adapt;
        private string _header = "Fly-out";
        private bool _isOpen;
        private Position _position = Position.Left;

        public string Header
        {
            get { return _header; }
            set
            {
                if (value == _header) return;

                _header = value;
                NotifyOfPropertyChange(nameof(Header));
            }
        }

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (value == _isOpen) return;
                _isOpen = value;
                NotifyOfPropertyChange(nameof(IsOpen));
            }
        }

        public Position Position
        {
            get { return _position; }
            set
            {
                if (value == _position) return;
                _position = value;
                NotifyOfPropertyChange(nameof(Position));
            }
        }

        public FlyoutTheme FlyoutTheme
        {
            get { return _flyoutTheme; }
            set
            {
                if (value == _flyoutTheme) return;
                _flyoutTheme = value;
                NotifyOfPropertyChange(nameof(FlyoutTheme));
            }
        }
    }
}
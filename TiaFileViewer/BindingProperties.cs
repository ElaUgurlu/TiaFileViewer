using System.ComponentModel;

namespace TiaFileViewer
{
    class BindingProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string mainWindowTitle;

        public string MainWindowTitle
        {
            get { return mainWindowTitle; }
            set
            {
                if (value != mainWindowTitle)
                {
                    mainWindowTitle = value;
                    OnPropertyChanged(nameof(mainWindowTitle));
                }
            }
        }
    }
}

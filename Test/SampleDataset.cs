using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace sbwpf.Test
{
    internal class SampleData : INotifyPropertyChanged
    {
        ///////////////////////////////////////////////////////////
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetField<TField>(ref TField field, TField value, string propertyName)
        {
            if (EqualityComparer<TField>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        #endregion INotifyPropertyChanged
        ///////////////////////////////////////////////////////////
        
        public string StringZero { get; set; } = nameof(StringZero);
        public string StringOne { get; set; } = nameof(StringOne);
        public Guid GuidOne { get; set; } = Guid.NewGuid();
    }

    internal class SampleDataset : INotifyPropertyChanged
    {

        ///////////////////////////////////////////////////////////
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetField<TField>(ref TField field, TField value, string propertyName)
        {
            if (EqualityComparer<TField>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        #endregion INotifyPropertyChanged
        ///////////////////////////////////////////////////////////



        ///////////////////////////////////////////////////////////
        #region Properties

        public static ObservableCollection<SampleData> Samples { get; } = [];

        #endregion Properties
        ///////////////////////////////////////////////////////////
        

        static SampleDataset()
        {
            for(int i = 0; i < 10; i++)
            {
                Samples.Add(new());
            }
        }
    }
}

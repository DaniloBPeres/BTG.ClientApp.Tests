using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BTG.ClientApp.Core.Models
{
    public class Client : INotifyPropertyChanged
    {
        private Guid _id = Guid.NewGuid();
        private string _name = string.Empty;
        private string _lastname = string.Empty;
        private int _age;
        private string _address = string.Empty;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                    OnPropertyChanged(nameof(FullName));
            }
        }

        public string Lastname
        {
            get => _lastname;
            set
            {
                if (SetProperty(ref _lastname, value))
                    OnPropertyChanged(nameof(FullName));
            }
        }

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public string FullName => $"{Name} {Lastname}";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
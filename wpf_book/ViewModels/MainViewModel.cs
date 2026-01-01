using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using wpf_book.Models;
using wpf_book.Services;
using wpf_book.Utils;

namespace wpf_book.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ContactService _contactService;
        private Contact? _selectedContact;
        private string _currentTheme = "Light";
        private string _statusMessage = "Ready";

        public ObservableCollection<Contact> Contacts { get; set; }

        public Contact? SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        public string CurrentTheme
        {
            get => _currentTheme;
            set
            {
                _currentTheme = value;
                OnPropertyChanged();
                SwitchTheme(value);
                UpdateStatusMessage();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> AvailableThemes { get; set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }

        public MainViewModel()
        {
            _contactService = new ContactService();
            Contacts = new ObservableCollection<Contact>();
            AvailableThemes = new ObservableCollection<string> { "Light", "Dark", "Blue", "Warm" };

            AddCommand = new RelayCommand(ExecuteAdd);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            SaveCommand = new RelayCommand(ExecuteSave);
            ImportCommand = new RelayCommand(ExecuteImport);
            ExportCommand = new RelayCommand(ExecuteExport, CanExecuteExport);

            LoadContacts();
        }

        private void LoadContacts()
        {
            var contacts = _contactService.LoadContacts();
            Contacts.Clear();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
            UpdateStatusMessage();
        }

        private void ExecuteAdd(object? parameter)
        {
            var newContact = new Contact
            {
                Name = "New Contact"
            };
            Contacts.Add(newContact);
            SelectedContact = newContact;
            UpdateStatusMessage();
        }

        private bool CanExecuteDelete(object? parameter)
        {
            return SelectedContact != null;
        }

        private void ExecuteDelete(object? parameter)
        {
            if (SelectedContact != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete '{SelectedContact.Name}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Contacts.Remove(SelectedContact);
                    SelectedContact = null;
                    UpdateStatusMessage();
                }
            }
        }

        private void ExecuteSave(object? parameter)
        {
            try
            {
                _contactService.SaveContacts(Contacts);
                StatusMessage = $"Saved {Contacts.Count} contacts - {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving contacts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteExport(object? parameter)
        {
            return Contacts.Count > 0;
        }

        private void ExecuteExport(object? parameter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json",
                FileName = "contacts.json"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _contactService.ExportContacts(dialog.FileName, Contacts);
                    MessageBox.Show($"Successfully exported {Contacts.Count} contacts.", "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting contacts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExecuteImport(object? parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = "json"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _contactService.ImportContacts(dialog.FileName, out var importedContacts);
                    
                    var result = MessageBox.Show(
                        $"Found {importedContacts.Count} contacts. Replace existing contacts or add to them?",
                        "Import Contacts",
                        MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes) // Replace
                    {
                        Contacts.Clear();
                        foreach (var contact in importedContacts)
                        {
                            Contacts.Add(contact);
                        }
                    }
                    else if (result == MessageBoxResult.No) // Add
                    {
                        foreach (var contact in importedContacts)
                        {
                            // Regenerate ID to avoid duplicates
                            contact.Id = Guid.NewGuid();
                            Contacts.Add(contact);
                        }
                    }

                    UpdateStatusMessage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing contacts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SwitchTheme(string themeName)
        {
            var app = Application.Current;
            app.Resources.MergedDictionaries.Clear();

            var themeUri = new Uri($"/Themes/Theme{themeName}.xaml", UriKind.Relative);
            var themeDict = new ResourceDictionary { Source = themeUri };
            app.Resources.MergedDictionaries.Add(themeDict);
        }

        private void UpdateStatusMessage()
        {
            StatusMessage = $"{Contacts.Count} contacts | Theme: {CurrentTheme}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

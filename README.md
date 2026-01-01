"# WPF Address Book Application

A modern WPF address book application built with .NET 8.0, following MVVM architecture pattern.

## Features

- **Contact Management**: Add, edit, delete contacts with full details (Name, Phone, Email, Address, Company, Notes)
- **JSON Persistence**: Contacts are automatically saved to `%LOCALAPPDATA%/wpf_book/contacts.json`
- **Import/Export**: Import and export contacts as JSON files
- **Multiple Themes**: Switch between 4 beautiful themes (Light, Dark, Blue, Warm)
- **Touch-Friendly UI**: Large fonts (≥16px), spacious buttons, and scrollable lists
- **Avatar Initials**: Contacts display with circular avatars showing their initials

## Project Structure

```
wpf_book/
├── Models/
│   └── Contact.cs              # Contact data model
├── Services/
│   └── ContactService.cs       # JSON persistence service
├── ViewModels/
│   └── MainViewModel.cs        # Main view model with commands
├── Views/
│   └── ContactListView.xaml    # Contact list and edit panel
├── Themes/
│   ├── ThemeLight.xaml         # Light theme
│   ├── ThemeDark.xaml          # Dark theme
│   ├── ThemeBlue.xaml          # Blue theme
│   └── ThemeWarm.xaml          # Warm theme
├── Utils/
│   └── RelayCommand.cs         # ICommand implementation
├── Converters/
│   └── NullToBoolConverter.cs  # Null to boolean converter
├── App.xaml                    # Application entry point
└── MainWindow.xaml             # Main window with toolbar and status bar
```

## Building and Running

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 (or VS Code with C# extension)

### Build
```bash
cd wpf_book
dotnet restore
dotnet build
```

### Run
```bash
cd wpf_book
dotnet run
```

## Usage

1. **Add Contact**: Click the "➕ Add" button to create a new contact
2. **Edit Contact**: Select a contact from the list and edit details in the right panel
3. **Delete Contact**: Select a contact and click "🗑️ Delete"
4. **Save**: Click "💾 Save" to persist contacts to disk
5. **Import**: Click "📥 Import" to import contacts from a JSON file
6. **Export**: Click "📤 Export" to export contacts to a JSON file
7. **Switch Theme**: Select a theme from the dropdown and click "🎨 Switch"

## Technologies Used

- **.NET 8.0**: Target framework
- **WPF**: Windows Presentation Foundation
- **MVVM Pattern**: Model-View-ViewModel architecture
- **Newtonsoft.Json**: JSON serialization
- **ResourceDictionary**: Dynamic theme switching

## License

This project is provided as-is for educational purposes.
" 

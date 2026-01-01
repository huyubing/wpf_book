# WPF Address Book - Testing Guide

## Overview
This document describes how to test the WPF Address Book application on a Windows machine with Visual Studio 2022.

## Prerequisites
- Windows 10/11
- Visual Studio 2022 with .NET Desktop Development workload
- .NET 8.0 SDK

## Building the Application

### Using Visual Studio 2022
1. Open the solution in Visual Studio 2022:
   - Navigate to the `wpf_book/wpf_book` directory
   - Open `wpf_book.csproj` or double-click to open in VS 2022
2. Build the solution: `Ctrl+Shift+B` or Build > Build Solution
3. Run the application: `F5` or Debug > Start Debugging

### Using Command Line
```bash
cd wpf_book
dotnet restore
dotnet build
dotnet run
```

## Testing Checklist

### 1. Initial Startup
- [ ] Application starts successfully
- [ ] Main window displays with "Address Book" title
- [ ] Toolbar is visible with all buttons (Add, Delete, Save, Import, Export)
- [ ] Theme selector shows current theme as "Light"
- [ ] Status bar shows "0 contacts | Theme: Light"
- [ ] Left panel (contact list) is empty
- [ ] Right panel (edit form) is disabled (grayed out)

### 2. Adding Contacts
- [ ] Click "➕ Add" button
- [ ] New contact appears in the left list with "New Contact" name
- [ ] Avatar circle shows "N" initial
- [ ] Right edit panel becomes enabled
- [ ] All fields are editable:
  - Name
  - Phone
  - Email
  - Address
  - Company
  - Notes (multi-line text area)
- [ ] Edit the contact name and verify avatar initial updates
- [ ] Status bar updates to show "1 contacts | Theme: Light"
- [ ] Add multiple contacts (3-5) to test list scrolling

### 3. Editing Contacts
- [ ] Select different contacts from the list
- [ ] Verify right panel updates to show selected contact details
- [ ] Edit various fields and verify changes are reflected
- [ ] Change contact name and verify:
  - Name updates in the list
  - Avatar initial updates
- [ ] Add long text in Notes field and verify text wrapping works

### 4. Deleting Contacts
- [ ] Select a contact
- [ ] Click "🗑️ Delete" button
- [ ] Verify confirmation dialog appears
- [ ] Click "Yes" to confirm deletion
- [ ] Verify contact is removed from list
- [ ] Verify status bar count decreases
- [ ] Try clicking Delete with no selection - button should be disabled

### 5. Saving Contacts
- [ ] Add/edit some contacts
- [ ] Click "💾 Save" button
- [ ] Verify status bar shows save confirmation with timestamp
- [ ] Close the application
- [ ] Reopen the application
- [ ] Verify all contacts are loaded automatically
- [ ] Check that the JSON file exists at:
  `%LOCALAPPDATA%\wpf_book\contacts.json`

### 6. Import Contacts
- [ ] Click "📥 Import" button
- [ ] Verify file dialog opens with JSON filter
- [ ] Select a valid JSON file with contacts
- [ ] Verify dialog asking "Replace or Add" appears
- [ ] Test "Yes" (Replace) option:
  - Existing contacts are replaced with imported ones
- [ ] Import again and test "No" (Add) option:
  - Contacts are added to existing list
  - New GUIDs are generated for imported contacts
- [ ] Test importing invalid JSON and verify error handling

### 7. Export Contacts
- [ ] Ensure there are some contacts in the list
- [ ] Click "📤 Export" button (should be enabled)
- [ ] Verify save file dialog opens
- [ ] Save to a JSON file
- [ ] Verify success message appears
- [ ] Open the exported file in a text editor
- [ ] Verify JSON structure is correct and readable
- [ ] Test export with empty list - button should be disabled

### 8. Theme Switching

#### Light Theme
- [ ] Select "Light" from theme dropdown
- [ ] Click "🎨 Switch" button
- [ ] Verify UI updates to light colors:
  - White background
  - Dark text
  - Blue accent colors
- [ ] Status bar shows "Theme: Light"

#### Dark Theme
- [ ] Select "Dark" from theme dropdown
- [ ] Click "🎨 Switch" button
- [ ] Verify UI updates to dark colors:
  - Dark gray/black background
  - Light text
  - Purple accent colors
- [ ] Status bar shows "Theme: Dark"

#### Blue Theme
- [ ] Select "Blue" from theme dropdown
- [ ] Click "🎨 Switch" button
- [ ] Verify UI updates to blue colors:
  - Light blue background
  - Navy text
  - Blue accent colors
- [ ] Status bar shows "Theme: Blue"

#### Warm Theme
- [ ] Select "Warm" from theme dropdown
- [ ] Click "🎨 Switch" button
- [ ] Verify UI updates to warm colors:
  - Cream/beige background
  - Brown text
  - Orange accent colors
- [ ] Status bar shows "Theme: Warm"

### 9. UI/UX Features

#### Touch-Friendly Design
- [ ] Verify font sizes are ≥16px throughout
- [ ] Verify buttons are large with adequate padding
- [ ] Verify spacing between elements is comfortable
- [ ] Test scrolling in contact list with many contacts
- [ ] Test scrolling in edit form
- [ ] Test scrolling in Notes text area

#### Avatar Circles
- [ ] Verify each contact shows circular avatar
- [ ] Verify initials are displayed correctly
- [ ] Verify avatar color matches theme accent
- [ ] Test with contacts starting with different letters
- [ ] Test with empty name (should show "?")

#### List Item Styles
- [ ] Verify list items have proper spacing
- [ ] Verify hover effect on list items
- [ ] Verify selected item is highlighted
- [ ] Verify border color changes on selection

#### Button Styles
- [ ] Primary buttons (Add, Save, Switch) have solid color
- [ ] Ghost buttons (Import, Export) have border style
- [ ] Danger button (Delete) has red color
- [ ] Hover effects work on all buttons
- [ ] Disabled buttons appear grayed out

### 10. Data Persistence

#### Auto-load on Startup
- [ ] Add contacts and save
- [ ] Close application
- [ ] Reopen application
- [ ] Verify all contacts are loaded automatically

#### JSON File Location
- [ ] Verify directory created at `%LOCALAPPDATA%\wpf_book\`
- [ ] Verify `contacts.json` file exists
- [ ] Open and verify JSON structure:
```json
[
  {
    "Id": "guid-here",
    "Name": "John Doe",
    "Phone": "555-1234",
    "Email": "john@example.com",
    "Address": "123 Main St",
    "Company": "Acme Corp",
    "Notes": "Some notes"
  }
]
```

### 11. Error Handling
- [ ] Test importing malformed JSON
- [ ] Test importing non-existent file
- [ ] Test saving to read-only location (if possible)
- [ ] Verify appropriate error dialogs appear
- [ ] Verify application doesn't crash on errors

### 12. Window Behavior
- [ ] Test resizing window
- [ ] Verify content scales appropriately
- [ ] Verify scrollbars appear when needed
- [ ] Test minimizing and restoring window
- [ ] Test closing and reopening application

## Expected Results Summary

The application should:
1. ✅ Build without errors or warnings
2. ✅ Run on .NET 8.0 Windows
3. ✅ Support full CRUD operations for contacts
4. ✅ Persist data to JSON file automatically
5. ✅ Support import/export of contacts
6. ✅ Provide 4 switchable themes
7. ✅ Display touch-friendly UI with large fonts and buttons
8. ✅ Show avatar circles with initials
9. ✅ Follow MVVM architecture pattern
10. ✅ Handle errors gracefully

## Screenshot Locations for Verification

Please take screenshots of:
1. Main window on startup (empty state)
2. Main window with multiple contacts loaded
3. Edit panel with a contact selected
4. Each theme (Light, Dark, Blue, Warm)
5. Theme switching in action
6. Import/Export dialogs
7. Delete confirmation dialog
8. Status bar with different messages
9. Contact list with avatars visible
10. JSON file in %LOCALAPPDATA%\wpf_book\

## Notes

- The application requires Windows to run (WPF is Windows-only)
- Testing should be done on Visual Studio 2022 with .NET 8.0
- All themes should apply consistently across all UI elements
- The application follows Material Design principles for theming

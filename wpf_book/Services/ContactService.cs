using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using wpf_book.Models;

namespace wpf_book.Services
{
    public class ContactService
    {
        private readonly string _dataPath;
        private readonly string _fileName = "contacts.json";

        public ContactService()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataPath = Path.Combine(localAppData, "wpf_book");
            
            // Ensure directory exists
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }
        }

        public string GetFilePath()
        {
            return Path.Combine(_dataPath, _fileName);
        }

        public List<Contact> LoadContacts()
        {
            var filePath = GetFilePath();
            
            if (!File.Exists(filePath))
            {
                return new List<Contact>();
            }

            try
            {
                var json = File.ReadAllText(filePath);
                var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
                return contacts ?? new List<Contact>();
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON deserialization error: {ex.Message}");
                return new List<Contact>();
            }
            catch (IOException ex)
            {
                System.Diagnostics.Debug.WriteLine($"File I/O error: {ex.Message}");
                return new List<Contact>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unexpected error loading contacts: {ex.Message}");
                return new List<Contact>();
            }
        }

        public void SaveContacts(IEnumerable<Contact> contacts)
        {
            var filePath = GetFilePath();
            var json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void ImportContacts(string importPath, out List<Contact> contacts)
        {
            if (!File.Exists(importPath))
            {
                throw new FileNotFoundException("Import file not found.", importPath);
            }

            try
            {
                var json = File.ReadAllText(importPath);
                contacts = JsonConvert.DeserializeObject<List<Contact>>(json) ?? new List<Contact>();
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException($"Invalid JSON format in import file: {ex.Message}", ex);
            }
            catch (IOException ex)
            {
                throw new IOException($"Error reading import file: {ex.Message}", ex);
            }
        }

        public void ExportContacts(string exportPath, IEnumerable<Contact> contacts)
        {
            var json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(exportPath, json);
        }
    }
}

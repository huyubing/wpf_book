using System;
using Newtonsoft.Json;

namespace wpf_book.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        [JsonIgnore]
        public string Initial
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return "?";
                
                return Name.Trim().Substring(0, 1).ToUpper();
            }
        }

        public Contact()
        {
            Id = Guid.NewGuid();
        }
    }
}

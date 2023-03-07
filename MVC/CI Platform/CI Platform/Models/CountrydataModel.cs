using CI_Platform.DataModels;

namespace CI_Platform.Models
{
    public class CountrydataModel
    {
        public long CountryId { get; set; }

        public string Name { get; set; } = null!;

        public string? Iso { get; set; }

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}

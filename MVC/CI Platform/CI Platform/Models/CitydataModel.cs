using CI_Platform.DataModels;

namespace CI_Platform.Models
{
    public class CitydataModel
    {
        public long CityId { get; set; }

        public long CountryId { get; set; }

        public string Name { get; set; } = null!;

        public byte[] CreatedAt { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual Country Country { get; set; } = null!;
    }
}

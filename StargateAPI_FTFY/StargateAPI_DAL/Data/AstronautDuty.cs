

using Azure.Data.Tables;
using Azure;

namespace StargateAPI_DAL
{

    public class AstronautDuty : ITableEntity
    {
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public int Id { get; set; }
        public string Rank { get; set; } = string.Empty;

        public string DutyTitle { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime DutyStartDate { get; set; }

        public DateTime? DutyEndDate { get; set; }
    }    
}

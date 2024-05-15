using Azure;
using Azure.Data.Tables;

namespace StargateAPI_DAL
{
     
    public class Astronaut : ITableEntity     
        {
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public int Id { get; set; } 

        public string Name { get; set; }// To keep it simple we will make this just full name (first{space}last). Seems like it could/should be separate columns Fname Lname etc --Jsw 

        public string CurrentRank { get; set; } 

        public string CurrentDutyTitle { get; set; } 

        public DateTime CareerStartDate { get; set; }

        public DateTime? CareerEndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual ICollection<AstronautDuty>? AstronautDuties { get; set; }
       
    } 
}

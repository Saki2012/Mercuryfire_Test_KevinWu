using System.ComponentModel.DataAnnotations;

namespace Mercuryfire_Test_KevinWu.Models
{
    public class ExecutionLogModel
    {
        public long DeLog_AutoID { get; set; }
        [MaxLength(120)]
        public string DeLog_StoredPrograms { get; set; } = string.Empty;
        public Guid DeLog_GroupID { get; set; }
        public bool DeLog_isCustomDebug { get; set; }
        [MaxLength(120)]
        public string DeLog_ExecutionProgram { get; set; } = string.Empty;
        public string? DeLog_ExecutionInfo { get; set; }
        public bool? DeLog_VerifyNeeded { get; set; }
        public DateTime DeLog_ExDateTime { get; set; }

    }
}

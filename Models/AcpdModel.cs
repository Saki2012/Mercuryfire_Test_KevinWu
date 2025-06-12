using System.ComponentModel.DataAnnotations;

namespace Mercuryfire_Test_KevinWu.Models
{
    public class AcpdModel
    {
        [MaxLength(20)]
        public string? ACPD_SID { get; set; }
        [MaxLength(60)]
        public string? ACPD_Cname { get; set; }
        [MaxLength(60)]
        public string? ACPD_Ename { get; set; }
        [MaxLength(40)]
        public string? ACPD_Sname { get; set; }
        [MaxLength(60)]
        public string? ACPD_Email { get; set; }
        public byte? ACPD_Status { get; set; }
        public bool? ACPD_Stop { get; set; }
        [MaxLength(60)]
        public string? ACPD_StopMemo { get; set; }
        [MaxLength(30)]
        public string? ACPD_LoginID { get; set; }
        [MaxLength(60)]
        public string? ACPD_LoginPWD { get; set; }
        [MaxLength(600)]
        public string? ACPD_Memo { get; set; }
        public DateTime? ACPD_NowDateTime { get; set; }
        [MaxLength(20)]
        public string? ACPD_NowID { get; set; }
        public DateTime? ACPD_UPDDateTime { get; set; }
        [MaxLength(20)]
        public string? ACPD_UPDID { get; set; }
    }
}

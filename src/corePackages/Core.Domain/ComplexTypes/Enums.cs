using System.ComponentModel;

namespace Core.Domain.ComplexTypes;

public class Enums
{
    public enum RecordStatu
    {
        None = 0,
        Active = 1,
        Passive = 2,
    }
    
    public enum ReportStatu
    {
        None = 0,
        [Description("Tamamlandı")]
        Success = 1,
        [Description("Hazırlanıyor")]
        InProgress = 2,
        [Description("Başarısız")]
        Failed = 3,
    }
}
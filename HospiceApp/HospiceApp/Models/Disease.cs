namespace HospiceApp.Models;

public class Disease
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ICDCode { get; set; } = string.Empty;
    public bool IsHospiceEligible { get; set; }
}
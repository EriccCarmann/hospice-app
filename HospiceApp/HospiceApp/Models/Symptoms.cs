namespace HospiceApp.Models;

public class Symptoms
{
    public string Disease { get; set; } = string.Empty;
    public double PainLevel { get; set; }
    public double DyspneaLevel { get; set; }
    public double NauseaLevel { get; set; }
    public double FatigueLevel { get; set; }
    public double AnxietyLevel { get; set; }
    public double ConfusionLevel { get; set; }
    public string NeedsSymptomSupport { get; set; }
}
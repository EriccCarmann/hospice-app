using CommunityToolkit.Mvvm.ComponentModel;
using HospiceApp.Models;

namespace HospiceApp.ViewModels;

public partial class SymptomBurdenModelView : ObservableObject
{
    [ObservableProperty] public bool isSymptomBurdenVisible;

    [ObservableProperty] private double _painLevel;
    [ObservableProperty] private double _dyspneaLevel;
    [ObservableProperty] private double _nauseaLevel;
    [ObservableProperty] private double _fatigueLevel;
    [ObservableProperty] private double _anxietyLevel;
    [ObservableProperty] private double _confusionLevel;
    [ObservableProperty] private bool _needsSymptomSupport;

    public Symptoms GetSymptoms()
    {
        return new Symptoms()
        {
            PainLevel = Math.Round(PainLevel, 2),
            DyspneaLevel = Math.Round(DyspneaLevel, 2),
            NauseaLevel = Math.Round(NauseaLevel, 2),
            FatigueLevel = Math.Round(FatigueLevel, 2),
            AnxietyLevel = Math.Round(AnxietyLevel, 2),
            ConfusionLevel = Math.Round(ConfusionLevel, 2),
            NeedsSymptomSupport = NeedsSymptomSupport ? "Yes" : "No"
        };
    }
}
using LoginTestAppMaui.Models;

namespace LoginTestAppMaui.Services.Abstract;

public interface IStrapiService
{
    public Task<List<Illness>> GetIllnessesAsync();
    
}
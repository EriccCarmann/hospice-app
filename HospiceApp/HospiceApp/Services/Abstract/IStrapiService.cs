using HospiceApp.Models;

namespace HospiceApp.Services.Abstract;

public interface IStrapiService
{
    public Task<List<Illness>> GetIllnessesAsync();
    public Task<List<Illness>> GetIllnessesByNameAsync(string substring);
    public Task<Illness> AddIllnessAsync(Illness illness);
    public Task<Illness> UpdateIllnessAsync(Illness illness);
    public Task DeleteIllnessAsync(Illness illness);
}
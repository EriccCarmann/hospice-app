using HospiceApp.Models;

namespace HospiceApp.Services.Abstract;

public interface IStrapiService
{
    public Task<List<Disease>> GetDiseasesAsync();
    public Task<List<Disease>> GetDiseasesByNameAsync(string substring);
    public Task<Disease> AddDiseaseAsync(Disease disease);
    public Task UpdateDiseaseAsync(string oldName, Disease disease);
    public Task DeleteDiseaseAsync(string name);
}
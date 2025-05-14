using HospiceApp.Models;

namespace HospiceApp.Services.Abstract;

public interface IStrapiService
{
    public Task<List<Disease>> GetDiseasesAsync();
    public Task<List<Disease>> GetDiseasesByNameAsync(string substring);
    public Task<Disease> AddDiseaseAsync(Disease disease);
    public Task<Disease> UpdateDiseaseAsync(Disease disease);
    public Task DeleteDiseaseAsync(Disease disease);
}
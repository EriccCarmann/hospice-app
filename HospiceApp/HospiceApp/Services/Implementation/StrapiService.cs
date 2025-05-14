using System.Diagnostics;
using System.Text.Json;
using LoginTestAppMaui.Models;
using LoginTestAppMaui.Services.Abstract;

namespace LoginTestAppMaui.Services.Implementation;

public class StrapiService : IStrapiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string BaseUrl = "https://mighty-whisper-8b282eed39.strapiapp.com";
    
    public StrapiService()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(BaseUrl)
        };
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        
    }

    public async Task<List<Illness>> GetIllnessesAsync()
    {
        var illnesses = new List<Illness>();

        try
        {
            var response = await _httpClient.GetAsync("/api/illnesses");
            response.EnsureSuccessStatusCode();

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataArray = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray();

            foreach (var item in dataArray)
            {
                var illness = new Illness
                {
                    Id                  = item.GetProperty("id").GetInt32(),
                    Name                = item.GetProperty("Name").GetString() ?? string.Empty,
                    Description         = item.GetProperty("Description").GetString() ?? string.Empty,
                    ICDCode             = item.GetProperty("ICDCode").GetString() ?? string.Empty,
                    IsHospiceEligible   = item.GetProperty("IsHospiceEligible").GetBoolean()
                };

                illnesses.Add(illness);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading illnesses: {ex}");
        }

        return illnesses;
    }

    public async Task<Illness> GetIllnessByFullNameAsync(string name)
    {
        var illness = new Illness();

        try
        {
            var encoded = Uri.EscapeDataString(name);
            var response = await _httpClient.GetAsync($"/api/illnesses?filters[Name][$eq]={encoded}");
            response.EnsureSuccessStatusCode();

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataObject = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray()
                .First();;

            illness = new Illness
            {
                Id                  = dataObject.GetProperty("id").GetInt32(),
                Name                = dataObject.GetProperty("Name").GetString() ?? string.Empty,
                Description         = dataObject.GetProperty("Description").GetString() ?? string.Empty,
                ICDCode             = dataObject.GetProperty("ICDCode").GetString() ?? string.Empty,
                IsHospiceEligible   = dataObject.GetProperty("IsHospiceEligible").GetBoolean()
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading illnesses: {ex}");
        }

        return illness;
    }
    
    public async Task<List<Illness>> GetIllnessesByNameAsync(string substring)
    {
        var illnesses = new List<Illness>();

        try
        {
            var encoded = Uri.EscapeDataString(substring);
            var response = await _httpClient.GetAsync($"/api/illnesses?filters[Name][$containsi]={encoded}");
            response.EnsureSuccessStatusCode();

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataObject = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray();;

            foreach (var item in dataObject)
            {
                illnesses.Add(new Illness
                {
                    Id                  = item.GetProperty("id").GetInt32(),
                    Name                = item.GetProperty("Name").GetString() ?? string.Empty,
                    Description         = item.GetProperty("Description").GetString() ?? string.Empty,
                    ICDCode             = item.GetProperty("ICDCode").GetString() ?? string.Empty,
                    IsHospiceEligible   = item.GetProperty("IsHospiceEligible").GetBoolean()
                });
            }
        }
        
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading illnesses: {ex}");
        }

        return illnesses;
    }

    public Task<Illness> AddIllnessAsync(Illness illness)
    {
        throw new NotImplementedException();
    }

    public Task<Illness> UpdateIllnessAsync(Illness illness)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIllnessAsync(Illness illness)
    {
        throw new NotImplementedException();
    }
}
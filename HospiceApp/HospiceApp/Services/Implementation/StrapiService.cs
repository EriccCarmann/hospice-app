using System.Diagnostics;
using System.Text.Json;
using HospiceApp.Models;
using HospiceApp.Services.Abstract;

namespace HospiceApp.Services.Implementation;

public class StrapiService : IStrapiService
{
    private readonly HttpClient _mainHttpClient;
    private readonly HttpClient _reservedHttpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string MainUrl = "https://mighty-whisper-8b282eed39.strapiapp.com";
    private const string ReservedUrl = "http://10.201.32.78:1337";
    
    public StrapiService()
    {
        _mainHttpClient = new HttpClient()
        {
            BaseAddress = new Uri(MainUrl)
        };

        _reservedHttpClient = new HttpClient()
        {
            BaseAddress = new Uri(ReservedUrl)
        };
        
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        
    }

    public async Task<List<Disease>> GetDiseasesAsync()
    {
        var diseases = new List<Disease>();

        try
        {
            var response = await TryGetResponse("/api/diseases");

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataArray = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray();

            foreach (var item in dataArray)
            {
                var illness = new Disease
                {
                    Id                  = item.GetProperty("id").GetInt32(),
                    Name                = item.GetProperty("Name").GetString() ?? string.Empty,
                    Description         = item.GetProperty("Description").GetString() ?? string.Empty,
                    ICDCode             = item.GetProperty("ICDCode").GetString() ?? string.Empty,
                    IsHospiceEligible   = item.GetProperty("IsHospiceEligible").GetBoolean()
                };

                diseases.Add(illness);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading diseases: {ex}");
        }

        return diseases;
    }

    public async Task<Disease> GetDiseaseByFullNameAsync(string name)
    {
        var disease = new Disease();

        try
        {
            var encoded = Uri.EscapeDataString(name);
            var response = await TryGetResponse($"/api/diseases?filters[Name][$eq]={encoded}");

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataObject = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray()
                .First();;

            disease = new Disease
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
            Debug.WriteLine($"Error loading diseases: {ex}");
        }

        return disease;
    }
    
    public async Task<List<Disease>> GetDiseasesByNameAsync(string substring)
    {
        var diseases = new List<Disease>();

        try
        {
            var encoded = Uri.EscapeDataString(substring);

            var response = await TryGetResponse($"/api/diseases?filters[Name][$containsi]={encoded}");

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataObject = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray();;

            foreach (var item in dataObject)
            {
                diseases.Add(new Disease
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
            Debug.WriteLine($"Error loading diseases: {ex}");
        }

        return diseases;
    }

    public Task<Disease> AddDiseaseAsync(Disease disease)
    {
        throw new NotImplementedException();
    }

    public Task<Disease> UpdateDiseaseAsync(Disease disease)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDiseaseAsync(Disease disease)
    {
        throw new NotImplementedException();
    }

    private async Task<HttpResponseMessage> TryGetResponse(string path)
    {
        var response = await _mainHttpClient.GetAsync(path);

        if (!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        {
            response = await _reservedHttpClient.GetAsync(path);
        }

        return response;
    }
}
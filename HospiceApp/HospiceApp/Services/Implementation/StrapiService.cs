using System.Diagnostics;
using System.Text;
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
                .First();

            disease = new Disease
            {
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

    public async Task<Disease> AddDiseaseAsync(Disease disease)
    {
        var newDisease = new Disease();
        try
        {
            var json = JsonSerializer.Serialize(new
            {
                data = disease
            });
        
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var path = "/api/diseases";

            await _mainHttpClient.PostAsync(path, content);
            await _reservedHttpClient.PostAsync(path, content);
        }
        
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading diseases: {ex}");
        }

        return newDisease;
    }

    public async Task UpdateDiseaseAsync(string oldName, Disease updatedDisease)
    {
        var result = new HttpResponseMessage();

        try
        {
            string json = JsonSerializer.Serialize(new
            {
                data = updatedDisease
            });
            
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var encoded = Uri.EscapeDataString(oldName);
            var response = await TryGetResponse($"/api/diseases?filters[Name][$eq]={encoded}");

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataObject = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray()
                .First();
            
            var url = $"api/diseases/{dataObject.GetProperty("documentId").GetString() ?? string.Empty}";

            result = await _mainHttpClient.PutAsync(url, content);
        }
        catch (Exception e)
        {
            Console.WriteLine(result.Content.ReadAsStringAsync() + " " + e);
            throw;
        };
    }

    public async Task DeleteDiseaseAsync(string name)
    {
        var result = new HttpResponseMessage();

        try
        {
            var encoded = Uri.EscapeDataString(name);
            var response = await TryGetResponse($"/api/diseases?filters[Name][$eq]={encoded}");

            using var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var dataObject = jsonDoc.RootElement
                .GetProperty("data")
                .EnumerateArray()
                .First();
            
            var url = $"api/diseases/{dataObject.GetProperty("documentId").GetString() ?? string.Empty}";
            
            result = await _mainHttpClient.DeleteAsync(url);
        }
        catch (Exception e)
        {
            Console.WriteLine(result.Content.ReadAsStringAsync() + " " + e);
            throw;
        }
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
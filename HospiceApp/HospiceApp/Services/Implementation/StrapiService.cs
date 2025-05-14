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

    public async Task<List<Illness>> GetIllnessesAsync()
    {
        var illnesses = new List<Illness>();

        try
        {
            var response = await TryGetResponse("/api/diseases");

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
            Debug.WriteLine($"Error loading diseases: {ex}");
        }

        return illnesses;
    }

    public async Task<Illness> GetIllnessByFullNameAsync(string name)
    {
        var illness = new Illness();

        try
        {
            var encoded = Uri.EscapeDataString(name);
            var response = await TryGetResponse($"/api/diseases?filters[Name][$eq]={encoded}");

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
            Debug.WriteLine($"Error loading diseases: {ex}");
        }

        return illness;
    }
    
    public async Task<List<Illness>> GetIllnessesByNameAsync(string substring)
    {
        var illnesses = new List<Illness>();

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
            Debug.WriteLine($"Error loading diseases: {ex}");
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
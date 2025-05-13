using System.Diagnostics;
using System.Text.Json;
using LoginTestAppMaui.Models;
using LoginTestAppMaui.Services.Abstract;

namespace LoginTestAppMaui.Services.Implementation;

public class StrapiService : IStrapiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public StrapiService()
    {
        _httpClient = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<List<Illness>> GetIllnessesAsync()
    {
        var illnesses = new List<Illness>();
        var uri = new Uri("https://mighty-whisper-8b282eed39.strapiapp.com/api/illnesses");

        try
        {
            var response = await _httpClient.GetAsync(uri);
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
}
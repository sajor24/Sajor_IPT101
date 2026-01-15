using FormyBabi.Models;
using System.Text.Json;

namespace FormyBabi.Services;

public class MemoryService
{
    private readonly string _dataPath = "wwwroot/data/memories.json";
    private List<Memory> _memories = new();

    public MemoryService()
    {
        LoadMemories();
    }

    private void LoadMemories()
    {
        try
        {
            // Create data directory if it doesn't exist
            var dataDir = Path.GetDirectoryName(_dataPath);
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir!);

            if (File.Exists(_dataPath))
            {
                var json = File.ReadAllText(_dataPath);
                _memories = JsonSerializer.Deserialize<List<Memory>>(json) ?? new List<Memory>();
            }
            else
            {
                // Initialize with default memories if file doesn't exist
                _memories = new List<Memory>
                {
                    new Memory { Id = 1, Title = "Sunset Love", Content = "Our romantic moment by the beach", Date = DateTime.Now.AddMonths(-6), ImagePath = "/photos/photo4.jpg" },
                    new Memory { Id = 2, Title = "Cathedral Adventure", Content = "Exploring beautiful places together", Date = DateTime.Now.AddMonths(-5), ImagePath = "/photos/photo5.jpg" },
                    new Memory { Id = 3, Title = "Polaroid Memories", Content = "Sweet moments captured forever", Date = DateTime.Now.AddMonths(-4), ImagePath = "/photos/photo6.jpg" },
                    new Memory { Id = 4, Title = "Sweet Memory", Content = "Another beautiful day together", Date = DateTime.Now.AddMonths(-3), ImagePath = "/photos/photo1.jpg" },
                    new Memory { Id = 5, Title = "Happy Times", Content = "Making memories every day", Date = DateTime.Now.AddMonths(-2), ImagePath = "/photos/photo2.jpg" },
                    new Memory { Id = 6, Title = "Together Forever", Content = "Our love story continues", Date = DateTime.Now.AddMonths(-1), ImagePath = "/photos/photo3.jpg" },
                    new Memory { Id = 7, Title = "Special Moment", Content = "A day to remember", Date = DateTime.Now.AddDays(-15), ImagePath = "/photos/S.jpg" }
                };
                SaveMemories();
            }
        }
        catch (Exception)
        {
            // If there's any error, use default memories
            _memories = new List<Memory>
            {
                new Memory { Id = 1, Title = "Sunset Love", Content = "Our romantic moment by the beach", Date = DateTime.Now.AddMonths(-6), ImagePath = "/photos/photo4.jpg" },
                new Memory { Id = 2, Title = "Cathedral Adventure", Content = "Exploring beautiful places together", Date = DateTime.Now.AddMonths(-5), ImagePath = "/photos/photo5.jpg" },
                new Memory { Id = 3, Title = "Polaroid Memories", Content = "Sweet moments captured forever", Date = DateTime.Now.AddMonths(-4), ImagePath = "/photos/photo6.jpg" }
            };
        }
    }

    private void SaveMemories()
    {
        try
        {
            var json = JsonSerializer.Serialize(_memories, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_dataPath, json);
        }
        catch (Exception ex)
        {
            // Handle save errors silently for now
            Console.WriteLine($"Error saving memories: {ex.Message}");
        }
    }

    public List<Memory> GetAllMemories() => _memories.OrderByDescending(m => m.Date).ToList();

    public void AddMemory(Memory memory)
    {
        memory.Id = _memories.Any() ? _memories.Max(m => m.Id) + 1 : 1;
        _memories.Add(memory);
        SaveMemories(); // Save to file immediately
    }

    public void DeleteMemory(int id)
    {
        var memory = _memories.FirstOrDefault(m => m.Id == id);
        if (memory != null) 
        {
            _memories.Remove(memory);
            SaveMemories(); // Save to file immediately
        }
    }
}
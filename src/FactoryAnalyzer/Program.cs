using SatisfactorySaveNet;
using SatisfactorySaveNet.Abstracts;
using SatisfactorySaveNet.Abstracts.Model;

var saveName = "example";

var saveRoot = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "FactoryGame", "Saved", "SaveGames");

var path = Directory.GetFiles(saveRoot, $"{saveName}.sav", SearchOption.AllDirectories)
    .FirstOrDefault();

if (path is null)
{
    Console.WriteLine($"No save called '{saveName}.sav' under {saveRoot}");
    return;
}

Console.WriteLine($"Reading {path}");

ISaveFileSerializer serializer = SaveFileSerializer.Instance;
var save = serializer.Deserialize(path);

Console.WriteLine($"session:  {save.Header.SessionName}");
Console.WriteLine($"saved:    {save.Header.SaveDateTimeUtc:yyyy-MM-dd HH:mm}");
Console.WriteLine($"playtime: {save.Header.PlayedSeconds / 3600.0:F1} h");

if (save.Body is BodyV8 body)
{
    Console.WriteLine($"levels:   {body.Levels.Count}");
    Console.WriteLine($"objects:  {body.Levels.Sum(l => l.Objects.Count)}");
}

Console.WriteLine(save);
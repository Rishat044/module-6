using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class ConfigurationManager
{
    private static ConfigurationManager _instance;
    private static readonly object _lock = new object();
    private Dictionary<string, string> _settings;

    private ConfigurationManager()
    {
        _settings = new Dictionary<string, string>();
    }

    public static ConfigurationManager GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
            }
        }
        return _instance;
    }

    public void LoadSettings(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Файл настроек не найден.");

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var keyValue = line.Split('=');
            if (keyValue.Length == 2)
            {
                SetSetting(keyValue[0].Trim(), keyValue[1].Trim());
            }
            else
            {
                throw new FormatException("Неверный формат строки конфигурации: " + line);
            }
        }
    }

    public void SaveSettings(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var kvp in _settings)
            {
                writer.WriteLine($"{kvp.Key}={kvp.Value}");
            }
        }
    }

    public void LoadSettingsFromDatabase(string connectionString)
    {
        var dbSettings = new Dictionary<string, string>
        {
            { "DbConnection", "Server=myServer;Database=myDB;" },
            { "DbTimeout", "30" }
        };

        foreach (var kvp in dbSettings)
        {
            SetSetting(kvp.Key, kvp.Value);
        }
    }

    public string GetSetting(string key)
    {
        if (!_settings.ContainsKey(key))
            throw new KeyNotFoundException($"Настройка с ключом '{key}' не найдена.");

        return _settings[key];
    }

    public void SetSetting(string key, string value)
    {
        _settings[key] = value;
    }
}

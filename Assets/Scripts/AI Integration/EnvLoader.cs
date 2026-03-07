using System;
using System.IO;
using UnityEngine;

namespace AI_Integration
{
    // Load api key from a .env file
    public class EnvLoader
    {
        public static string GetApiKey()
        {
            var relative = Path.Combine(Application.dataPath, "..", ".env");
            var path = Path.GetFullPath(relative);

            Debug.Log($"EnvLoader: looking for .env at: {path} (Exists: {File.Exists(path)})");

            if (!File.Exists(path)) return null;

            try
            {
                var lines = File.ReadAllLines(path);
                for (var i = 0; i < lines.Length; i++)
                {
                    var raw = lines[i]?.Trim();
                    if (string.IsNullOrEmpty(raw)) continue;

                    // Expect lines like: GROQ_API_KEY=your_key_here
                    if (raw.StartsWith("GROQ_API_KEY", StringComparison.OrdinalIgnoreCase))
                    {
                        var idx = raw.IndexOf('=');
                        if (idx < 0) continue;

                        var value = raw.Substring(idx + 1).Trim();
                        value = value.Trim('"', '\'');
                        value = value.Trim('\uFEFF', '\u200B');

                        if (!string.IsNullOrEmpty(value))
                        {
                            Debug.Log("EnvLoader: GROQ_API_KEY found in .env");
                            return value;
                        }
                    }
                }

                Debug.LogWarning("EnvLoader: GROQ_API_KEY not found in .env");
            }
            catch (Exception ex)
            {
                Debug.LogError($"EnvLoader: failed reading .env: {ex.Message}");
            }

            return null;
        }
    }
}
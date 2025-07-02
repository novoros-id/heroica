using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    private static VoiceLineManager _instance;
    public static VoiceLineManager Instance { get { return _instance; } }

    [Header("Настройки")]
    public int minMatchesToTrigger = 3;

    private List<VoiceLine> voiceLines = new List<VoiceLine>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVoiceLines();
    }

    private void LoadVoiceLines()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("VoiceLines");

        if (jsonFile == null)
        {
            Debug.LogError("VoiceLines.json не найден в папке Resources!");
            return;
        }

        VoiceLine[] lines = JsonHelper.FromJson<VoiceLine>(jsonFile.text);
        voiceLines.AddRange(lines);

        // Предпосчитываем длины тегов
        foreach (var line in voiceLines)
        {
            line.tagCount = line.tags.Count;
        }
    }

    /// <summary>
    /// Проверяет, есть ли подходящая фраза по текущим тегам
    /// </summary>
    public void CheckForMatchingVoiceLine(List<string> currentTags)
    {
        if (voiceLines == null || voiceLines.Count == 0 || currentTags == null || currentTags.Count == 0)
        {
            return;
        }

        var currentTagsSet = new HashSet<string>(currentTags);
        const int MIN_MATCHES = 3;

        foreach (var line in voiceLines)
        {
            // Пропускаем заведомо неподходящие фразы
            if (line.tagCount < MIN_MATCHES)
                continue;

            int matches = 0;

            foreach (var tag in line.tags)
            {
                if (currentTagsSet.Contains(tag))
                {
                    matches++;

                    // Early exit: если уже набрано достаточно совпадений
                    if (matches >= MIN_MATCHES)
                        break;
                }
            }

            if (matches >= MIN_MATCHES)
            {
                Debug.Log($"[VoiceLine] {line.text}");
                return;
            }
        }

        // Необязательно: логгирование отсутствия фразы
        // Debug.Log("[VoiceLine] ...");
    }
}
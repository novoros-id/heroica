using System.Collections.Generic;
using UnityEngine;

public class GameLogger : MonoBehaviour
{
    private static GameLogger _instance;
    public static GameLogger Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameLogger");
                _instance = go.AddComponent<GameLogger>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    // Все записанные действия (каждое — список тегов)
    private List<List<string>> logEntries = new List<List<string>>();

    // Последние MAX_TAGS тегов для контекста
    private Queue<string> recentTags = new Queue<string>();
    private const int MAX_TAGS = 5;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Инициализируем списки здесь, чтобы избежать NullReference
            logEntries = new List<List<string>>();
            recentTags = new Queue<string>();
        }
    }

    /// <summary>
    /// Логирует событие и проверяет, нет ли подходящей фразы
    /// </summary>
    public void Log(List<string> tags)
    {
        // Сохраняем всё событие как одно целое
        logEntries.Add(tags);

        // Добавляем все теги в историю
        foreach (string tag in tags)
        {
            AddToRecentTags(tag);
        }

        List<string> CurrentTags = GetCurrentTags();
        // Проверяем голосовые фразы только один раз после обновления контекста
        if (VoiceLineManager.Instance != null)
        {
            VoiceLineManager.Instance.CheckForMatchingVoiceLine(CurrentTags);
        }
        else
        {
            Debug.LogWarning("VoiceLineManager не инициализирован!");
        }
    }

    private void AddToRecentTags(string tag)
    {
        if (recentTags.Count >= MAX_TAGS)
            recentTags.Dequeue();

        recentTags.Enqueue(tag);
    }

    private List<string> GetCurrentTags()
    {
        return new List<string>(recentTags);
    }

    /// <summary>
    /// Возвращает весь лог как одну строку, каждое событие — в квадратных скобках
    /// </summary>
    public string GetFullLog()
    {
        List<string> lines = new List<string>();

        foreach (var tags in logEntries)
        {
            string line = "[" + string.Join(", ", tags) + "]";
            lines.Add(line);
        }

        return string.Join("\n", lines);
    }

    /// <summary>
    /// Очищает все записи лога
    /// </summary>
    public void ClearLog()
    {
        logEntries.Clear();
        recentTags.Clear();
    }
}
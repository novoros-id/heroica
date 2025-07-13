using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    private static VoiceLineManager _instance;
    public static VoiceLineManager Instance { get { return _instance; } }

    [Header("Настройки")]
    public int minMatchesToTrigger = 3;

    private List<VoiceLine> voiceLines = new List<VoiceLine>();
    private HashSet<string> usedVoiceLines = new HashSet<string>();

    private int checkCallCounter = 0;
    private AudioSource audioSource;

    private bool isVoiceLinePlaying = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(gameObject);

        LoadVoiceLines();
        // Создаем AudioSource
        audioSource = gameObject.GetComponent<AudioSource>();
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
        if (isVoiceLinePlaying)
            return;

        checkCallCounter++;
        if (checkCallCounter % 5 != 0)
            return;

        if (voiceLines == null || voiceLines.Count == 0 || currentTags == null || currentTags.Count == 0)
            return;

        const int MIN_MATCHES = 5;
        var candidates = new List<(VoiceLine line, List<string> matchedTags)>();

        foreach (var line in voiceLines)
        {
            if (line.tagCount < MIN_MATCHES || currentTags.Count < MIN_MATCHES)
                continue;

            for (int i = 0; i <= line.tags.Count - MIN_MATCHES; i++)
            {
                for (int j = 0; j <= currentTags.Count - MIN_MATCHES; j++)
                {
                    bool sequenceMatch = true;
                    List<string> matchedTags = new List<string>();
                    for (int k = 0; k < MIN_MATCHES; k++)
                    {
                        if (line.tags[i + k] == currentTags[j + k])
                            matchedTags.Add(line.tags[i + k]);
                        else
                        {
                            sequenceMatch = false;
                            break;
                        }
                    }
                    if (sequenceMatch)
                    {
                        candidates.Add((line, matchedTags));
                    }
                }
            }
        }

        // Исключаем уже использованные фразы
        var unusedCandidates = candidates.FindAll(c => !usedVoiceLines.Contains(c.line.text));

        if (unusedCandidates.Count > 0)
        {
            // Выбираем случайную фразу из оставшихся
            var chosen = unusedCandidates[Random.Range(0, unusedCandidates.Count)];
            Debug.Log($"[VoiceLine] {chosen.line.text} | Совпавшие теги по порядку: [{string.Join(", ", chosen.matchedTags)}]");
            StartCoroutine(PlayVoiceLineWithBlock(chosen.line));
            usedVoiceLines.Add(chosen.line.text);
        }
        // Если нет новых подходящих фраз — ничего не делаем
    }

    public void PlayVoiceLine(VoiceLine line)
    {
        if (line == null || string.IsNullOrEmpty(line.audioClipName))
            return;
        // Удаляем расширение, если оно есть
        string clipName = System.IO.Path.GetFileNameWithoutExtension(line.audioClipName);

        //AudioClip clip = Resources.Load<AudioClip>("Audio/" + "test");
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + clipName);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Аудиофайл {line.audioClipName} не найден!");
        }
    }

    private System.Collections.IEnumerator PlayVoiceLineWithBlock(VoiceLine line)
    {
        isVoiceLinePlaying = true;
        PlayVoiceLine(line);
        yield return new WaitForSecondsRealtime(6f); // 4 секунды блокировки
        isVoiceLinePlaying = false;
    }

    public void ClearUsedVoiceLines()
    {
        usedVoiceLines.Clear();
    }

    public string GetUsedVoiceLinesAsString()
    {
        if (usedVoiceLines.Count == 0)
            return "(пусто)";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var line in usedVoiceLines)
        {
            sb.AppendLine(line);
        }
        return sb.ToString();
    }
}


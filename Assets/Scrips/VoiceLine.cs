using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VoiceLine
{
    public List<string> tags;
    public string text;
    [HideInInspector] public int tagCount; // Предпосчитанное количество тегов
}
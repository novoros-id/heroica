using System;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        // Оборачиваем JSON-массив в "Items" для корректного парсинга
        string newJson = "{ \"Items\": " + json + " }";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper?.Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
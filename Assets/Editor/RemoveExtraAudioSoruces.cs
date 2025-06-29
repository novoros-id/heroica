using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class RemoveExtraAudioSources : EditorWindow
{
    [MenuItem("Tools/Удалить лишние AudioSource из сцен")]
    static void RemoveExtraSources()
    {
        string[] sceneGUIDs = AssetDatabase.FindAssets("t:Scene");
        Debug.Log($"Найдено сцен: {sceneGUIDs.Length}");

        foreach (string guid in sceneGUIDs)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log($"Открываем сцену: {scenePath}");

            // Открываем сцену в редакторе
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            // Получаем все объекты на сцене
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (GameObject root in rootObjects)
            {
                // Находим все AudioSource в объекте и его дочерних элементах
                AudioSource[] sources = root.GetComponentsInChildren<AudioSource>(false);

                foreach (var source in sources)
                {
                    // Проверяем, является ли компонент частью префаба
                    PrefabAssetType assetType = PrefabUtility.GetPrefabAssetType(source);
                    bool isPartOfPrefab = assetType == PrefabAssetType.Regular || assetType == PrefabAssetType.Variant;

                    if (!isPartOfPrefab)
                    {
                        // Это override → удаляем его
                        Debug.Log($"[Удалён] Лишний AudioSource на объекте: {source.gameObject.name}", source.gameObject);
                        Undo.DestroyObjectImmediate(source);
                    }
                }
            }

            // Сохраняем изменения в сцене
            EditorSceneManager.SaveOpenScenes();
        }

        Debug.Log("✅ Все лишние AudioSource удалены из всех сцен!");
    }
}
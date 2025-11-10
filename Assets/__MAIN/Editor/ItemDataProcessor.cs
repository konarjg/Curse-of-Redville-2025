namespace __MAIN.Editor {
  using System.Collections.Generic;
  using Source.Inventory;
  using Source.Inventory.Items;
  using UnityEditor;
  using UnityEngine;

  public class ItemDataProcessor : AssetPostprocessor {
    private static Dictionary<string,string> _idToPathMap = new();

    [InitializeOnLoadMethod]
    private static void InitializeCache() {
      BuildIdCache();
    }

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
      bool cacheNeedsRebuild = false;

      foreach (string path in importedAssets) {
        if (!path.EndsWith(".asset")) {
          continue;
        }
        
        ItemData itemData = AssetDatabase.LoadAssetAtPath<ItemData>(path);
        
        if (itemData == null) {
          continue;
        }

        if (string.IsNullOrEmpty(itemData.Id)) {
          itemData.GenerateId();
          EditorUtility.SetDirty(itemData);
          Debug.Log($"[ItemDataProcessor] Assigned new ID to newly created item: {path}");
          cacheNeedsRebuild = true;
          continue;
        }

        if (_idToPathMap.TryGetValue(itemData.Id, out string existingPath) && existingPath != path) {
          ItemData oldAsset = AssetDatabase.LoadAssetAtPath<ItemData>(existingPath);
          
          if (oldAsset == null) {
            continue;
          }
          
          itemData.GenerateId();
          EditorUtility.SetDirty(itemData);
          Debug.LogWarning($"[ItemDataProcessor] Detected duplicate ID on '{path}'. Regenerated a new unique ID: {itemData.Id}");
          cacheNeedsRebuild = true;
        }
      }

      if (deletedAssets.Length <= 0 && movedAssets.Length <= 0 && !cacheNeedsRebuild) {
        return;
      }

      BuildIdCache();
    }

    [MenuItem("Tools/Curse of Redville/Validate Item IDs")]
    private static void BuildIdCache() {
      _idToPathMap.Clear();
      string[] guids = AssetDatabase.FindAssets("t:ItemData");

      foreach (string guid in guids) {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        ItemData itemData = AssetDatabase.LoadAssetAtPath<ItemData>(path);

        if (itemData == null || string.IsNullOrEmpty(itemData.Id)) {
          continue;
        }

        if (_idToPathMap.ContainsKey(itemData.Id)) {
          Debug.LogError($"[ItemDataProcessor] CRITICAL DUPLICATE ID: '{itemData.Id}'. Found on assets:\n1: {path}\n2: {_idToPathMap[itemData.Id]}\nPlease manually regenerate one using the Context Menu.");
          continue;
        } 
        
        _idToPathMap.Add(itemData.Id, path);
      }
    }
  }
}
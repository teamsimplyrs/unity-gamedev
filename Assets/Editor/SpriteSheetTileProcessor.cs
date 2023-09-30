using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteSheetToPaletteProcessor : EditorWindow
{
    private Texture2D spriteSheet;
    private int tileSize = 32;
    private GameObject tilePalettePrefab;


    [MenuItem("Tools/SpriteSheet to Tile Palette Processor")]
    public static void ShowWindow()
    {
        GetWindow<SpriteSheetToPaletteProcessor>("SpriteSheet to Tile Palette Processor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Slice Sprite Sheet and Add to Tile Palette", EditorStyles.boldLabel);

        spriteSheet = (Texture2D)EditorGUILayout.ObjectField("Sprite Sheet", spriteSheet, typeof(Texture2D), false);
        tileSize = EditorGUILayout.IntField("Tile Size", tileSize);
        tilePalettePrefab = (GameObject)EditorGUILayout.ObjectField("Tile Palette Prefab", tilePalettePrefab, typeof(GameObject), false);

        if (GUILayout.Button("Process"))
        {
            if (spriteSheet && tilePalettePrefab)
            {
                SliceSpriteSheet();
                CreateTilesAndAddToPalette();
            }
        }
    }

    private void SliceSpriteSheet()
    {
        // Assuming square tiles for simplicity
        int tilesX = spriteSheet.width / tileSize;
        int tilesY = spriteSheet.height / tileSize;

        string spriteAssetPath = AssetDatabase.GetAssetPath(spriteSheet);
        string directory = System.IO.Path.GetDirectoryName(spriteAssetPath);
        string outputPath = directory + "/Sliced/";

        // Check if directory exists, if not, create it
        if (!System.IO.Directory.Exists(outputPath))
        {
            System.IO.Directory.CreateDirectory(outputPath);
        }

        TextureImporter ti = AssetImporter.GetAtPath(spriteAssetPath) as TextureImporter;
        ti.isReadable = true;
        ti.spriteImportMode = SpriteImportMode.Multiple;

        System.Collections.Generic.List<SpriteMetaData> newData = new System.Collections.Generic.List<SpriteMetaData>();

        for (int y = 0; y < tilesY; y++)
        {
            for (int x = 0; x < tilesX; x++)
            {
                SpriteMetaData smd = new SpriteMetaData
                {
                    alignment = 0,
                    border = new Vector4(),
                    name = spriteSheet.name + "_tile_" + x + "_" + y,
                    pivot = new Vector2(0.5f, 0.5f),
                    rect = new Rect(x * tileSize, spriteSheet.height - (y + 1) * tileSize, tileSize, tileSize)
                };

                newData.Add(smd);
            }
        }

        ti.spritesheet = newData.ToArray();
        AssetDatabase.ImportAsset(spriteAssetPath, ImportAssetOptions.ForceUpdate);
    }

    private void CreateTilesAndAddToPalette()
    {
        if (!tilePalettePrefab)
        {
            Debug.LogError("Tile Palette Prefab not set.");
            return;
        }

        // Instantiate the prefab in the scene
        GameObject instance = PrefabUtility.InstantiatePrefab(tilePalettePrefab) as GameObject;
        Tilemap targetTilemap = instance.GetComponentInChildren<Tilemap>();
        targetTilemap.ClearAllTiles();

        if (!targetTilemap)
        {
            Debug.LogError("No Tilemap found in the Tile Palette Prefab.");
            DestroyImmediate(instance);
            return;
        }

        string spriteAssetPath = AssetDatabase.GetAssetPath(spriteSheet);

        Object[] objects = AssetDatabase.LoadAllAssetsAtPath(spriteAssetPath);
        foreach (Object o in objects)
        {
            if (o is Sprite)
            {
                Sprite sprite = (Sprite)o;

                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = sprite;

                string assetPath = "Assets/Tiles/" + sprite.name + ".asset";  // Change path as needed
                AssetDatabase.CreateAsset(tile, assetPath);

                // Assuming you have a rectangular grid and tiles fit perfectly without overlaps
                Vector3Int position = new Vector3Int((int)(sprite.rect.x / tileSize), (int)(sprite.rect.y / tileSize), 0);
                Debug.Log(position);
                targetTilemap.SetTile(position, tile);
            }
        }

        // Apply changes to the prefab
        PrefabUtility.ApplyPrefabInstance(instance, InteractionMode.UserAction);

        // Destroy the instance from the scene
        DestroyImmediate(instance);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void CreateTiles()
    {
        string spriteAssetPath = AssetDatabase.GetAssetPath(spriteSheet);
        string directory = System.IO.Path.GetDirectoryName(spriteAssetPath);
        string outputPath = directory + "/Sliced/";

        Object[] objects = AssetDatabase.LoadAllAssetsAtPath(spriteAssetPath);
        foreach (Object o in objects)
        {
            if (o is Sprite)
            {
                Sprite sprite = (Sprite)o;

                // For this example, I'm assuming you're using the basic Tile. Replace with your custom tile type if needed.
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = sprite;

                string tilePath = outputPath + sprite.name + ".asset";
                AssetDatabase.CreateAsset(tile, tilePath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

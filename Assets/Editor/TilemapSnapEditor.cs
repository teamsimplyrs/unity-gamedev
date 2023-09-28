using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[InitializeOnLoad]
public class TilemapAutoSnap
{
    private static Tilemap tilemap;
    private static bool isDragging;
    private static Vector3 initialMousePos;
    private static Vector3 initialObjectPos;
    private static bool autoSnapEnabled = true;

    static TilemapAutoSnap()
    {
        SceneView.duringSceneGui += OnSceneGUI;

        // Find the tilemap in the scene; adjust if you have multiple tilemaps
        tilemap = GameObject.FindObjectOfType<Tilemap>();

        // Ensure the correct initial state of the menu item
        EditorApplication.delayCall += () => {
            Menu.SetChecked("Tools/Auto Tilemap Snap", autoSnapEnabled);
        };
    }

    // Add a menu item in the Unity Editor to toggle the snapping
    [MenuItem("Tools/Auto Tilemap Snap")]
    private static void ToggleSnap()
    {
        autoSnapEnabled = !autoSnapEnabled;
        Menu.SetChecked("Tools/Auto Tilemap Snap", autoSnapEnabled);
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        if (autoSnapEnabled && Tools.current == Tool.Move)
        {
            foreach (Transform transform in Selection.transforms)
            {
                HandleDragging(transform);
            }
        }
    }

    private static void HandleDragging(Transform t)
    {
        Event e = Event.current;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    initialMousePos = e.mousePosition;
                    initialObjectPos = t.position;
                    isDragging = true;
                    e.Use();
                }
                break;

            case EventType.MouseDrag:
                if (isDragging)
                {
                    Vector3 offset = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin -
                                      HandleUtility.GUIPointToWorldRay(initialMousePos).origin;

                    t.position = initialObjectPos + offset;

                    if (Mathf.Abs(offset.x) >= tilemap.cellSize.x * 0.5f ||
                        Mathf.Abs(offset.y) >= tilemap.cellSize.y * 0.5f ||
                        Mathf.Abs(offset.z) >= tilemap.cellSize.z * 0.5f)
                    {
                        SnapToTileCenter(t);
                    }

                    e.Use();
                }
                break;

            case EventType.MouseUp:
                if (isDragging && e.button == 0)
                {
                    isDragging = false;
                    SnapToTileCenter(t);
                    e.Use();
                }
                break;
        }
    }

    private static void SnapToTileCenter(Transform t)
    {
        if (tilemap != null)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(t.position);
            t.position = tilemap.GetCellCenterWorld(cellPosition);
        }
    }
}

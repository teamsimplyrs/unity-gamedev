using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestTile))]
public class TestTileEditor : Editor
{
    private SerializedProperty sprite;
    private SerializedProperty color;
    private SerializedProperty footstepColor;

    void OnEnable()
    {
        sprite = serializedObject.FindProperty("m_Sprite");
        color = serializedObject.FindProperty("m_Color");
        footstepColor = serializedObject.FindProperty("footstepColor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the sprite field
        EditorGUILayout.PropertyField(sprite);

        // Draw the sprite preview
        if (sprite.objectReferenceValue != null)
        {
            Sprite spriteObj = (Sprite)sprite.objectReferenceValue;
            Rect spriteRect = spriteObj.rect;
            float textureWidth = spriteObj.texture.width;
            float textureHeight = spriteObj.texture.height;

            Rect coords = new Rect(spriteRect.x / textureWidth, spriteRect.y / textureHeight, spriteRect.width / textureWidth, spriteRect.height / textureHeight);
            Rect previewRect = GUILayoutUtility.GetAspectRect((float)spriteRect.width / spriteRect.height, GUILayout.Width(100), GUILayout.ExpandWidth(false));

            GUI.DrawTextureWithTexCoords(previewRect, spriteObj.texture, coords);
        }

        // Draw other properties
        EditorGUILayout.PropertyField(color);
        EditorGUILayout.PropertyField(footstepColor);

        serializedObject.ApplyModifiedProperties();
    }

    // Check if there's a preview to display
    public override bool HasPreviewGUI()
    {
        return sprite.objectReferenceValue != null;
    }

    // Draw the preview content
    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        Sprite spriteObj = (Sprite)sprite.objectReferenceValue;
        if (spriteObj != null)
        {
            Rect spriteRect = spriteObj.rect;
            float textureWidth = spriteObj.texture.width;
            float textureHeight = spriteObj.texture.height;

            Rect coords = new Rect(spriteRect.x / textureWidth, spriteRect.y / textureHeight, spriteRect.width / textureWidth, spriteRect.height / textureHeight);

            GUI.DrawTextureWithTexCoords(r, spriteObj.texture, coords);
        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        TestTile tile = target as TestTile;
        if (tile.sprite == null)
            return null;

        // Extract the sprite's pixels from the original texture
        var croppedTexture = new Texture2D((int)tile.sprite.rect.width, (int)tile.sprite.rect.height);
        var pixels = tile.sprite.texture.GetPixels((int)tile.sprite.textureRect.x,
                                                   (int)tile.sprite.textureRect.y,
                                                   (int)tile.sprite.textureRect.width,
                                                   (int)tile.sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        // Scale the texture to the preview dimensions
        var scaledTexture = ScaleTexture(croppedTexture, width, height);

        return scaledTexture;
    }

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        result.filterMode = FilterMode.Point;

        float scaleX = (float)source.width / targetWidth;
        float scaleY = (float)source.height / targetHeight;
        int pixelX, pixelY;

        for (int y = 0; y < result.height; y++)
        {
            for (int x = 0; x < result.width; x++)
            {
                pixelX = Mathf.FloorToInt(x * scaleX);
                pixelY = Mathf.FloorToInt(y * scaleY);
                result.SetPixel(x, y, source.GetPixel(pixelX, pixelY));
            }
        }

        result.Apply();
        return result;
    }

}


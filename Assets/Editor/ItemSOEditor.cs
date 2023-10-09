using UnityEngine;
using UnityEditor;
using Inventory.Model;

[CustomEditor(typeof(ItemSO))]
public class ItemSOEditor : Editor
{
    public override bool HasPreviewGUI()
    {
        return true;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        ItemSO item = target as ItemSO;
        if (item && item.ItemSprite)
        {
            EditorGUI.DrawTextureTransparent(r, item.ItemSprite.texture, ScaleMode.ScaleToFit);
        }
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        ItemSO item = target as ItemSO;
        if (item && item.ItemSprite)
        {
            Texture2D tex = new Texture2D((int)item.ItemSprite.rect.width, (int)item.ItemSprite.rect.height, TextureFormat.RGBA32, false);

            Color[] spriteColors = item.ItemSprite.texture.GetPixels((int)item.ItemSprite.textureRect.x,
                                                                        (int)item.ItemSprite.textureRect.y,
                                                                        (int)item.ItemSprite.textureRect.width,
                                                                        (int)item.ItemSprite.textureRect.height);

            tex.SetPixels(spriteColors);
            tex.Apply();

            return ScaleTexture(tex, width, height);  // Use the resize method here.
        }
        return null;
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

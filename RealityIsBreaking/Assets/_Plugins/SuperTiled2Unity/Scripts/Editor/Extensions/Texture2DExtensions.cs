using System.Linq;
using UnityEngine;

namespace SuperTiled2Unity.Editor {
    public static class Texture2DExtensions {
        public static void BlitRectFrom(this Texture2D texture, int dx, int dy, Texture2D sourceTexture, Rect sourceRect) {
            var format = sourceTexture.format;
            if (format == TextureFormat.ARGB32 || format == TextureFormat.BGRA32 || format == TextureFormat.RGBA32) {
                // Order of magnitude faster
                Graphics.CopyTexture(sourceTexture, 0, 0, (int) sourceRect.x, (int) sourceRect.y, (int) sourceRect.width, (int) sourceRect.height, texture, 0, 0, dx, dy);
            }
            else {
                // So, why do we create a temporary render texture? Because the source texture may not be enabled for reading.
                // See this: https://support.unity3d.com/hc/en-us/articles/206486626-How-can-I-get-pixels-from-unreadable-textures-

                // Create a tempoary texture that has readable texture data. We will copy from that texture to our target.
                var tmp = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
                Graphics.Blit(sourceTexture, tmp);

                // Keep track of active render texture and push our temp
                var previous = RenderTexture.active;
                RenderTexture.active = tmp;

                // Copy the source texture into our copy
                var sourceTextureCopy = new Texture2D(sourceTexture.width, sourceTexture.height);
                sourceTextureCopy.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
                sourceTextureCopy.Apply();

                // Pop our temporary
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(tmp);

                // Do the actual Blit (from a copy of our source)
                var sx = (int) sourceRect.x;
                var sy = (int) sourceRect.y;
                var sw = (int) sourceRect.width;
                var sh = (int) sourceRect.height;

                var sourcePixels = sourceTextureCopy.GetPixels(sx, sy, sw, sh).Select(c => (Color32) c).ToArray();
                texture.SetPixels32(dx, dy, sw, sh, sourcePixels);

                // Destroy our source copy
                Object.DestroyImmediate(sourceTextureCopy);
            }
        }

        public static void BlitRectFrom(this Texture2D texture, float dx, float dy, Texture2D sourceTexture, Rect sourceRect) {
            texture.BlitRectFrom((int) dx, (int) dy, sourceTexture, sourceRect);
        }

        public static void CopyOwnPixels(this Texture2D texture, int dx, int dy, Rect sourceRect) {
            // Take for granted that our own texture is read/write enabled
            var sx = (int) sourceRect.x;
            var sy = (int) sourceRect.y;
            var sw = (int) sourceRect.width;
            var sh = (int) sourceRect.height;

            var pixels = texture.GetPixels(sx, sy, sw, sh).Select(c => (Color32) c).ToArray();
            //pixels = pixels.Select(p => NamedColors.Purple).ToArray();

            texture.SetPixels32(dx, dy, sw, sh, pixels);
        }

        public static void CopyOwnPixels(this Texture2D texture, float dx, float dy, Rect sourceRect) {
            texture.CopyOwnPixels((int) dx, (int) dy, sourceRect);
        }
    }
}
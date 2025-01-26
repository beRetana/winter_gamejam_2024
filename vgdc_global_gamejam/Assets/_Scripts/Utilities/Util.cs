using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
public static class Util
{
    // Transform helpers
    public static void SetPosition(this Transform transform, Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
    public static void SetPositionX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    public static void SetPositionY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public static void SetPositionZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    public static void SetLocalPositionX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }
    public static void SetLocalPositionY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }
    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }
    public static void AddRotation(this Transform transform, float angle)
    {
        transform.rotation *= Quaternion.Euler(0, 0, angle);
    }
    public static void SetLocalScaleX(this Transform transform, float scaleX)
    {
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
    public static void SetLocalScaleY(this Transform transform, float scaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
    public static void SetLocalScaleZ(this Transform transform, float scaleZ)
    {
        transform.localScale = new Vector3(transform.localScale.z, transform.localScale.y, scaleZ);
    }
    public static void SetRotationX(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
    public static void SetRotationY(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
    }
    public static void SetRotationZ(this Transform transform, float angle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
    }
    public static void SetLocalRotationX(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(angle, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }
    public static void SetLocalRotationY(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, angle, transform.localRotation.eulerAngles.z);
    }
    public static void SetLocalRotationZ(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, angle);
    }

    /// <summary>
    /// Returns a list containing the transforms of children objects without their own children
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static List<Transform> GetChildTransforms(this Transform root)
    {
        List<Transform> list = new List<Transform>();
        StoreTransforms(root, list);
        return list;
    }
    private static void StoreTransforms(Transform root, List<Transform> list)
    {
        if (root.childCount == 0)
        {
            list.Add(root);
        }
        else
        {
            for (int i = 0, len = root.childCount; i < len; i++)
            {
                StoreTransforms(root.GetChild(i), list);
            }
        }
    }
    public static void EnableTransforms(this List<Transform> transforms)
    {
        foreach (Transform transform in transforms)
        {
            transform.gameObject.SetActive(true);
        }
    }
    public static void DisableTransforms(this List<Transform> transforms)
    {
        foreach (Transform transform in transforms)
        {
            transform.gameObject.SetActive(false);
        }
    }

    // GameObject helpers
    public static void EnableObjects(this List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
    public static void DisableObjects(this List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
    public static List<GameObject> GetChildrenObjects(this Transform root)
    {
        List<GameObject> gameObjects = new();
        for (int i = 0, len = root.childCount; i < len; ++i)
        {
            gameObjects.Add(root.GetChild(i).gameObject);
        }
        return gameObjects;
    }

    // RectTransform helpers
    public static void SetPosX(this RectTransform rectTransform, float x)
    {
        rectTransform.localPosition = new Vector3(x, rectTransform.localPosition.y, rectTransform.localPosition.z);
    }
    public static void SetPosY(this RectTransform rectTransform, float y)
    {
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, y, rectTransform.localPosition.z);
    }
    public static void SetWidth(this RectTransform rectTransform, float width)
    {
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }
    public static void SetHeight(this RectTransform rectTransform, float height)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }
    // SpriteRenderer helpers
    public static void SetAlpha(this SpriteRenderer spriteRenderer, float opacity)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacity);
    }
    public static void AddAlpha(this SpriteRenderer spriteRenderer, float amount)
    {
        spriteRenderer.color += new Color(0, 0, 0, amount);
    }
    public static IEnumerator<SpriteRenderer> FadeAlpha(SpriteRenderer spriteRenderer, float targetOpacity, float duration)
    {
        if (spriteRenderer.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = spriteRenderer.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            spriteRenderer.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        spriteRenderer.SetAlpha(targetOpacity);
        yield break;
    }
    // Image helpers
    public static void SetAlpha(this Image image, float opacity)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
    }
    // Tilemap helpers
    public static void SetAlpha(this Tilemap tilemap, float opacity)
    {
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, opacity);
    }
    public static IEnumerator<Tilemap> FadeAlpha(Tilemap tilemap, float targetOpacity, float duration)
    {
        if (tilemap.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = tilemap.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            tilemap.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        tilemap.SetAlpha(targetOpacity);
        yield break;
    }
    // Camera helpers
    public static IEnumerator<Camera> FadeColor(Camera camera, Color targetColor, float duration)
    {
        if (camera.backgroundColor == targetColor) { yield break; }
        float currentTime = 0;
        Color start = camera.backgroundColor;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            camera.backgroundColor = Color.Lerp(start, targetColor, currentTime / duration);
            yield return null;
        }
        camera.backgroundColor = targetColor;
        yield break;
    }
    // TMPro helpers
    public static void SetAlpha(this TextMeshPro text, float opacity)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }
    public static IEnumerator<TextMeshPro> FadeAlpha(TextMeshPro text, float targetOpacity, float duration)
    {
        if (text.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = text.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        text.SetAlpha(targetOpacity);
        yield break;
    }
    public static void SetAlpha(this TextMeshProUGUI text, float opacity)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }
    public static IEnumerator<TextMeshProUGUI> FadeAlpha(TextMeshProUGUI text, float targetOpacity, float duration)
    {
        if (text.color.a == targetOpacity) { yield break; }
        float currentTime = 0;
        float start = text.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.SetAlpha(Mathf.Lerp(start, targetOpacity, currentTime / duration));
            yield return null;
        }
        text.SetAlpha(targetOpacity);
        yield break;
    }
    // Vector helpers
    public static Vector3Int ToVector3Int(Vector3 vector3)
    {
        return new Vector3Int((int)vector3.x, (int)vector3.y, (int)vector3.z);
    }
    public static Vector3 ToVector3(this Vector2 vector2, float z)
    {
        return new Vector3(vector2.x, vector2.y, z);
    }
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
    public static Vector3 SetX(this Vector3 vector3, float value)
    {
        return new Vector3(value, vector3.y, vector3.z);
    }
    public static Vector3 SetY(this Vector3 vector3, float value)
    {
        return new Vector3(vector3.x, value, vector3.z);
    }
    public static Vector3 SetZ(this Vector3 vector3, float value)
    {
        return new Vector3(vector3.x, vector3.y, value);
    }
    // String helpers
    public static string RemoveSpaces(this string text)
    {
        return text.Replace(" ", "");
    }
    public static string AddSpaces(this string text)
    {
        string finalText = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && i != 0)
            {
                finalText += " ";
            }
            finalText += text[i];
        }
        return finalText;
    }
    public static string ToCamelCase(this string text)
    {
        string finalText = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (i == 0)
            {
                finalText += char.ToLower(text[0]);
            }
            else if (text[i] != ' ')
            {
                finalText += text[i];
            }
        }
        return finalText;
    }
    public static KeyCode ToKeyCode(string s)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), s, true);
    }
    // Isometric player movement
    public static Vector3 ToIsometric(this Vector3 originalInput)
    {
        Quaternion defaultRotation = Quaternion.Euler(0, 45, 0);
        return Matrix4x4.Rotate(defaultRotation).MultiplyPoint3x4(originalInput);
    }

    // Animator helpers
    public static AnimationClip GetAnimation(this Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}
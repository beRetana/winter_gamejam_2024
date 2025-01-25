using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Allows for communication of data between scripts, decoupling
public class DataMessenger : MonoBehaviour
{
    private static Dictionary<string, bool> bools;
    private static Dictionary<string, float> floats;
    private static Dictionary<string, int> ints;
    private static Dictionary<string, string> strings;
    private static Dictionary<string, List<string>> stringLists;
    private static Dictionary<string, Vector2> vector2s;
    private static Dictionary<string, Vector3> vector3s;
    private static Dictionary<string, Quaternion> quaternions;
    private static Dictionary<string, GameObject> gameObjects;
    private static Dictionary<string, ScriptableObject> scriptableObjects;

    private static readonly string DEFAULT_STRING = string.Empty;
    private static readonly List<string> DEFAULT_STRING_LIST = new();
    private static readonly Vector3 DEFAULT_VECTOR = Vector3.zero;
    private static readonly Quaternion DEFAULT_QUATERNION = Quaternion.identity;

    private void Awake()
    {
        bools = new Dictionary<string, bool>();
        floats = new Dictionary<string, float>();
        ints = new Dictionary<string, int>();
        strings = new Dictionary<string, string>();
        stringLists = new Dictionary<string, List<string>>();
        vector2s = new Dictionary<string, Vector2>();
        vector3s = new Dictionary<string, Vector3>();
        quaternions = new Dictionary<string, Quaternion>();
        gameObjects = new Dictionary<string, GameObject>();
        scriptableObjects = new Dictionary<string, ScriptableObject>();
    }
    #region Bool
    public static bool GetBool(string key)
    {
        if (!bools.TryGetValue(key, out bool v))
        {
            bools[key] = default;
            return bools[key];
        }
        return v;
    }
    public static bool GetBool(BoolKey key)
    {
        return GetBool(key.ToString());
    }
    public static void SetBool(string key, bool value)
    {
        bools[key] = value;
    }
    public static void SetBool(BoolKey key, bool value)
    {
        SetBool(key.ToString(), value);
    }
    public static void ToggleBool(string key)
    {
        bools[key] = !bools[key];
    }
    public static void ToggleBool(BoolKey key)
    {
        ToggleBool(key.ToString());
    }
    public static IEnumerator WaitForBool(string key, bool doInvert = false)
    {
        while (doInvert ? !GetBool(key) : GetBool(key)) yield return null;
    }
    public static IEnumerator WaitForBool(BoolKey key, bool doInvert = false)
    {
        yield return WaitForBool(key.ToString(), doInvert);
    }
    #endregion Bool

    #region Float
    public static float GetFloat(string key)
    {
        if (!floats.TryGetValue(key, out float v))
        {
            floats[key] = default;
            return floats[key];
        }
        return v;
    }
    public static float GetFloat(FloatKey key)
    {
        return GetFloat(key.ToString());
    }
    public static void SetFloat(string key, float value)
    {
        floats[key] = value;
    }
    public static void SetFloat(FloatKey key, float value)
    {
        SetFloat(key.ToString(), value);
    }
    /// <summary>
    /// Performs an operation on the float associated with the given key with the value given. The operator is + by default.
    /// </summary>
    public static float OperateFloat(string key, float value, char op = '+')
    {
        switch (op)
        {
            case '+':
                SetFloat(key, GetFloat(key) + value);
                break;
            case '*':
                SetFloat(key, GetFloat(key) * value);
                break;
            case '/':
                SetFloat(key, GetFloat(key) / value);
                break;
        }
        return floats[key];
    }
    /// <summary>
    /// Performs an operation on the float associated with the given key with the value given. The operator is + by default.
    /// </summary>
    public static float OperateFloat(FloatKey key, float value, char op = '+')
    {
        return OperateFloat(key.ToString(), value, op);
    }
    #endregion Float

    #region GameObject
    public static GameObject GetGameObject(string key)
    {
        if (!gameObjects.TryGetValue(key, out GameObject obj))
        {
            gameObjects[key] = default;
            return gameObjects[key];
        }
        return obj;
    }
    public static GameObject GetGameObject(GameObjectKey key)
    {
        return GetGameObject(key.ToString());
    }
    public static void SetGameObject(string key, GameObject obj)
    {
        gameObjects[key] = obj;
    }
    public static void SetGameObject(GameObjectKey key, GameObject obj)
    {
        SetGameObject(key.ToString(), obj);
    }
    #endregion GameObject

    #region Int
    public static int GetInt(string key)
    {
        if (!ints.TryGetValue(key, out int v))
        {
            ints[key] = default;
            return ints[key];
        }
        return v;
    }
    public static int GetInt(IntKey key)
    {
        return GetInt(key.ToString());
    }
    public static void SetInt(string key, int value)
    {
        ints[key] = value;
    }
    public static void SetInt(IntKey key, int value)
    {
        SetInt(key.ToString(), value);
    }
    /// <summary>
    /// Performs an operation on the int associated with the given key with the int given. The operator is + by default.
    /// </summary>
    public static int OperateInt(string key, int value, char op = '+')
    {
        switch (op)
        {
            case '+':
                SetInt(key, GetInt(key) + value);
                break;
            case '*':
                SetInt(key, GetInt(key) * value);
                break;
            case '/':
                SetInt(key, GetInt(key) / value);
                break;
        }
        return ints[key];
    }
    /// <summary>
    /// Performs an operation on the int associated with the given key with the int given. The operator is + by default.
    /// </summary>
    public static int OperateInt(IntKey key, int value, char op = '+')
    {
        return OperateInt(key.ToString(), value, op);
    }

    /// <summary>
    /// Performs an operation on the int associated with the given key with the float given. 
    /// </summary>
    /// <param name="op">The operator is + by default.</param>
    /// <param name="doRound">Truncates instead of rounds by default.</param>
    public static int OperateInt(string key, float value, char op = '+', bool doRound = false)
    {
        switch (op)
        {
            case '+':
                SetInt(key, (int)(GetInt(key) + value + (doRound ? 0.5f : 0)));
                break;
            case '*':
                SetInt(key, (int)(GetInt(key) * value + (doRound ? 0.5f : 0)));
                break;
            case '/':
                SetInt(key, (int)(GetInt(key) / value + (doRound ? 0.5f : 0)));
                break;
        }
        return ints[key];
    }
    /// <summary>
    /// Performs an operation on the int associated with the given key with the float given. 
    /// </summary>
    /// <param name="op">The operator is + by default.</param>
    /// <param name="doRound">Truncates instead of rounds by default.</param>
    public static int OperateInt(IntKey key, float value, char op = '+', bool doRound = false)
    {
        return OperateInt(key.ToString(), value, op, doRound);
    }
    #endregion Int
    #region Quaternion
    public static Quaternion GetQuaternion(string key)
    {
        if (!quaternions.TryGetValue(key, out Quaternion v))
        {
            quaternions[key] = DEFAULT_QUATERNION;
            return quaternions[key];
        }
        return v;
    }
    public static Quaternion GetQuaternion(QuaternionKey key)
    {
        return GetQuaternion(key.ToString());
    }
    public static void SetQuaternion(string key, Quaternion value)
    {
        quaternions[key] = value;
    }
    public static void SetQuaternion(QuaternionKey key, Quaternion value)
    {
        SetQuaternion(key.ToString(), value);
    }
    #endregion Quaternion

    #region ScriptableObject
    public static ScriptableObject GetScriptableObject(string key)
    {
        if (!scriptableObjects.TryGetValue(key, out ScriptableObject obj))
        {
            scriptableObjects[key] = default;
            return scriptableObjects[key];
        }
        return obj;
    }
    public static ScriptableObject GetScriptableObject(ScriptableObjectKey key)
    {
        return GetScriptableObject(key.ToString());
    }
    public static void SetScriptableObject(string key, ScriptableObject obj)
    {
        scriptableObjects[key] = obj;
    }
    public static void SetScriptableObject(ScriptableObjectKey key, ScriptableObject obj)
    {
        SetScriptableObject(key.ToString(), obj);
    }
    #endregion ScriptableObject

    #region String

    public static string GetString(string key)
    {
        if (!strings.TryGetValue(key, out string v))
        {
            strings[key] = DEFAULT_STRING;
            return strings[key];
        }
        return v;
    }
    public static string GetString(StringKey key)
    {
        return GetString(key.ToString());
    }
    public static void SetString(string key, string value)
    {
        strings[key] = value;
    }
    public static void SetString(StringKey key, string value)
    {
        SetString(key.ToString(), value);
    }
    #endregion String

    #region StringList
    public static List<string> GetStringList(string key)
    {
        if (!stringLists.TryGetValue(key, out List<string> v))
        {
            stringLists[key] = DEFAULT_STRING_LIST;
            return stringLists[key];
        }
        return v;
    }
    public static List<string> GetStringList(StringListKey key)
    {
        return GetStringList(key.ToString());
    }
    public static void SetStringList(string key, List<string> value)
    {
        stringLists[key] = value;
    }
    public static void SetStringList(StringListKey key, List<string> value)
    {
        SetStringList(key.ToString(), value);
    }
    public static void AddStringToList(string key, string value)
    {
        List<string> list;
        if (!stringLists.TryGetValue(key, out list))
        {
            stringLists[key] = DEFAULT_STRING_LIST;
        }
        list.Add(value);
    }
    public static void AddStringToList(StringListKey key, string value)
    {
        AddStringToList(key.ToString(), value);
    }
    /// <returns>Whether the string was removed.</returns>
    public static bool RemoveStringFromList(string key, string value)
    {
        List<string> list;
        if (!stringLists.TryGetValue(key, out list))
        {
            return false;
        }
        list.Remove(value);
        return true;
    }
    /// <returns>Whether the string was removed.</returns>
    public static bool RemoveStringFromList(StringKey key, string value)
    {
        return RemoveStringFromList(key.ToString(), value);
    }
    #endregion StringList

    #region Vector3
    public static Vector3 GetVector3(string key)
    {
        if (!vector3s.TryGetValue(key, out Vector3 v))
        {
            vector3s[key] = DEFAULT_VECTOR;
            return vector3s[key];
        }
        return v;
    }
    public static Vector3 GetVector3(Vector3Key key)
    {
        return GetVector3(key.ToString());
    }

    public static void SetVector3(string key, Vector3 value)
    {
        vector3s[key] = value;
    }
    public static void SetVector3(Vector3Key key, Vector3 value)
    {
        SetVector3(key.ToString(), value);
    }
    #endregion Vector3
    #region Vector2
    public static Vector3 GetVector2(string key)
    {
        if (!vector2s.TryGetValue(key, out Vector2 v))
        {
            vector2s[key] = DEFAULT_VECTOR;
            return vector2s[key];
        }
        return v;
    }
    public static Vector3 GetVector2(Vector2Key key)
    {
        return GetVector2(key.ToString());
    }

    public static void SetVector2(string key, Vector2 value)
    {
        vector2s[key] = value;
    }
    public static void SetVector2(Vector2Key key, Vector2 value)
    {
        SetVector2(key.ToString(), value);
    }
    #endregion Vector2
}
#region KeyEnums
public enum BoolKey
{
    IsBallInPlay
}
public enum FloatKey
{
    CurrentBallSpeed,
    CurrentBallGravityScale
}
public enum IntKey
{
    CurrentScore,
    ScoreToAdd,
    NewPegID,
    RemainingBallCount,
}
public enum StringKey
{

}
public enum StringListKey
{

}
public enum Vector2Key
{
    BulletDirection,
}
public enum Vector3Key
{
}
public enum QuaternionKey
{

}
public enum GameObjectKey
{
    NewPegObject,
    PlayerBall,
    PegManager
}
public enum ScriptableObjectKey
{

}
#endregion KeyEnums
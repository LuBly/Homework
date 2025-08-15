
using System.Linq;
using UnityEngine;
using URandom = UnityEngine.Random;


public static class UnityExtensions
{
    #region [ Component ]
    public static T GetOrAddComponent<T>(this Transform transform) where T : Component
    {
        T comp = transform.GetComponent<T>();
        if (comp == null)
            comp = transform.gameObject.AddComponent<T>();

        return comp;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T comp = gameObject.GetComponent<T>();
        if (comp == null)
            comp = gameObject.AddComponent<T>();

        return comp;
    }

    static public T FindComponent<T>(this Transform transform, string name) where T : Component
    {
        Transform child = transform.Find(name);
        if (child != null)
            return child.GetComponent<T>();
        return null;
    }

    static public T FindComponent<T>(this Component component, string name) where T : Component
    {
        return FindComponent<T>(component.transform, name);
    }

    static public T FindOrAddComponent<T>(this Transform transform, string name) where T : Component
    {
        Transform child = transform.Find(name);
        if (child != null)
            return child.GetOrAddComponent<T>();
        return null;
    }

    static public T FindOrAddComponent<T>(this Component component, string name) where T : Component
    {
        return FindOrAddComponent<T>(component.transform, name);
    }

    static public GameObject FindChildObject(this Transform transform, string name)
    {
        Transform child = transform.Find(name);
        if (child != null)
            return child.gameObject;

        return null;
    }

    static public GameObject FindChildObject(this Component component, string name)
    {
        Transform child = component.transform.Find(name);
        if (child != null)
            return child.gameObject;

        return null;
    }

    static public Transform SearchInChildren(this Transform transform, string name, bool includeInactive = false)
    {
        var allKids = transform.GetComponentsInChildren<Transform>(includeInactive);
        return allKids.Where(k => k.gameObject.name == name).FirstOrDefault();
    }

    static public void SetActive(this Component component, bool active)
    {
        component.gameObject.SetActive(active);
    }

    public static void SetLayer(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
    }

    public static void SetLayers(this GameObject gameObject, int layer, bool includeInactive = true)
    {
        var transforms = gameObject.GetComponentsInChildren<Transform>(includeInactive);
        foreach (Transform child in transforms)
            child.gameObject.layer = layer;
    }
    #endregion [ Component ]

    #region [ Vector ]
    public static Vector3 Divide(this Vector3 v, Vector3 other)
    {
        return new Vector3(v.x / other.x, v.y / other.y, v.z / other.z);
    }
    #endregion [ Vector ]

    #region [ Rect ]
    public static Vector2 Limit(this Rect rect, Vector2 point)
    {
        point.x = Mathf.Clamp(point.x, rect.xMin, rect.xMax);
        point.y = Mathf.Clamp(point.y, rect.yMin, rect.yMax);

        return point;
    }

    public static Vector2 RandomPoint(this Rect rect)
    {
        Vector2 point;
        point.x = URandom.Range(rect.xMin, rect.xMax);
        point.y = URandom.Range(rect.yMin, rect.yMax);

        return point;
    }

    public static bool CompletelyInsideCircle(this Rect rect, Vector2 point, float radius)
    {
        float diameter = radius * 2f;
        if (rect.width < diameter || rect.height < diameter) return false;

        if (point.x < rect.xMin + radius) return false;
        if (rect.xMax - radius < point.x) return false;
        if (point.y < rect.yMin + radius) return false;
        if (rect.yMax - radius < point.y) return false;

        return true;
    }

    public static Rect DownSize(this Rect rect, float down)
    {
        Rect result = new Rect(rect);

        float diameter = down * 2f;
        if (rect.width < diameter)
            result.width = 0;
        else
        {
            result.xMin += down;
            result.xMax -= down;
        }

        if (rect.height < diameter)
            result.height = 0;
        else
        {
            result.yMin += down;
            result.yMax -= down;
        }

        return result;
    }
    #endregion [ Rect ]

    #region [ Canvas ]
    public static Vector2 ToLocalPoint(this Canvas canvas, RectTransform rect, Vector3 screenPos)
    {
        if (canvas.renderMode != RenderMode.ScreenSpaceCamera)
            return screenPos;

        Vector2 localPos;
        // this UI element has been clicked by the mouse so determine the local position on your UI element
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, canvas.worldCamera, out localPos);

        return localPos;
    }

    public static Vector3 ToWorldPos(this Canvas canvas, Vector2 screenPos)
    {
        if (canvas.renderMode != RenderMode.ScreenSpaceCamera)
            return screenPos;

        Vector3 scrPos = screenPos;
        scrPos.z = canvas.planeDistance;

        return canvas.worldCamera.ScreenToWorldPoint(scrPos);
    }

    public static Vector2 ToScreenPos(this Canvas canvas, Vector3 worldPosition)
    {
        Camera uiCam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera != null)
            uiCam = canvas.worldCamera;

        return RectTransformUtility.WorldToScreenPoint(uiCam, worldPosition);
    }

    public static Rect GetScreenSpaceRect(this Canvas canvas, RectTransform rt)
    {
        Camera uiCam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera != null)
            uiCam = canvas.worldCamera;

        var corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        for (var i = 0; i < corners.Length; i++)
            corners[i] = RectTransformUtility.WorldToScreenPoint(uiCam, corners[i]);

        var position = corners[0];
        var size = corners[2] - corners[0];

        return new Rect(position, size);
    }
    #endregion [ Canvas ]

    #region [ Utils ]
    public static float MaxAbsUnit(this Vector3 vector)
    {
        return Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
    #endregion [ Utils ]

    #region [ Animator ]
    public static bool HasParameter(this Animator animator, string name)
    {
        if (animator == null || string.IsNullOrEmpty(name))
            return false;
        return animator.parameters.Any(param => param.name == name);
    }
    #endregion [ Animator ]
}

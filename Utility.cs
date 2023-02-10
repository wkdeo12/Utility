using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class Utility
{
    #region UI Control
    public static void UpdateText(TextMeshProUGUI text, string value)
    {
        text.text = value.ToString();
    }
    public static void UpdateText(TextMeshProUGUI text, int value)
    {
        text.text = value.ToString();
    }
    public static void UpdateText(TextMeshProUGUI text, float value)
    {
        text.text = value.ToString();
    }
    public static void UpdateText<T>(TextMeshProUGUI text, T value)
    {
        text.text = value.ToString();
    }
    public static void OpenWindow(GameObject window, UnityEvent openAction = null)
    {
        window.gameObject.SetActive(true);
        if (openAction != null)
            openAction.Invoke();
    }
    public static void CloseWindow(GameObject window, UnityEvent closeAction = null)
    {
        window.gameObject.SetActive(false);
        if (closeAction != null)
            closeAction.Invoke();
    }
    public static void ChangeUIIMG(ref Image img, Sprite sprite)
    {
        img.sprite = sprite;
    }

    #endregion
    public static void LookAtPosition(Transform transform, Vector2 position, float offset = 0)
    {
        Vector3 relative = transform.InverseTransformPoint(position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle + offset);
    }
    
    public static List<T> GetRandomData<T>(ref List<T> dataList, int count,bool remove)
    {
        List<T> l = new List<T>();
        for (int i = 0; i < count; i++)
        {
            int randomValue = Random.Range(0, dataList.Count);
            var o = dataList[randomValue];
            if (remove)
            {
                dataList.RemoveAt(randomValue);
            }
            l.Add(o);
        }
        return l;
    }

    #region Timer
    public static bool Timer(ref float value, float duration)
    {
        value += Time.deltaTime;
        if (value >= duration)
        {
            value = 0;
            return true;
        }
        return false;
    }

    public static bool Timer(ref float value, float duration, bool timeScale)
    {
        value += timeScale ? Time.deltaTime : Time.unscaledDeltaTime;
        if (value >= duration)
        {
            value = 0;
            return true;
        }
        return false;
    }

    public static bool TimerInverse(ref float value, float duration)
    {
        if (value >= duration)
        {
            value = 0;
            return false;
        }
        value = Mathf.Clamp(value + Time.deltaTime, 0, duration);
        return true;
    }

    public static bool TimerReverse(ref float value, float limit)
    {
        if (value <= limit)
        {
            value = limit;
            return true;
        }
        value = Mathf.Max(value - Time.deltaTime, limit);
        return false;
    }
    #endregion

    public static void Add<T>(ref List<T> targetList, ref T addData, UnityAction addEvent = null,bool remove = false)
    {
        if (addData != null)
        {
            targetList.Add(addData);
            if (addEvent != null)
            {
                addEvent.Invoke();
            }
            if (remove)
                addData = default;
        }
    }

    public static void AddFromList<T>(ref List<T> targetList, ref List<T> currentList,int idx,bool remove = false,UnityAction action = null)
    {
        targetList.Add(currentList[idx]);
        if (action != null)
            action.Invoke();
        if (remove)
            currentList.RemoveAt(idx);
    }
    public static void AddList<T>(ref List<T> targetList, ref List<T> currentList, bool remove = false)
    {
        targetList.AddRange(currentList);
        
        if (remove)
            currentList.Clear();
    }


    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

using Random = UnityEngine.Random;

public static class Extensions
{
    public static T Choose<T>(this T[] array)
    {
        if(array.Length == 0) return default;

        int index = Random.Range(0, array.Length);
        return array[index];
    }

    public static T Choose<T>(this T[] array, out int index)
    {
        index = -1;
        if(array.Length == 0) return default;

        index = Random.Range(0, array.Length);
        return array[index];
    }

    public static T Choose<T>(this List<T> list)
    {
        if(list.Count == 0) return default;

        int index = Random.Range(0, list.Count);
        return list[index];
    }

    public static List<T2> GetOrAdd<T1,T2>(this Dictionary<T1, List<T2>> dict, T1 key)
    {
        if(!dict.ContainsKey(key))
        {
            dict.Add(key, new List<T2>());
        }

        return dict[key];
    }

    public static Vector3 Sum(this IEnumerable<Vector3> collection)
    {
        Vector3 result = Vector3.zero;
        foreach(Vector3 item in collection) result += item;
        return result;
    }

    public static Vector3 Sum<T>(this IEnumerable<T> collection, Func<T, Vector3> selector)
    {
        Vector3 result = Vector3.zero;
        foreach(T item in collection) result += selector(item);
        return result;
    }

    public static Vector2 Sum(this IEnumerable<Vector2> collection)
    {
        Vector2 result = Vector2.zero;
        foreach(Vector2 item in collection) result += item;
        return result;
    }

    public static Vector2 Sum<T>(this IEnumerable<T> collection, Func<T, Vector2> selector)
    {
        Vector2 result = Vector2.zero;
        foreach(T item in collection) result += selector(item);
        return result;
    }

    public static IEnumerator SleepRoutine(float seconds)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
    }
}

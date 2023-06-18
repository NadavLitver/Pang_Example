using System.Collections.Generic;
using UnityEngine;
public interface IObjectPool<T>
{
    List<T> Pool { get; }
    T GetFromPool();
    void ReturnToPool(T obj);
    void PopulatePool();
}
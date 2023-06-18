using System.Collections.Generic;

public interface IObjectPool<T>
{
    List<T> Pool { get; }
    T GetFromPool();
    void ReturnToPool(T obj);
    void PopulatePool();
}
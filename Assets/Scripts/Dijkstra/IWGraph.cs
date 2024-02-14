using System.Collections.Generic;


public interface IWGraph<T>
{
    IEnumerable<T> Neighbors(T node);
    int getW(T node);
}
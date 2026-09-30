using System;

[Serializable]
public struct LevelValuePair<T>
{
    public EGameLevel Level;
    public T Value;
}
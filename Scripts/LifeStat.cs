using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthStat
{
    bool ApplyDamage(int playerIndex);

    bool IsZero();
}


public struct SharedHealthStat : IHealthStat
{
    public int Value;
    public int MaxValue;
    public SharedHealthStat(int MaxValue)
    {
        this.MaxValue = MaxValue;
        Value = MaxValue;
    }

    public bool IsZero()
    {
        return Value <= 0;
    }

    public bool ApplyDamage(int playerIndex)
    {
        Value -= 1;
        Value = Value < 0 ? 0 : Value;

        return true;
    }

}

public struct UniqueHealthStat : IHealthStat
{
    public Queue<int> Values;
    public int MaxValue;
    public int MaxPlayerNum;



    public UniqueHealthStat(int MaxValue, int MaxPlayerNum)
    {
        this.MaxValue = MaxValue;
        this.MaxPlayerNum = MaxPlayerNum;

        Values = new Queue<int>();

        for (int i = 0; i < MaxPlayerNum; i++)
        {
            Values.Enqueue(i);
        }
    }

    public bool IsZero()
    {
        return Values.Count == 0;
    }

    public bool ApplyDamage(int playerID)
    {
        int index = ConvertPlayerIndex(playerID);
        if (Values.Peek() == index)
        {
            Values.Dequeue();
            return true;
        }

        return false;
    }

    public int ConvertPlayerIndex(int playerIndex)
    {
        return playerIndex;
    }



}
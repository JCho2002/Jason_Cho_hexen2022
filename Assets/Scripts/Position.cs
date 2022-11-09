using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    private readonly int _q;
    private readonly int _r;
    private readonly int _s;

    public int Q => _q;
    public int R => _r;

    public Position(int q, int r)
    {
        _q = q;
        _r = r;
        _s = -_q - _r;
    }

    public new string ToString()
    {
        return $"Grid Position: (Q: {_q}, R: {_r}, S: {_s})";
    }

}

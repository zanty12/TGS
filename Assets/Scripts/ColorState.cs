using System;
using UnityEngine;

public enum COLORSTATE
{
    Red,
    Green,
    Blue,
    Yellow,
    Magenta,
    Cyan,
    White,
}

public class ColorState : MonoBehaviour
{

    public static COLORSTATE MergeColors(COLORSTATE color1, COLORSTATE color2)
    {
        if (color1 == color2)
        {
            return color1;
        }

        if (color1 == COLORSTATE.Red && color2 == COLORSTATE.Blue ||
            color1 == COLORSTATE.Blue && color2 == COLORSTATE.Red)
        {
            return COLORSTATE.Magenta;
        }

        if (color1 == COLORSTATE.Red && color2 == COLORSTATE.Green ||
            color1 == COLORSTATE.Green && color2 == COLORSTATE.Red)
        {
            return COLORSTATE.Yellow;
        }

        if (color1 == COLORSTATE.Blue && color2 == COLORSTATE.Green ||
            color1 == COLORSTATE.Green && color2 == COLORSTATE.Blue)
        {
            return COLORSTATE.Cyan;
        }

        return COLORSTATE.White;
    }

    public static Color StateToColor(COLORSTATE colorState)
    {
        return colorState switch
        {
            COLORSTATE.Red => UnityEngine.Color.red,
            COLORSTATE.Green => UnityEngine.Color.green,
            COLORSTATE.Blue => UnityEngine.Color.blue,
            COLORSTATE.Yellow => UnityEngine.Color.yellow,
            COLORSTATE.Magenta => UnityEngine.Color.magenta,
            COLORSTATE.Cyan => UnityEngine.Color.cyan,
            COLORSTATE.White => UnityEngine.Color.white,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

using UnityEngine;
using System.Collections;

public class MenuColours : MonoBehaviour {

    public enum ColourType { LightGreen, DarkGreen, Tan };

    public static Color GetColour(ColourType colour)
    {
        switch (colour)
        {
            case ColourType.DarkGreen:
                return new Color(0.25f, 0.4f, 0.17f, 0.9019f);
            case ColourType.LightGreen:
                return new Color(0.5f, 0.804f, 0.345f, 0.9019f);
            case ColourType.Tan:
                break;
            default:
                break;
        }

        return new Color(0.5f, 0.804f, 0.345f, 0.9019f);
    }
}

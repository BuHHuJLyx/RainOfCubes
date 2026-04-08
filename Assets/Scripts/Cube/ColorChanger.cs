using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color SetRandomColor()
    {
        return Random.ColorHSV();
    }

    public Color SetDefaultColor()
    {
        return Color.white;
    }
}
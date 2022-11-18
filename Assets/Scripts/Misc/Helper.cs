using UnityEngine;

public static class Helper
{
    public static float LinearToDecibels(float linear)
    {
        float linearScaleRange = 20f;

        // ������� �����������
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }
}

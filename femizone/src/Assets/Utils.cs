using UnityEngine;
using System.Collections;

public static class Utils {

    public static void RemoveAllChildsIn(Transform container)
    {
        int num = container.transform.childCount;
        for (int i = 0; i < num; i++) UnityEngine.Object.DestroyImmediate(container.transform.GetChild(0).gameObject);
    }
    public static string FormatNumbers(int num)
	{
		return string.Format ("{0:#,#}",  num);
	}
}

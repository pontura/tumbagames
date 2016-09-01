using UnityEngine;
using System.Collections;

public static class Utils {

    public static void RemoveAllChildsIn(Transform container)
    {
        int num = container.transform.childCount;
        for (int i = 0; i < num; i++) UnityEngine.Object.DestroyImmediate(container.transform.GetChild(0).gameObject);
    }
    public static string IntToMoney(int num)
    {
        string numToString = num.ToString();
        string num2;
        string num1;
        if (num < 100)
        {
            num1 = "0";
            num2 = num.ToString();
        }
        else
        {
            num1 = (num / 100).ToString();
            num2 = numToString.Substring(numToString.Length - 2);
        }
        return "$" + num1 + "," + num2;
    }
}

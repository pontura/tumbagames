using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuletaItem : MonoBehaviour {

    public Image image;
    public Color color;
    
    public void Init(Ruleta.Item item,  int height)
    {
        if (item.ropa != "" && item.ropa != null)
        {
            print("ropa" + item.ropa);
            image.sprite = Resources.Load("ropa_thumbs/" + item.ropa, typeof(Sprite)) as Sprite;
        }
        else if (item.zone > 0)
        {
            image.sprite = Resources.Load("zones/" + item.zone, typeof(Sprite)) as Sprite;
        }
        else if (item.item > 0)
        {
            image.sprite = Resources.Load("helmets/" + item.item, typeof(Sprite)) as Sprite;
        }
        GetComponent<Image>().color = Color.white;  //item.color;
        GetComponent<LayoutElement>().minHeight = height;
    }
}

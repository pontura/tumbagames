using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Ruleta : MonoBehaviour {

    public float InitialSpeed;
    public float acceleration;
    public int selectedID;

    public GameObject container;
    public RuletaItem ruletaItem;
    public List<Item> items;
    public int itemsHeight;
    public Color[] colors;

    [Serializable]
    public class Item
    {
        public Color color;
        public int item;
        public int zone;
        public string text;
    }
    public states state;
    public enum states
    {
        IDLE,
        ROLLING,
        REPOSITION,
        FINISH
    }
    public float speed;
    public int totalHeight;
    public float offsetY;
    public float repositionTo;

	public void Init () {
        
        offsetY = container.transform.localPosition.y;

        items.Clear();

        int id = 0;
        foreach (TextsRuleta.Data data in Data.Instance.texts.ruleta.data)
        {
            Item item = new Item();
            item.item = data.item;
            item.zone = data.zone;
            item.text = data.text;
            item.color = colors[id];
            items.Add(item);
            id++;
        }
        foreach (Item item in items)
            Add(item);

        Add(items[0]);

        totalHeight = items.Count * itemsHeight;
	}
    void Add(Item item)
    {
        RuletaItem newItem = Instantiate(ruletaItem);
        newItem.transform.SetParent(container.transform);
        newItem.transform.transform.localScale = Vector2.one;
        newItem.Init(item, itemsHeight);
    }
    public void RuletaOn()
    {
        InitialSpeed = UnityEngine.Random.Range(15, 50);
        speed = InitialSpeed;
        state = states.ROLLING;
    }
    void Update()
    {
        if (state == states.ROLLING) Rolling();
        else if (state == states.REPOSITION) Repositionate();
    }
   
    void Rolling()
    {
        speed -= Time.deltaTime + acceleration;
        float newY = container.transform.localPosition.y + speed;

        if (speed <= 0)
        {
            CalculateItem();
            state = states.REPOSITION;            
        }

        if (container.transform.localPosition.y > (totalHeight + offsetY))
            ResetPosition();
        else
            container.transform.localPosition = new Vector3(0, newY, 0);
    }
    private void CalculateItem()
    {
        selectedID = (int)Mathf.Round((container.transform.localPosition.y - offsetY) / itemsHeight);
        repositionTo = (itemsHeight * selectedID) + offsetY;
        if (selectedID >= items.Count) selectedID = 0;
        Debug.Log("item id: " + selectedID);
    }
    void Repositionate()
    {
        if (container.transform.localPosition.y > repositionTo)
            RepositionateUp();
        else
            RepositionateDown();
    }
    void RepositionateUp()
    {
        speed += Time.deltaTime + acceleration;
        float newY = container.transform.localPosition.y - speed;
        container.transform.localPosition = new Vector3(0, newY, 0);

        if (container.transform.localPosition.y <= repositionTo) Ready();

    }
    void RepositionateDown()
    {
        speed += Time.deltaTime + acceleration;
        float newY = container.transform.localPosition.y + speed;
        container.transform.localPosition = new Vector3(0, newY, 0);

        if (container.transform.localPosition.y >= repositionTo) Ready();

    }
    void Ready()
    {
        container.transform.localPosition = new Vector3(0, repositionTo, 0);
        state = states.FINISH;
        GetComponent<RuletaPopup>().Open(items[selectedID]);
    }
    void ResetPosition()
    {
        container.transform.localPosition = new Vector3(0, offsetY, 0);
    }
}

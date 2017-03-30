using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementListsPopup : MonoBehaviour {

	public GameObject popup;
	public Image medal;
	public AchievementListButton button;
	private List<Achievement> list;
	public Transform container;
	public Text field;

	void Start()
	{
		Events.OnShowAchievementList += OnShowAchievementList;
		popup.SetActive (false);
	}
	void OnDestroy()
	{
		Events.OnShowAchievementList -= OnShowAchievementList;
	}
	void OnScreenShotReady()
	{
		Invoke("Close", 0.5f);
	}
	void OnShowAchievementList(List<Achievement> list)
	{			
		Utils.RemoveAllChildsIn (container);
		this.list = list;	
		popup.SetActive (true);
		int ready = 0;
		string medalName = "";

		if (list [0].listID == "FUTBOL")
			medalName = "achivements_FUTBOL_GENERICO";
		else
			medalName = list[0].image;
		
		foreach (Achievement ach in list) {
			AchievementListButton b = Instantiate (button);
			b.Init (this, ach);
			b.transform.SetParent (container);
			b.transform.localPosition = Vector3.zero;
			b.transform.localScale = Vector3.one;
			b.GetComponent<Button>().onClick.AddListener(() => { Open(ach); });
			if (ach.ready)
				ready++;
		}
		field.text = "Lograte " + ready + " de " + list.Count;
		medal.sprite = Resources.Load("achievements/" + medalName, typeof(Sprite)) as Sprite;
	}
	public void Open(Achievement ach)
	{
		Events.OnShowAchievementSignal (ach.image, ach.title, ach.ready);
	}
	public void Close()
	{
		popup.SetActive (false);
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{

    public bool isArcade;

    const string PREFAB_PATH = "Data";
    
    static Data mInstance = null;
	public Settings settings;
    public ArcadeRanking arcadeRanking;
    public LevelsManager levelsManager;
    public LoadingAsset loadingAsset;
    public TextsManager textsManager;
    public SequenceData sequenceData;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<Data>();
                }
            }
            return mInstance;
        }
    }
    public string currentLevel;
    public void LoadScene(string aLevelName)
    {       
        this.currentLevel = aLevelName;
        Time.timeScale = 1;
        Events.OnMusicVolumeChanged(0f);
        loadingAsset.SetOn(OnLoaded);
    }
    void OnLoaded()
    {
        Events.OnChangeScene(currentLevel);
        SceneManager.LoadScene(currentLevel);
        loadingAsset.SetOff();
        Events.OnMusicVolumeChanged(0.6f);
    }
    void Awake()
    {
		QualitySettings.vSyncCount = 1;
        Screen.fullScreen = true;
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        if(isArcade)
            Cursor.visible = false;

        DontDestroyOnLoad(this.gameObject);

       
        GetComponent<MusicManager>().Init();
		settings = GetComponent<Settings> ();
        arcadeRanking = GetComponent<ArcadeRanking>();
        levelsManager = GetComponent<LevelsManager>();
        textsManager = GetComponent<TextsManager>();
        sequenceData = GetComponent<SequenceData>();
    }
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Alpha1))
       // {
       //     Application.Quit();
       // }
    }
}

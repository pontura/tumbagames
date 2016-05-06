using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    public GameData gameData;

    //malisimo
    public bool MainMenuPopupOn;

    const string PREFAB_PATH = "Data";
    
    static Data mInstance = null;
    [HideInInspector]
    public ClothesSettings clothesSettings;
    [HideInInspector]
    public EnemiesManager enemiesManager;
    [HideInInspector]
    public GameSettings gameSettings;
    [HideInInspector]
    public AreasManager areasManager;
    [HideInInspector]
    public MusicManager musicManager;
    [HideInInspector]
    public SoundManager soundManager;
    [HideInInspector]
    public CombosManager combosManager;
    [HideInInspector]
    public SpecialItemsManager specialItems;
    [HideInInspector]
    public MoodsManager moodsManager;
    [HideInInspector]
    public Texts texts;

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
    public void LoadLevel(string aLevelName)
    {
        Time.timeScale = 1;
        Application.LoadLevel(aLevelName);
    }
    void Awake()
    {
		QualitySettings.vSyncCount = 1;

        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        clothesSettings = GetComponent<ClothesSettings>();
        enemiesManager = GetComponent<EnemiesManager>();
        gameData = GetComponent<GameData>();
        gameSettings = GetComponent<GameSettings>();
        areasManager = GetComponent<AreasManager>();
        musicManager = GetComponent<MusicManager>();
        soundManager = GetComponent<SoundManager>();
        combosManager = GetComponent<CombosManager>();
        specialItems = GetComponent<SpecialItemsManager>();
        moodsManager = GetComponent<MoodsManager>();
        texts = GetComponent<Texts>();

        GetComponent<MusicManager>().Init();
        GetComponent<SoundManager>().Init();
        moodsManager.Init();

//#if UNITY_ANDROID || UNITY_IPHONE
       // Handheld.PlayFullScreenMovie(movPath, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
//#endif

    }
}

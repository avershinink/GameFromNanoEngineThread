using UnityEngine;
using UnityEngine.UI;

public class GameWorker : MonoBehaviour {

    public static GameWorker GM;
    public static int score;
    public Text BaseInfo;
    public Text ScoreInfo;
    public Text ResultScoreInfo;
    public Text TimeInfo;
    public Base Base;
    public Enemy RedEnemy;
    public Enemy GreenEnemy;

    // Enemy spawning
    public float[]      SpawnDelayStages;
    public float     RedSpawnDelay;
    public float[]   RedSpawnDelayByStage;
    public float   GreenSpawnDelay;
    public float[] GreenSpawnDelayByStage;
    public Transform RedEnemySpawn1;
    public Transform RedEnemySpawn2;
    public Transform GreenEnemySpawn1;

    // enemy paths
    public Transform[] MovingPath1;
    public Transform[] MovingPath2;
    public Transform[] MovingPath3;
    public Transform[] MovingPath4;
    public Transform[] MovingPath5;
    public Transform[] MovingPath6;

    private int SpawnStageIndex;
    private static System.Random rnd;
    private GameObject[] pm;
    private GameObject[] gom;
    private float RedSpawnTimer;
    private float GreenSpawnTimer;
    private bool GameOver;
    private float GameOverDelay;

    void Start () {
        if (GM == null)
            GM = this;
        Time.timeScale  = 1;

        rnd = new System.Random();

        // UI
        pm = GameObject.FindGameObjectsWithTag("PauseMenu");
        gom = GameObject.FindGameObjectsWithTag("GameOverMenu");
        foreach (GameObject item in pm)
            item.SetActive(false);
        foreach (GameObject item in gom)
            item.SetActive(false);
    }

    void Update () {
        // pause/resume
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject item in pm)
                item.SetActive(Time.timeScale == 1);
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }

        if (GameOver)
        {
            if (GameOverDelay == 0)
            {
                GetComponent<AudioSource>().Play();
                foreach (GameObject item in gom)
                    item.SetActive(true);
                ResultScoreInfo.text = string.Format("Score: {0:D3}", score);
                GameOverDelay = GetComponent<AudioSource>().clip.length;
            }

            if (GameOverDelay <= 0)
                Time.timeScale = 0;
            else
                GameOverDelay -= Time.timeScale;
        }
        else
        {
            if (RedSpawnTimer >= RedSpawnDelay)
            {
                Enemy RP1 = Instantiate<Enemy>(RedEnemy, RedEnemySpawn1.position, Quaternion.identity);
                RP1.MovingPath = rnd.Next(0, 1) == 0 ? MovingPath1 : MovingPath2;
                Enemy RP2 = Instantiate<Enemy>(RedEnemy, RedEnemySpawn2.position, Quaternion.identity);
                RP2.MovingPath = rnd.Next(0, 1) == 0 ? MovingPath3 : MovingPath4;
                RedSpawnTimer = 0;
            }
            if (GreenSpawnTimer >= GreenSpawnDelay)
            {
                Enemy GP = Instantiate<Enemy>(GreenEnemy, GreenEnemySpawn1.position, Quaternion.identity);
                GP.MovingPath = rnd.Next(0, 1) == 0 ? MovingPath5 : MovingPath6;
                GreenSpawnTimer = 0;
            }

            RedSpawnTimer += Time.deltaTime;
            GreenSpawnTimer += Time.deltaTime;

            // Spawning frequency over time
            if (SpawnDelayStages.Length <= SpawnStageIndex
                && Time.timeSinceLevelLoad > SpawnDelayStages[SpawnStageIndex]
                && RedSpawnDelay > RedSpawnDelayByStage[SpawnStageIndex])
            {
                RedSpawnDelay   =   RedSpawnDelayByStage[SpawnStageIndex];
                GreenSpawnDelay = GreenSpawnDelayByStage[SpawnStageIndex];
                SpawnStageIndex++;
            }

            BaseInfo.text  = string.Format("Base: {0}/{1}", Base.HP, Base.MaxHP);
            ScoreInfo.text = string.Format("Score: {0:D3}", score);
            TimeInfo.text  = string.Format("{0}:{1}", Mathf.Floor(Time.timeSinceLevelLoad / 60).ToString("00"), (Time.timeSinceLevelLoad % 60).ToString("00"));
            
            if (Base.HP == 0)
                GameOver = true;
        }
    }
}

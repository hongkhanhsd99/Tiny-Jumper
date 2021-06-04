using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player playerPrefabs;
    public Plattform plattformPrefabs;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;

    public float powerBarUp;
    Player m_player;
    int score;
    public CamController mainCam;

    bool isGameStarted;

    public bool IsGameStarted { get => isGameStarted;  }

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public override void Start()
    {
        base.Start();

        GameGUIManager.Ins.UpdateScoreCounting(score);
        GameGUIManager.Ins.UpdatePowerBar(0,1);
        AudioController.Ins.PlayBackgroundMusic();
    }

    public void PlayGame()
    {
        StartCoroutine(PlatformInit());

        GameGUIManager.Ins.SHowGameGUI(true);
    }
    IEnumerator PlatformInit()
    {
        Plattform platformClone = null;
        if (plattformPrefabs)
        {
            platformClone = Instantiate(plattformPrefabs, new Vector2(0, Random.Range(minSpawnY, maxSpawnY)),Quaternion.identity);
            platformClone.id = platformClone.gameObject.GetInstanceID();

            yield return new WaitForSeconds(0.5f);
        }

        if (playerPrefabs)
        {
            m_player = Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity);
            m_player.lastPlattformId = platformClone.id;
        }
        if (plattformPrefabs)
        {
            float spawnX = m_player.transform.position.x + minSpawnX;

            float spawnY = Random.Range(minSpawnY, maxSpawnY);

            Plattform platformClone2 = Instantiate(plattformPrefabs, new Vector2(spawnX, spawnY), Quaternion.identity);
            platformClone2.id = platformClone2.gameObject.GetInstanceID();
        }
        yield return new WaitForSeconds(0.5f);
        isGameStarted = true;
    }

    public void CreatePlatform()
    {
        if (!plattformPrefabs || !m_player)
            return;
        float spawnX = Random.Range(m_player.transform.position.x + minSpawnX, m_player.transform.position.x + minSpawnX);

        float spawnY = Random.Range(minSpawnY, maxSpawnY);

        Plattform platformClone = Instantiate(plattformPrefabs, new Vector2(spawnX, spawnY), Quaternion.identity);
        platformClone.id= platformClone.gameObject.GetInstanceID();
    }

    public void CreatePlatformAndLerp(float playerXPos)
    {
        if (mainCam)
        {
            mainCam.LerpTrigger(playerXPos+minSpawnX);
        }
        CreatePlatform();
    }
    public void AddScore()
    {
        score++;
        Prefs.BestScore = score;
        GameGUIManager.Ins.UpdateScoreCounting(score);
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);
    }
  
}

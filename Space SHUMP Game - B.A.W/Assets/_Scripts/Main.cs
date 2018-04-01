using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[]
    {
        WeaponType.phaser, WeaponType.laser, WeaponType.laser, WeaponType.laser
    };
    private BoundsCheck bndCheck;

    public void ShipDestroyed(Enemy e)
    {
        if(Random.value <= e.powerUpDropChance)
        {
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];

            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();

            pu.SetType(puType);

            pu.transform.position = e.transform.position;
        }
    }

    private void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        //generic dictionary with WeaponType as the key...
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(bndCheck.cameraWidth * 2, bndCheck.cameraHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }


    public void SpawnEnemy()
    {
        //print(spawnCount);
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        float enemyPadding = enemyDefaultPadding;
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.cameraWidth + enemyPadding;
        float xMax = bndCheck.cameraWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.cameraHeight + enemyPadding;
        pos.z = 10;
        go.transform.position = pos;
        go.GetComponent<Enemy>().health = Random.Range(2, 10);
        go.GetComponent<Enemy>().score = 10 * (int) go.GetComponent<Enemy>().health;
<<<<<<< HEAD
        if(go.GetComponent<Enemy>().health > 7 && Level.getWaves()!=5)
=======
        if(go.GetComponent<Enemy>().health > 7)
>>>>>>> fc6e4a0ae7a7f834dadd3a02fb2b325cdd7a6ec5
        {
            go.GetComponent<Enemy>().powerUpDropChance = 0.7f;
        }
        else
        {
            go.GetComponent<Enemy>().powerUpDropChance = 0f;
        }

            Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
        if (Level.getDeathCount() >= Level.getWaves()) //level up if player killed enough enemies
        {
            levelUp();
        }

    }

    public void levelUp()
    {
        Level.SetLevelOver(true); //sets flag
        enemySpawnPerSecond *= 1.5f; //increase freq of enemies spawned every level
    }

	public void DelayedRestart (float delay){
		Invoke ("Restart", delay);
	}

	public void Restart(){
		SceneManager.LoadScene ("_Scene_0");
	}

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        //check if key exists
        //throw error if key doesnt exist
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }
        //returns new wt with no selected weapon
        return (new WeaponDefinition());
    }

}

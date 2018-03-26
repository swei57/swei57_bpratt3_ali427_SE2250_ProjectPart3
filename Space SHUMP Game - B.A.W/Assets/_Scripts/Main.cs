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
    private BoundsCheck bndCheck;

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

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
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

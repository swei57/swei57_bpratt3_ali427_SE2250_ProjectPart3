using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [Header("Set in Inspector")]
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(.25f, 2);
    public float lifeTime = 6f;
    public float fadeTime = 4f;

    [Header("Set Dynamically")]
    public WeaponType type;
    public GameObject cube;
    public TextMesh letter;
    public Vector3 rotPerSecond;
    public float birthTime;

    private Rigidbody _rigid;
    private BoundsCheck _bndCheck;
    private Renderer _cubeRend;

    private void Awake()
    {
        cube = transform.Find("Cube").gameObject;

        letter = GetComponent<TextMesh>();
        _rigid = GetComponent<Rigidbody>();
        _bndCheck = GetComponent<BoundsCheck>();
        _cubeRend = cube.GetComponent<Renderer>();

        Vector3 vel = Random.onUnitSphere;

        vel.z = 0;
        vel.Normalize();
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        _rigid.velocity = vel;

        transform.rotation = Quaternion.identity;

        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y),
            Random.Range(rotMinMax.x, rotMinMax.y));

        birthTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;

        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }

        if (u > 0)
        {
            Color c = _cubeRend.material.color;
            c.a = 1f - u;
            _cubeRend.material.color = c;

            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;
        }

        if (!_bndCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
	}

    public void SetType(WeaponType wt)
    {
        WeaponDefinition def = Main.GetWeaponDefinition(wt);

        _cubeRend.material.color = def.color;
        if(wt == WeaponType.phaser)
        {
            letter.text = "P";
            _cubeRend.material.color = Color.red;
        }
        else if (wt == WeaponType.laser)
        {
            letter.text = "L";
            _cubeRend.material.color = Color.magenta;
        }else if(wt == WeaponType.shield)
        {
            letter.text = "S";
            _cubeRend.material.color = Color.green;
        }
        else
        {
            letter.text = "O";
        }
        type = wt;
    }

    public void AbsorbedBy(GameObject target)
    {
        Destroy(this.gameObject);
    }
}

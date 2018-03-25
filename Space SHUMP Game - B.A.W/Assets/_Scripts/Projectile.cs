using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Use this for initialization
    private BoundsCheck bndCheck;
    [SerializeField]
    private Renderer rend;

    [Header("Set Dynamically")]
    public Rigidbody rigid;
    [SerializeField]
    private WeaponType _type;
    public WeaponType type //property class
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }
	void Awake () {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
	}

    public void SetType(WeaponType eType)
    {
        //set type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }
}

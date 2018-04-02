using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Use this for initialization
    private BoundsCheck _bndCheck;
    [SerializeField]
    private Renderer _rend;

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
        _bndCheck = GetComponent<BoundsCheck>();
        _rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_bndCheck.offUp)
        {
            Destroy(gameObject);
        }
	}

    public void SetType(WeaponType eType)
    {
        //set type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        _rend.material.color = def.projectileColor;
    }
}

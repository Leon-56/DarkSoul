using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public WeaponManager testWm;

    private static GameManager instance;
    private DataBase weaponDB;
    private WeaponFactory weaponFact;

    void Awake()
    {
        CheckGameObject();
        CheckSingle();
    }

    void Start()
    {
        InitWeaponDB();
        InitWeaponFactory();

        Collider col = weaponFact.CreateWeapon("Sword", "R", testWm);
        testWm.UpdateCollider("R", col);
        testWm.ChangeDualHands(true);
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 150, 30), "R: Sword"))
        {
            testWm.UnloadWeapon("R");
            Collider col = weaponFact.CreateWeapon("Sword", "R", testWm);
            testWm.UpdateCollider("R", col);
            testWm.ChangeDualHands(true);
        }
        if(GUI.Button(new Rect(10, 50, 150, 30), "R: Spikes"))
        {
            testWm.UnloadWeapon("R");
            Collider col = weaponFact.CreateWeapon("Spikes", "R", testWm);
            testWm.UpdateCollider("R", col);
            testWm.ChangeDualHands(false);
        }
    }

    ///
    ///
    ///

    private void InitWeaponFactory()
    {
        weaponFact = new WeaponFactory(weaponDB);
    }

    private void InitWeaponDB()
    {
        weaponDB = new DataBase();
    }

    private void CheckSingle()
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    private void CheckGameObject()
    {
        // 确保GameManager挂载在 "GM" layer上
        if(tag == "GM")
            return ;
        Destroy(this);
    }

    
}

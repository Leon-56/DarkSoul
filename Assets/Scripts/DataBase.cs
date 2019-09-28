using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase
{
    private string weaponDataBaseFileName = "weaponData";
    public readonly JSONObject weaponDataBase;

    public DataBase()
    {
        TextAsset weaponContent = Resources.Load(weaponDataBaseFileName) as TextAsset;
        weaponDataBase = new JSONObject(weaponContent.text);
    }

}

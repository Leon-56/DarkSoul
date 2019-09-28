using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    void Awake() {
        CheckGameObject();
        CheckSingle();
    }

    void Start()
    {
        TextAsset myText = Resources.Load("weapon") as TextAsset;
        //GameObject prefab = (GameObject)Resources.Load("Spikes_01");
    }

    private void CheckSingle() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    private void CheckGameObject() {
        // 确保GameManager挂载在 "GM" layer上
        if(tag == "GM")
            return ;
        Destroy(this);
    }

}

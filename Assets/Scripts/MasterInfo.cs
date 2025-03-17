using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInfo : MonoBehaviour
{
    public static int coinCount = 0;
    [SerializeField] GameObject coinDisplay;

    void Awake()
    {
        // Make this object persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        coinDisplay.GetComponent<TMPro.TMP_Text>().text = "COINS: " + coinCount;
    }
}

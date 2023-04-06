using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnlinePlayerHandler : MonoBehaviour
{
    public MutiplayerManager mainPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = FindObjectOfType<MutiplayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mainPlayer.serverRes);
    }
}

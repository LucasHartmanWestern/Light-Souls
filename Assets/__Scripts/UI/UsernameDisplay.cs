using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameDisplay : MonoBehaviour
{
    public Transform player;
    public Text usernameDisplay;
    public float yOffset = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        usernameDisplay.text = player.name;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position + Vector3.up * yOffset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(playerPos);

        if (IsFacingCamera(playerPos))
        {
            usernameDisplay.enabled = true;
            usernameDisplay.transform.position = screenPos;
        }
        else
        {
            usernameDisplay.enabled = false;
        }
    }

    private bool IsFacingCamera(Vector3 worldPosition)
    {
        Vector3 toPlayer = (worldPosition - Camera.main.transform.position).normalized;
        float dotProduct = Vector3.Dot(toPlayer, Camera.main.transform.forward);

        // Check if the angle between the camera forward vector and the vector to the player is less than 90 degrees
        return dotProduct > 0;
    }
}

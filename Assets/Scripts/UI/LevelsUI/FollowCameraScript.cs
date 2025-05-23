using UnityEngine;
using Cinemachine;
using System.Collections;

public class FollowCameraScript : MonoBehaviour {
    public CinemachineVirtualCamera virtualCamera;

    IEnumerator Start() 
    {
        yield return new WaitForSeconds(0.05f); 

        GameObject player = GameObject.FindGameObjectWithTag("MainCharacter");
        if (player != null && virtualCamera != null) 
        {

            Camera.main.transform.position = new Vector3(
                player.transform.position.x,
                player.transform.position.y,
                Camera.main.transform.position.z
            );

            virtualCamera.Follow = player.transform;

            Debug.Log("Camera snapped to player and follow set.");
        } else 
        {
            Debug.Log("Player not found after delay.");
        }
    }
}

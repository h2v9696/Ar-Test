using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerPhoton : MonoBehaviour {
    public string roomName = "AR";
    public string playerDuckName = "duck";
    public GameObject spawnPoint;
    public GameObject regionCapture;
    public Transform imageTarget;
	void Start () {
        PhotonNetwork.ConnectUsingSettings ("0.1");

	}

    void OnJoinedLobby() {
        RoomOptions roomOptions = new RoomOptions () { IsVisible = false, MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom() {
        GameObject duck = PhotonNetwork.Instantiate (playerDuckName,
                              spawnPoint.transform.position,
                              Quaternion.identity, 0);
//        duck.transform.SetParent (imageTarget);

        duck.GetComponent<GetTexture> ().Region_Capture = regionCapture;

        
    }
}

using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;

public class GameLogic2 : MonoBehaviour
{
    public GameObject regionCapture;
    private void Start()
    {
        var go = NetworkManager.Instance.InstantiateDuck ();
        go.gameObject.GetComponent<GetTexture> ().Region_Capture = regionCapture;
    }
}
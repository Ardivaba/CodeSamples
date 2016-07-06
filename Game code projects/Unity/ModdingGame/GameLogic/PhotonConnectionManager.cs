using UnityEngine;
using System.Collections;

/*
    Connects to and manages Photon.
*/
[RequireComponent(typeof(GameMode))]
public class PhotonConnectionManager : PhotonSingleton<PhotonConnectionManager>
{
    #region pun hooks
    // Connects to master server
    protected void ConnectToMasterServer()
    {
        PhotonNetwork.ConnectToRegion(CloudRegionCode.eu, "0.1");
    }

    // Joins lobby after connection to photon is established
    public override void OnConnectedToPhoton()
    {
        PhotonNetwork.JoinLobby();
    }

    // Creates or joins a room called MainRoom after we've joined into lobby
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("MainRoom", null, null);
    }
    #endregion

    #region unity hooks

    // Connects to master server when game starts
    void Start()
    {
        ConnectToMasterServer();
    }

    // Displays current photon connection state
    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 200), PhotonNetwork.connectionState.ToString());
    }
    #endregion
}

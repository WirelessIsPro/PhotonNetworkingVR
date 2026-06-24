using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
// I didn't know what using's I need for this so I did most I know

namespace Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        // using these Transforms so in the Rig Class I can Have Transforms that follow scene ones
        public NetworkManager instance;
        public GameObject LeftHand;
        public GameObject RightHand;
        public GameObject Head;
        public GameObject Body; // maybe if I make the body not move w head
        public string NetworkPlayerPath;
        // I don't even know what  callbacks to do or anything
        void Start()
        {
            instance = this;
            // just connect on start I really don't know
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("connecting");
        }

        public override void OnConnectedToMaster()
        {
            // is this vaild?
            // yes this this vaild
            // i'ma find photon api's for this
            Debug.Log("joining room");
            PhotonNetwork.JoinRandomOrCreateRoom();	 // idk if arguments are needed here, i'm just calling it, they were needed, no they were not.
        }

        public override void OnJoinedRoom()
        {
            // tried doing transform, didn't work so I'll just do a gameobject and get it's transform!
            Debug.Log("spawning prefab, in a room yo");
            PhotonNetwork.Instantiate(NetworkPlayerPath, Head.transform.position, Head.transform.rotation);
        }

    }
}
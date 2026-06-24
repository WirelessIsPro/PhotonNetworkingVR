using UnityEngine;
using Photon.Pun;

namespace Networking
{
    public class NetworkRig : MonoBehaviourPunCallbacks
    {
        public NetworkManager networkManager;
	public PhotonView pv;
        public GameObject Head;
        public GameObject Body;
        public GameObject LeftHand;
        public GameObject RightHand;

        void Start()
        {
        // now seeing I need a pv check
         if (pv.IsMine) 
         {
            // fucking thing
            networkManager = FindAnyObjectByType<NetworkManager>();
            // as soon as this is created do the thing
            Head.transform.position = networkManager.Head.transform.position;
            Head.transform.rotation = networkManager.Head.transform.rotation;
	 }
        }

        void Update()
        {
		// forgot to put this ismine check
	     if (pv.IsMine) 
         {
            Head.transform.position = networkManager.Head.transform.position;
            Body.transform.rotation = networkManager.Head.transform.rotation;
            Body.transform.position = networkManager.Body.transform.position;
            Head.transform.rotation = networkManager.Body.transform.rotation;
            LeftHand.transform.position = networkManager.LeftHand.transform.position;
            LeftHand.transform.rotation = networkManager.LeftHand.transform.rotation;
            RightHand.transform.position = networkManager.RightHand.transform.position;
            RightHand.transform.rotation = networkManager.RightHand.transform.rotation;
        }
		}
    }
}

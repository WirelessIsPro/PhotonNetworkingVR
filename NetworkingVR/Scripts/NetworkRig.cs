using UnityEngine;
using Photon.Pun;
using Photon.Realtime; // I'm adding this for cosmetics stuff ye
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;

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
        public GameObject[] Cosmetics;

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
            foreach (GameObject obj in Cosmetics)
            {
                Hashtable playerCosmetic;

                if (obj.name == "playerCosmetic")
                {
                    obj.SetActive(true);
                }
                else
                    obj.SetActive(false);
            }
        }
        void ChangeCosmetics(string CurrentCosmetic)
        {
            Hashtable playerCosmetic = new Hashtable();
            // this is ment to create or change the hashtable
            playerCosmetic["Cosmetic"] = CurrentCosmetic;
            // this will sync - photon unity docs
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerCosmetic);
        }
    }
}

using UnityEngine;
using Photon.Pun;
using Photon.Realtime; // I'm adding this for cosmetics stuff ye
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;
using JetBrains.Annotations;
using TMPro;

namespace Networking
{
    public class NetworkRig : MonoBehaviourPunCallbacks
    {
        public NetworkRig instance;
        public NetworkManager networkManager;
        public PhotonView pv;
        public GameObject Head;
        public GameObject Body;
        public GameObject LeftHand;
        public GameObject RightHand;
        public TextMeshPro name;

        public GameObject[] HeadCosmetics;
        public GameObject[] FaceCosmetics;
        public GameObject[] BodyCosmetics;
        public GameObject[] RightHandCosmetics;
        public GameObject[] LeftHandCosmetics;
        public string CurrentHead;
        public string CurrentFace;
        public string CurrentBody;
        public string CurrentLeftHand;
        public string CurrentRightHand;

        void Start()
        {
            // now seeing I need a pv check
            if (pv.IsMine)
            {
                instance = this;
                // fucking thing
                networkManager = FindAnyObjectByType<NetworkManager>();
                // as soon as this is created do the thing
                Head.transform.position = networkManager.Head.transform.position;
                Head.transform.rotation = networkManager.Head.transform.rotation;

            }
        }

        public override void OnEnable()
        {
            base.OnEnable();

            if (photonView.Owner.CustomProperties.TryGetValue("HeadCos", out object head))
                CurrentHead = (string)head;

            if (photonView.Owner.CustomProperties.TryGetValue("FaceCos", out object face))
                CurrentFace = (string)face;

            if (photonView.Owner.CustomProperties.TryGetValue("BodyCos", out object body))
                CurrentBody = (string)body;

            if (photonView.Owner.CustomProperties.TryGetValue("LeftHandCos", out object left))
                CurrentLeftHand = (string)left;

            if (photonView.Owner.CustomProperties.TryGetValue("RightHandCos", out object right))
                CurrentRightHand = (string)right;

            RefreshCosmetics();
        }

        void Update()
        {
            if (pv.IsMine)
            {
                Head.transform.position = networkManager.Head.transform.position;
                Body.transform.rotation = networkManager.Body.transform.rotation;
                Body.transform.position = networkManager.Body.transform.position;
                Head.transform.rotation = networkManager.Head.transform.rotation;
                LeftHand.transform.position = networkManager.LeftHand.transform.position;
                LeftHand.transform.rotation = networkManager.LeftHand.transform.rotation;
                RightHand.transform.position = networkManager.RightHand.transform.position;
                RightHand.transform.rotation = networkManager.RightHand.transform.rotation;
            }
            name.text = PhotonNetwork.NickName;
        }


        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (targetPlayer != photonView.Owner)
                return;

            if (changedProps.ContainsKey("HeadCos"))
                CurrentHead = (string)changedProps["HeadCos"];

            if (changedProps.ContainsKey("FaceCos"))
                CurrentFace = (string)changedProps["FaceCos"];

            if (changedProps.ContainsKey("BodyCos"))
                CurrentBody = (string)changedProps["BodyCos"];

            if (changedProps.ContainsKey("LeftHandCos"))
                CurrentLeftHand = (string)changedProps["LeftHandCos"];

            if (changedProps.ContainsKey("RightHandCos"))
                CurrentRightHand = (string)changedProps["RightHandCos"];

            RefreshCosmetics();
        }
        public void ChangeCosmetic(string propertyName, string cosmeticName)
        {
            Hashtable props = new Hashtable();

            props[propertyName] = cosmeticName;

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public void ChangeName(string name)
        {
            name = PhotonNetwork.NickName;
        }

        // it was insaenlyu laggy because there was SO MANY shit going at once, I made it just smth to call
        void RefreshCosmetics()
        {
            SetCosmetic(HeadCosmetics, CurrentHead);
            SetCosmetic(FaceCosmetics, CurrentFace);
            SetCosmetic(BodyCosmetics, CurrentBody);
            SetCosmetic(LeftHandCosmetics, CurrentLeftHand);
            SetCosmetic(RightHandCosmetics, CurrentRightHand);
        }

        void SetCosmetic(GameObject[] cosmetics, string cosmeticName)
        {
            foreach (GameObject obj in cosmetics)
            {
                obj.SetActive(obj.name == cosmeticName);
            }
        }
    }
}

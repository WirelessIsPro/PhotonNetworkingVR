using UnityEngine;
using System;
using Unity.VisualScripting;
using Photon.Pun;

namespace Networking
{
    public class EquipCosmeticExample : MonoBehaviour
    {
        public GameObject TheRig;
        public NetworkRig therigscript; // save me
        public NetworkManager AssignMeIntheScene;
        public enum CosmeticSlot
        {
            Head,
            Face,
            Body,
            LeftHand,
            RightHand
        }

        public CosmeticSlot Slot;
        public string Cosmetic;

        void Update() 
        {
            if (PhotonNetwork.InRoom)
            {
                // I did this in NetworkManager, reused it
                foreach (PhotonView view in FindObjectsOfType<PhotonView>())
                {
                    if (view.IsMine && view.gameObject.name.Contains("NetworkPlayer"))
                    {
                        TheRig = view.gameObject;
                        break;
                    }
                }
                // ahh I didn't assign this shit to rig script
                therigscript = TheRig.GetComponent<NetworkRig>();
            }
        }
        void OnTriggerEnter(Collider other)
        {
            string property = "";

            switch (Slot)
            {
                case CosmeticSlot.Head:
                    property = "HeadCos";
                    break;

                case CosmeticSlot.Face:
                    property = "FaceCos";
                    break;

                case CosmeticSlot.Body:
                    property = "BodyCos";
                    break;

                case CosmeticSlot.LeftHand:
                    property = "LeftHandCos";
                    break;

                case CosmeticSlot.RightHand:
                    property = "RightHandCos";
                    break;
            }

            therigscript.ChangeCosmetic(property, Cosmetic);
        }
    }
}

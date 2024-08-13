using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.UI
{
    public class CharacterUI : MonoBehaviour
    {
        public GameObject typeActionImage;
        public Image typeActionImageColor;
        public GameObject delayImage;

        public Image image;

        public Color colorPreparation;
        public Color colorTimeAttack;
        public Color colorChangingWeapon;

        internal void ActiveGameObjectImage(bool state)
        {
            typeActionImage.SetActive(state);
            delayImage.SetActive(state);
        }
    }
}
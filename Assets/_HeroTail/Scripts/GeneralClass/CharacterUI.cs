using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kosilek.UI
{
    public class CharacterUI : MonoBehaviour
    {
        public GameObject typeActionImage;
        public GameObject delayImage;

        public Image image;

        internal void ActiveGameObjectImage(bool state)
        {
            typeActionImage.SetActive(state);
            delayImage.SetActive(state);
        }
    }
}
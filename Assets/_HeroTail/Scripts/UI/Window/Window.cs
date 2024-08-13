using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosilek.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Window : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private Coroutine coroutine;
        private bool isActive = false;

        private CanvasGroup canvasGroup;

        protected virtual void Awake()
        {
            SetValues();
        }

        private void SetValues()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        internal void Open()
        {
            if (isActive)
                return;

            isActive = true;

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(IE());

            IEnumerator IE()
            {
                for (float i = 0; i < 1f; i += Time.deltaTime * moveSpeed)
                {
                    canvasGroup.alpha = i;
                    yield return null;
                }

                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
                isActive = false;
                coroutine = null;
            }
        }

        internal void Close()
        {
            if (isActive)
                return;

            isActive = true;

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(IE());

            IEnumerator IE()
            {
                for (float i = 1f; i >= 0; i -= Time.deltaTime * moveSpeed)
                {
                    canvasGroup.alpha = i;
                    yield return null;
                }

                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
                isActive = false;
                coroutine = null;
            }
        }
    }
}
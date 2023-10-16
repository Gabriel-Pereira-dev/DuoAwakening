using System;
using System.Collections;
using UnityEngine;

namespace Hole
{
    public class HoleScript : MonoBehaviour
    {
        public float fallDuration;
        public AudioSource thisAudioSource;

        private void Awake()
        {
            thisAudioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherGameobject = other.gameObject;
            var otherLayer = otherGameobject.layer;
            var collidedWithPlayer = LayerMask.NameToLayer("Player") == otherLayer;

            if (collidedWithPlayer)
            {
                StartCoroutine(StartFall());
            }
        }

        private IEnumerator StartFall()
        {
            thisAudioSource.PlayOneShot(thisAudioSource.clip);
            yield return new WaitForSeconds(fallDuration);
            var gameManager = GameManager.Instance;
            var playerLife = gameManager.player.GetComponent<LifeScript>();

            if (playerLife != null)
            {
                playerLife.InflictDamage(gameObject,1);
                if (!playerLife.IsDead())
                {
                    gameManager.LoadCheckpoint();
                }
            }
        }
    }
}
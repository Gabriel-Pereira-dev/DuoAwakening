using System.Collections;
using System.Collections.Generic;
using EventArgs;
using Item;
using UnityEngine;

namespace Door
{
    public class Doorcript : MonoBehaviour
    {
        public Interaction interaction;
        public Item.Item requiredKey;
        private bool isOpen;
        private Animator thisAnimator;
        // Start is called before the first frame update
        void Awake()
        {
            thisAnimator = this.GetComponent<Animator>();

        }

        void Start()
        {
            interaction.OnInteraction += OnInteraction;
            interaction.interactionWidgetComponent.setActionText("Open Door");
        }

        // Update is called once per frame
        void Update()
        {
            if (!isOpen)
            {
                //Check key
                var hasKey = false;

                if (requiredKey == null)
                {
                    hasKey = true;
                }
                else if (requiredKey.itemType == ItemType.Key)
                {
                    hasKey = GameManager.Instance.keys > 0;
                }
                else if (requiredKey.itemType == ItemType.BossKey)
                {
                    hasKey = GameManager.Instance.hasBossKey;
                }

                // Toggle availability
                interaction.SetAvailable(hasKey);
            }
        }

        private void OnInteraction(object sender, InteractionEventArgs e)
        {
            
            // Checa se tem a chave
            var hasRequiredKey = true;
            if (!isOpen)
            {
                if (hasRequiredKey)
                {
                    OpenDoor();
                }

            }
            else
            {
                CloseDoor();
            }
        }

        private void OpenDoor()
        {
            isOpen = true;
            
            // Take Key
            if (requiredKey != null)
            {

                if (requiredKey.itemType == ItemType.Key)
                {
                    GameManager.Instance.keys--;
                }
                else if (requiredKey.itemType == ItemType.BossKey)
                {
                    // Open Boss door
                    GameManager.Instance.hasBossKey = false;
                    GlobalEvents.Instance.InvokeOnBossDoorOpen(this,new BossDoorOpenArgs());
                }
            }
            
            // Disabled Interaction
            interaction.SetAvailable(false);
            
            // Update Animation
            thisAnimator.SetTrigger("tOpen");
            
            // Update UI
            var gameplayUI = GameManager.Instance.gameplayUI;
            gameplayUI.RemoveObject(requiredKey.itemType);
            

        }

        private void CloseDoor()
        {
            isOpen = false;
            thisAnimator.SetTrigger("tClose");

        }
    }
}

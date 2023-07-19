using System.Collections;
using System.Collections.Generic;
using EventArgs;
using Item;
using UnityEngine;

namespace Chest
{
    public class ChestScript : MonoBehaviour
    {
        public Interaction interaction;
        private Animator thisAnimator;
        public GameObject itemHolder;
        public Item.Item item;
        // Start is called before the first frame update
        void Awake()
        {
            thisAnimator = this.GetComponent<Animator>();

        }

        void Start()
        {
            interaction.OnInteraction += OnInteraction;
            interaction.interactionWidgetComponent.setActionText("Open Chest");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnInteraction(object sender, InteractionEventArgs e)
        {
            Debug.Log("Interagiu com o báu");
            // Disable interaction
            interaction.SetAvailable(false);
            // Update animator
            thisAnimator.SetTrigger("tOpen");

            // Create item object
            var itemPrefab = item.itemPrefab;
            var rotation = itemPrefab.transform.rotation;
            var position = itemHolder.transform.position;
            var itemObject = Instantiate(itemPrefab, position, rotation);
            itemObject.transform.SetParent(itemHolder.transform);

            // Update Invetory
            var itemType = item.itemType;
            if (itemType == ItemType.Key)
            {
                GameManager.Instance.keys++;
            }
            else if (itemType == ItemType.BossKey)
            {
                GameManager.Instance.hasBossKey = true;
            }
            else if (itemType == ItemType.Potion)
            {
                var player = GameManager.Instance.player;
                var life = player.GetComponent<LifeScript>();
                life.RestoreHealth();
            }
        }
    }
}

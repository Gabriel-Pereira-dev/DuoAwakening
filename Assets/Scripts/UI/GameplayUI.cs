using System;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace UI
{
    public class GameplayUI : MonoBehaviour
    {
        public HealthBar playerHealthBar;
        public HealthBar bossHealthBar;
        [SerializeField] private GameObject bossContainer;
        private bool isBossVisible = false;
        private List<ObjectEntry> entries = new();

        [Header("Objects")] 
        [SerializeField] private GameObject objectContainer;
        public GameObject normalKeyTemplate;
        public GameObject bossKeyTemplate;

        private void Start()
        {
            bossContainer.SetActive(isBossVisible);
        }

        public void ToggleBossBar(bool enabled)
        {
            isBossVisible = enabled;
            bossContainer.SetActive(enabled);
        }

        public void AddObject(ItemType type)
        {
            if (type != ItemType.Key && type != ItemType.BossKey) return;
            
            // Create widget
            var template = type == ItemType.Key ? normalKeyTemplate : bossKeyTemplate;
            var widget = Instantiate(template, template.transform);
            widget.SetActive(true);
            widget.transform.SetParent(objectContainer.transform);
            
            // Create entry
            var entry = new ObjectEntry()
            {
                type = type,
                widget = widget
            };
            
            entries.Add(entry);
        }

        public void RemoveObject(ItemType type)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                if (entry.type == type)
                {
                    Destroy(entry.widget);
                    entries.RemoveAt(i);
                    return;
                }
            }
        }
        
        private struct ObjectEntry
        {
            public ItemType type;
            public GameObject widget;
        }

        
    }
    
}

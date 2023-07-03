using Unity.VisualScripting;
using UnityEngine;

namespace HUD
{
    public class S_HUD : MonoBehaviour
    {
        public Canvas deathScreen;
    
    
        private void Awake()
        {
            deathScreen.GameObject().SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
    
        }

        // Update is called once per frame
        void Update()
        {
    
        }
    }
}


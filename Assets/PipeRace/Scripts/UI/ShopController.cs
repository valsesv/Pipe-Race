using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [Space(10)] 
        [SerializeField] private GameObject buyButton;

        [Space(10)] 
        [SerializeField] private MeshRenderer pipe;
        [SerializeField] private List<Material> materials;

        private int _currentMaterial;

        private void Start()
        {
            leftButton.onClick.AddListener(SwitchToLeftSkin);
            rightButton.onClick.AddListener(SwitchToRightSkin);
            
            buyButton.SetActive(false);
        }
        
        private void SwitchToRightSkin()
        {
            _currentMaterial++;
            if (_currentMaterial >= materials.Count)
            {
                _currentMaterial = 0;
            }

            SetMaterialToMesh();
        }

        private void SwitchToLeftSkin()
        {
            _currentMaterial--;
            if (_currentMaterial < 0)
            {
                _currentMaterial = materials.Count - 1;
            }

            SetMaterialToMesh();
        }


        private void SetMaterialToMesh()
        {
            pipe.material = materials[_currentMaterial];
        }
    }
}
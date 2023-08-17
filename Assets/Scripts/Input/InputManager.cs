using TorchRun.Player;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace TorchRun.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private float xOffsetCoefficient;
        private PlayerControls controls;
        private bool hasPointerContact;

        private void Awake()
        {
            controls = new PlayerControls();
            EnhancedTouchSupport.Enable();
            controls.Player.PointerContact.started += _ => { hasPointerContact = true; };
            controls.Player.PointerContact.canceled += _ => { hasPointerContact = false; };
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void Update()
        {
            if (hasPointerContact && player.Running)
            {
                var pointerDelta = controls.Player.PointerDelta.ReadValue<Vector2>();
                player.MoveSideways(pointerDelta.x * xOffsetCoefficient);
            }
        }
    }
}
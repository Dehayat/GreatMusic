using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Rosa
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerController mc_controller;

        private bool m_listenForInput = true;
        private int m_moveInput = 0;
        private bool m_jumpInput = false;
        private Vector2 m_aimInput = Vector2.zero;
        private bool m_hookInput = false;
        private bool m_attackInput = false;
        private bool m_pickupInput = false;
        private void Awake()
        {
            mc_controller = GetComponent<PlayerController>();
        }
        private void Update()
        {
            mc_controller.SetMoveDir(m_moveInput);
            mc_controller.SetJump(m_jumpInput);
            mc_controller.SetAttack(m_attackInput);
        }

        public void MoveInput(CallbackContext callbackContext)
        {
            if (!m_listenForInput)
            {
                m_moveInput = 0;
                return;
            }
            float moveInputFloat = callbackContext.ReadValue<float>();
            if (moveInputFloat > float.Epsilon)
            {
                m_moveInput = 1;
            }
            else if (moveInputFloat < -float.Epsilon)
            {
                m_moveInput = -1;
            }
            else
            {
                m_moveInput = 0;
            }
        }
        public void JumpInput(CallbackContext callbackContext)
        {
            if (!m_listenForInput)
            {
                m_jumpInput = false;
                return;
            }
            if (callbackContext.performed)
            {
                m_jumpInput = true;
            }
            if (callbackContext.canceled)
            {
                m_jumpInput = false;
            }
        }
        public void AimInput(CallbackContext callbackContext)
        {
            if (!m_listenForInput)
            {
                m_aimInput = Vector2.right;
                return;
            }
            m_aimInput = callbackContext.ReadValue<Vector2>();
            m_aimInput.Normalize();
        }
        public void FireHookInput(CallbackContext callbackContext)
        {
            if (!m_listenForInput)
            {
                m_hookInput = false;
                return;
            }
            if (callbackContext.performed)
            {
                m_hookInput = true;
            }
            if (callbackContext.canceled)
            {
                m_hookInput = false;
            }
        }
        public void PickupInput(CallbackContext callbackContext)
        {
            if (!m_listenForInput)
            {
                m_pickupInput = false;
                return;
            }
            if (callbackContext.performed)
            {
                m_pickupInput = true;
            }
            else if (callbackContext.canceled)
            {
                m_pickupInput = true;
            }
        }

    }
}

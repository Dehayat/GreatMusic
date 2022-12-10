using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rosa
{
    public class PlayerController : MonoBehaviour
    {
        private enum MoveState
        {
            Idle,
            Run,
            Jump,
            Fall,
        }

        private Rigidbody2D mc_rb;
        private Combat mc_combat;
        private Health mc_health;
        private float m_moveDir = 0f;
        private int m_facingDir = 1;
        private MoveState m_moveState;
        private int m_currentJumpSteps;

        private float m_savedGravity;
        private bool m_isGrounded;

        [SerializeField]
        private float m_runSpeed = 10f;
        [SerializeField]
        private Animator mc_anim;
        [SerializeField]
        private float m_jumpSteps = 10f;
        [SerializeField]
        private float m_jumpSpeed = 10f;
        [SerializeField]
        private LayerMask m_groundLayer;


        private void Awake()
        {
            mc_rb = GetComponent<Rigidbody2D>();
            mc_combat = GetComponent<Combat>();
            mc_health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            mc_combat.HitEvent += OnHit;
            EventSystem.GetInstance().ListenToEvent("UpgradeMoveSpeed", UpgradeMoveSpeedEvent);
        }
        private void OnDisable()
        {
            mc_combat.HitEvent -= OnHit;
            EventSystem.GetInstance().IgnoreEvent("UpgradeMoveSpeed", UpgradeMoveSpeedEvent);
        }

        private Coroutine speedUpgradeCo;
        private void UpgradeMoveSpeedEvent(EventData obj)
        {
            FloatEventData eventData = obj as FloatEventData;
            if (speedUpgradeCo != null)
            {
                m_runSpeed = savedRunSpeed;
                StopCoroutine(speedUpgradeCo);
            }
            speedUpgradeCo = StartCoroutine(SetMoveSpeedForDuration(eventData.value1, eventData.value2));
        }
        private float savedRunSpeed;
        IEnumerator SetMoveSpeedForDuration(float speed, float duration)
        {
            savedRunSpeed = m_runSpeed;
            m_runSpeed = speed;
            yield return new WaitForSeconds(duration);
            m_runSpeed = savedRunSpeed;
        }


        private void OnHit(HitInfo hitInfo)
        {
            //Do particles of something
            mc_health.Damage(hitInfo.attackData.data.damage);
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            switch (m_moveState)
            {
                case MoveState.Idle:
                    IdleState();
                    break;
                case MoveState.Run:
                    RunState();
                    break;
                case MoveState.Jump:
                    JumpState();
                    break;
                case MoveState.Fall:
                    FallState();
                    break;
                default:
                    break;
            }
        }

        private void IdleState()
        {
            mc_rb.velocity = Vector2.zero;
            if (!m_isGrounded)
            {
                StartFalling();
            }
        }
        private void RunState()
        {
            Vector2 vel = mc_rb.velocity;
            vel.x = m_moveDir * m_runSpeed;
            mc_rb.velocity = vel;
            if (!m_isGrounded)
            {
                StopRunning();
                StartFalling();
            }
        }
        private void JumpState()
        {
            m_savedGravity = mc_rb.gravityScale;
            mc_rb.gravityScale = 2;
            if (m_currentJumpSteps < m_jumpSteps)
            {

                Vector2 velocity = mc_rb.velocity;
                velocity.y = m_jumpSpeed;
                mc_rb.velocity = velocity;
            }
            else
            {
                StopJumping();
                StartFalling();
            }
            m_currentJumpSteps++;
        }
        private void FallState()
        {
            m_savedGravity = mc_rb.gravityScale;
            mc_rb.gravityScale = 3;
            if (m_isGrounded)
            {
                StopFalling();
                m_moveState = MoveState.Idle;
            }
            Vector2 vel = mc_rb.velocity;
            vel.x = m_moveDir * m_runSpeed;
            mc_rb.velocity = vel;
        }

        private void StartRunning()
        {
            m_moveState = MoveState.Run;
            mc_anim.SetBool("Running", true);
        }
        private void StopRunning()
        {
            m_moveState = MoveState.Idle;
            mc_anim.SetBool("Running", false);
        }
        private void StartJumping()
        {
            m_currentJumpSteps = 0;
            m_moveState = MoveState.Jump;
            mc_anim.SetBool("Jumping", true);
        }
        private void StopJumping()
        {
            mc_anim.SetBool("Jumping", false);
            mc_rb.gravityScale = m_savedGravity;
            Vector2 velocity = mc_rb.velocity;
            velocity.y = 0f;
            mc_rb.velocity = velocity;
        }
        private void StartFalling()
        {
            m_moveState = MoveState.Fall;
            mc_anim.SetBool("Falling", true);
        }
        private void StopFalling()
        {
            mc_anim.SetBool("Falling", false);
            mc_rb.gravityScale = m_savedGravity;
            Vector2 velocity = mc_rb.velocity;
            velocity.y = 0f;
            mc_rb.velocity = velocity;
        }


        private void FaceLeft()
        {
            m_facingDir = -1;
            Quaternion rot = transform.rotation;
            Vector3 euler = rot.eulerAngles;
            euler.y = 180f;
            transform.rotation = Quaternion.Euler(euler);
        }
        private void FaceRight()
        {
            m_facingDir = 1;
            Quaternion rot = transform.rotation;
            Vector3 euler = rot.eulerAngles;
            euler.y = 0;
            transform.rotation = Quaternion.Euler(euler);
        }

        private ContactPoint2D[] groundedContactResult = new ContactPoint2D[1];
        private RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[1];
        private void CheckGrounded()
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.NoFilter();
            contactFilter.useNormalAngle = true;
            contactFilter.minNormalAngle = 85;
            contactFilter.maxNormalAngle = 95;
            int result = mc_rb.GetContacts(contactFilter, groundedContactResult);
            if (result > 0)
            {
                m_isGrounded = true;
            }
            else
            {
                m_isGrounded = false;
            }
            if (Physics2D.RaycastNonAlloc(transform.position, -transform.up, raycastHit2Ds, 0.2f, m_groundLayer.value) > 0)
            {
                m_isGrounded = true;
            }

        }

        public void SetMoveDir(int moveDir)
        {
            if ((m_moveState == MoveState.Idle || m_moveState == MoveState.Run || m_moveState == MoveState.Fall))
            {
                if (moveDir != 0 && m_moveState == MoveState.Idle)
                {
                    StartRunning();
                }
                else if (moveDir == 0 && m_moveState == MoveState.Run)
                {
                    StopRunning();
                }
                m_moveDir = moveDir;
                if (m_moveDir == 1)
                {
                    FaceRight();
                }
                else if (m_moveDir == -1)
                {
                    FaceLeft();
                }
            }
        }
        public void SetJump(bool jumpInput)
        {
            if ((m_moveState == MoveState.Idle || m_moveState == MoveState.Run))
            {
                if (jumpInput)
                {
                    if (m_moveState == MoveState.Run)
                    {
                        StopRunning();
                    }
                    StartJumping();
                }
            }
        }

        private ContactPoint2D[] platformContactResult = new ContactPoint2D[4];
        public void SetDropDown(bool dropDownInput)
        {
            Collider2D dropCollider = null;
            if (dropDownInput)
            {
                ContactFilter2D contactFilter = new ContactFilter2D();
                contactFilter.NoFilter();
                contactFilter.useNormalAngle = true;
                contactFilter.minNormalAngle = 85;
                contactFilter.maxNormalAngle = 95;
                int result = mc_rb.GetContacts(contactFilter, platformContactResult);
                for (int i = 0; i < result; i++)
                {
                    var effector = platformContactResult[i].collider.GetComponent<PlatformEffector2D>();
                    if (effector != null)
                    {
                        dropCollider = platformContactResult[i].collider;
                    }
                }
            }
            if (dropCollider != null)
            {
                dropCollider.enabled = false;
                StartCoroutine(EnablePlatformDelay(dropCollider));
            }
        }
        IEnumerator EnablePlatformDelay(Collider2D collider)
        {
            yield return new WaitForSeconds(0.5f);
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashIndicator : MonoBehaviour
{
    PlayerController m_Parent;
    SpriteRenderer m_Renderer;

    [SerializeField, Tooltip("The circle")]
    SpriteRenderer m_Circle;

    // Start is called before the first frame update
    void Start()
    {
        m_Parent = GetComponentInParent<PlayerController>();
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Renderer.enabled = m_Circle.enabled = m_Parent.IsAiming();
        if (m_Parent.IsAiming())
        {
            m_Renderer.color = m_Circle.color = Color.Lerp(Color.white, Color.red, m_Parent.GetDashIntensityCharge());

            Vector2 dashDirection = m_Parent.GetDashDirection();

            float angleRad = Mathf.Atan2(dashDirection.y, dashDirection.x);
            transform.rotation = Quaternion.AngleAxis(angleRad * Mathf.Rad2Deg, Vector3.forward);
            Vector3 pos = transform.localPosition;
            pos.x = Mathf.Cos(angleRad) * m_Circle.transform.localScale.x;
            pos.y = Mathf.Sin(angleRad) * m_Circle.transform.localScale.y;
            transform.localPosition = pos;
        }
    }
}

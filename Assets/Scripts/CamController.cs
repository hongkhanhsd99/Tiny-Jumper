using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float lerpTime;
    public float xOffset;
    bool m_canLerp;
    float m_lerpXDistance;
   
    // Update is called once per frame
    void Update()
    {
        if (m_canLerp)
        {
            MoveLerp();
        }
    }

    private void MoveLerp()
    {
        float xPos = transform.position.x;
        xPos = Mathf.Lerp(xPos, m_lerpXDistance, lerpTime * Time.deltaTime);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        if(transform.position.x >= (m_lerpXDistance - xOffset))
        {
            m_canLerp = false;
        }
    }

   public void LerpTrigger(float dist)
    {
        m_canLerp = true;
        m_lerpXDistance = dist;
    }
}

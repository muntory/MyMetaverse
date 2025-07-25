using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    Transform target;

    float offsetX;
    float offsetY;
    bool isMain;

    void Start()
    {
        if (target == null)
            return;

        if (target.GetComponent<Player>() != null)
        {
            isMain = true;
        }
        else
        {
            isMain = false;
        }

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;

        if (isMain)
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x + offsetX;
            pos.y = target.position.y + offsetY;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x + offsetX;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 4f);
        }

        

        
    }
}

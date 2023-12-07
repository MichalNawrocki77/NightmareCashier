
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform transformToFollow;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        transform.position = new Vector3(transformToFollow.position.x,
                                         transformToFollow.position.y,
                                         transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScriptIK : MonoBehaviour
{
    public LegPositions right;
    public LegPositions left;
    public Transform center;
    public float threshold = 2f;
    private float legDistance;

    // Start is called before the first frame update
    void Start()
    {
        legDistance = 0.5f; //Vector2.Distance(right.current.position, left.current.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (right.current.position.x > right.target.position.x + legDistance)
            right.current.DOJump(left.target.position, 0.3f, 1, 0.2f);

        if (left.current.position.x > right.target.position.x + legDistance)
            left.current.DOJump(left.target.position, 0.3f, 1, 0.2f);
    }

    [System.Serializable]
    public struct LegPositions
    {
        public Transform current;
        public Transform target;
    }
}

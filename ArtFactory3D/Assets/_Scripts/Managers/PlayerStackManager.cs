using System;
using System.Collections.Generic;
using System.Linq;
using ArtFactory._Scripts.Units;
using DG.Tweening;
using UnityEngine;

namespace ArtFactory._Scripts.Managers
{
    public class PlayerStackManager : MonoBehaviour
    {
        public List<Transform> balls = new List<Transform>();
        [SerializeField] private Transform paintCubePlace;
        private float _yAxis, _delay;
        void Start()
        {
            balls.Add(paintCubePlace);
        }

        private void Update()
        {
            BallStack(balls);
        }

        private void BallStack(List<Transform> stackObj)
        {
            if (stackObj.Count > 0)
            {
                var len = stackObj.Count;
                for (int i = 1; i < len; i++)
                {
                    var firststacked = stackObj.ElementAt(i - 1);
                    var secondstacked = stackObj.ElementAt(i);

                    var position = secondstacked.position;
                    var position1 = firststacked.position;
                    position = new Vector3(Mathf.Lerp(position.x, position1.x, Time.deltaTime * 30f),
                        Mathf.Lerp(position.y, position1.y + 0.32f, Time.deltaTime * 30f), position1.z);
                    secondstacked.position = position;
                }
            }
            if (Physics.Raycast(transform.position, transform.forward, out var hit, 2f))
            {
                var transform1 = transform;
                Debug.DrawRay(transform1.position, transform1.forward * 1f, Color.green);

                if (hit.collider.CompareTag($"BallCube") && stackObj.Count < 10)
                {
                    if (hit.collider.transform.childCount > 1)
                    {
                        var paint = hit.collider.transform.GetChild(1);
                        stackObj.Add(paint);
                        paint.parent = null;

                        var BallstackObj = hit.collider.transform.parent.GetComponent<BallStack>();

                        if (BallstackObj.CountPaints > 1)
                        {
                            BallstackObj.CountPaints--;
                        }

                        if (BallstackObj.YAxis > 0f)
                        {
                            BallstackObj.YAxis -= BallstackObj.YAxisOffset;
                        }
                    }
                }
                if (hit.collider.CompareTag($"ClawMachine") && stackObj.Count > 1)
                {
                    Debug.Log("clawMachine");
                    var clawPlace = hit.collider.transform;
                    if (clawPlace.childCount > 0)
                    {
                        _yAxis = clawPlace.GetChild(clawPlace.childCount - 1).position.y;
                    }
                    else
                    {
                        _yAxis = clawPlace.position.y;
                    }

                    for (int i = stackObj.Count-1; i >= 1; i--)
                    {
                        var position = clawPlace.position;
                        stackObj[i].DOJump(new Vector3(position.x, _yAxis, position.z), 2f, 1, 0.5f)
                            .SetDelay(_delay).SetEase(Ease.Flash);

                        stackObj.ElementAt(i).parent = clawPlace;
                        stackObj.RemoveAt(i);

                        // paints[i - 1].GetComponent<Rigidbody>().isKinematic = false;
                        // StartCoroutine("DelayRB");
                        var BallstackObj = hit.collider.transform.parent.GetComponent<BallStack>();
                        _yAxis += BallstackObj.YAxisOffset;
                        _delay += 0.02f;
                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.forward * 1f, Color.red);
                }

            }
        }

    }
}

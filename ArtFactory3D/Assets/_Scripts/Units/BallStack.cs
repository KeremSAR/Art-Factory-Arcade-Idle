using System.Collections;

using DG.Tweening;
using UnityEngine;

namespace ArtFactory._Scripts.Units
{
    public class BallStack : MonoBehaviour
    {
        [SerializeField] private Transform[] BallPlace;
        [SerializeField] private GameObject Ball;
        [SerializeField] private Transform hole;
        [SerializeField] private float DeliveryTime;
        
        public float YAxisOffset;
        public float CountPaints, YAxis;
        public int PP_index = 0;
        void Start()
        {
            var len = BallPlace.Length;
            for (int i = 0; i < len; i++)
            {
                BallPlace[i] = transform.GetChild(0).GetChild(i);
            }

            StartCoroutine(DeliverBall(DeliveryTime));
        }

        IEnumerator DeliverBall(float Time)
        {
            CountPaints = 0;

            while (CountPaints < 100)
            {
                var position = hole.position;
                GameObject newPaint = Instantiate(Ball, new Vector3(position.x, -3f, position.z),
                    Quaternion.identity, transform.GetChild(1));

                newPaint.transform
                    .DOJump(
                        new Vector3(BallPlace[PP_index].position.x, BallPlace[PP_index].position.y + YAxis,
                            BallPlace[PP_index].position.z), 2f, 1, 0.5f).SetEase(Ease.OutQuad);

                if (PP_index < BallPlace.Length-1)
                {
                    PP_index++;
                }
                else
                {
                    PP_index = 0;
                    YAxis += YAxisOffset;
                }
                Debug.Log("pp index" +  PP_index);
                yield return new WaitForSecondsRealtime(Time);
            }

        }
    }
}

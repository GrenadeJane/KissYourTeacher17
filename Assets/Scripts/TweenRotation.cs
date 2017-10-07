﻿using UnityEngine;
using System.Collections;

namespace ActiveMe
{
    namespace Tweens
    {
        public class TweenRotation : UITweener
        {
            [SerializeField]
            public bool isLocal = true;
            [SerializeField]
            public bool useQuaternion = true;
            [SerializeField]
            public Vector3 src;
            [SerializeField]
            public Vector3 dst;


            public override void ResetAtBeginning()
            {
                base.ResetAtBeginning();

                if (useQuaternion)
                {
                    if (isLocal)
                        Target.localRotation = Quaternion.Euler(src);
                    else
                        Target.rotation = Quaternion.Euler(src);
                }
                else
                {
                    if (isLocal)
                        Target.localEulerAngles = src;
                    else
                        Target.eulerAngles = src;
                }
            }

            public override void ResetAtTheEnd()
            {
                base.ResetAtTheEnd();

                if (useQuaternion)
                {
                    if (isLocal)
                        Target.localRotation = Quaternion.Euler(dst);
                    else
                        Target.rotation = Quaternion.Euler(dst);
                }
                else
                {
                    if (isLocal)
                        Target.localEulerAngles = dst;
                    else
                        Target.eulerAngles = dst;
                }
            }

            public override void Animate()
            {
                base.Animate();

                if (useQuaternion)
                {
                    if (isLocal)
                        Target.localRotation = Quaternion.Lerp(Quaternion.Euler(src), Quaternion.Euler(dst), curve.Evaluate(factor));
                    else
                        Target.rotation = Quaternion.Lerp(Quaternion.Euler(src), Quaternion.Euler(dst), curve.Evaluate(factor));
                }
                else
                {
                    if (isLocal)
                        Target.localEulerAngles = Vector3.Lerp(src, dst, curve.Evaluate(factor));
                    else
                        Target.eulerAngles = Vector3.Lerp(src, dst, curve.Evaluate(factor));
                }
            }

            private void Update()
            {
                // Update factor over time
                FactorUpdate();

                //Do Tween
                Animate();

                // Check if factor is "< 0" or "> 1"
                CheckEndTween();
            }
        }
    }
}
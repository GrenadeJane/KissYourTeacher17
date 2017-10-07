﻿using UnityEngine;
using System.Collections;

        public class UITweenScale : UITweener
        {
            [SerializeField]
            public Vector3 src = Vector3.one;
            [SerializeField]
            public Vector3 dst = Vector3.one;

            public override void ResetAtBeginning()
            {
                base.ResetAtBeginning();
                RectTransform.localScale = src;
            }

            public override void ResetAtTheEnd()
            {
                base.ResetAtTheEnd();
                RectTransform.localScale = dst;
            }

            public override void Animate()
            {
                base.Animate();

                RectTransform.localScale = Vector3.Lerp(src, dst, curve.Evaluate(factor));

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

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoveYouForever
{
    [CreateAssetMenu(fileName = "TestData", menuName = "Test/TestData")]
    public class TestDataConfigs : ConfigsBase
    {
        [ShowInInspector]
        public int TestInt1;
        public int TestInt2;
        public int TestInt3;
        public int TestInt4;
        public int TestInt5;
        
        
        public float TestFloat1;
        public float TestFloat2;
        public float TestFloat3;
        public float TestFloat4;
        public float TestFloat5;

        
        public Ease TestEase1;
        public Ease TestEase2;
        public Ease TestEase3;

        [Button("重置测试数据")]
        private void ResetTestData()
        {
            TestInt1 = 0;
            TestInt2 = 0;
            TestInt3 = 0;
            TestInt4 = 0;
            TestInt5 = 0;
            
            TestFloat1 = 0;
            TestFloat2 = 0;
            TestFloat3 = 0;
            TestFloat4 = 0;
            TestFloat5 = 0;
            
            TestEase1 = Ease.Unset;
            TestEase2 = Ease.Unset;
            TestEase3 = Ease.Unset;
        }
    }
}

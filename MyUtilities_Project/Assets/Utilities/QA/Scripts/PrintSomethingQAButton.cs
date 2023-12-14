using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.QA
{
    public class PrintSomethingQAButton : AbstractQAButton
    {
        public override void DoSomething()
        {
            Debug.Log($"PrintSomethingQAButton.DoSomething()");
        }
    }
}

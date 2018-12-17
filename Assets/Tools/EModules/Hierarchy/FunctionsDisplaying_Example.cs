//////// Custom Tree-IGenericMenu Example ////////
/*
    Display of variables and functions in the hierarchy window.
    Use '[SHOW_IN_HIER]' attribute in your code. 
*/

#if UNITY_EDITOR

using UnityEngine;


namespace Hierarchy_Examples
{
    class FunctionsDisplaying_Example : MonoBehaviour
    {


        // "Method button" + custom button width
        [SHOW_IN_HIER(width: 26)]
        public float Method1()
        {
            //Debug.Log("Method1()");
            return 0;
        }



        // "Method button" with default arguments
        [SHOW_IN_HIER]
        public void Method2(string str)
        {
            Debug.Log("Method2(): " + string.IsNullOrEmpty(str));
        }



        // "Property Label" + custom color {r,g,b,a}
        [SHOW_IN_HIER(color: new float[] { 1, 0, 0, 1 })]
        GameObject Target {
            get { return null; }
        }



        // "Field Label"
        [SHOW_IN_HIER]
        float speedField;


    }
}
#endif
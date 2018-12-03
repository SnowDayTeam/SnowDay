using UnityEngine;


namespace SnowDay.Diego.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T m_Instance;

        public static T GetInstance()
        {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<T>();

                if (!m_Instance)
                {
                    var singletonObject = new GameObject();
                    m_Instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }

            }
            return m_Instance;

        }
    }
}
using UnityEngine;

namespace SnowDay.Diego.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_Instance;

        
        private bool destroyOnLoad = true;
        /// <summary>
        /// Destroy manager on scene load. (Default = true)
        /// </summary>
        public bool DestroyOnLoad {
            get
            {
                return destroyOnLoad;
            }
            set
            {
                destroyOnLoad = value;
                if (!destroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        /// <summary>
        /// <para>Gets instance of manager.</para> 
        /// <para>Creates on if it isn't available in the scene.</para>
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            if (!m_Instance)
            {
                m_Instance = FindObjectOfType<T>();

                if (!m_Instance)
                {
                    CreateManager();
                }
            }
            return m_Instance;
        }

        /// <summary>
        /// Creates Manager
        /// </summary>
        private static void CreateManager()
        {
            var singletonObject = new GameObject();

            m_Instance = singletonObject.AddComponent<T>();

            singletonObject.name = typeof(T).ToString() + " (Singleton)";
        }
    }
}
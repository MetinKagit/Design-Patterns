using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T m_instance;
    static bool s_Quitting;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics()
    {
        m_instance = null;
        s_Quitting = false;
    }
    protected virtual void OnApplicationQuit() => s_Quitting = true;
    public static T Instance
    {
        get
        {
            if (s_Quitting) return null;

            if (m_instance == null)
            {
                m_instance = GameObject.FindFirstObjectByType<T>(FindObjectsInactive.Include);
                if (m_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_instance = singleton.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }
    public virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (m_instance == this as T) m_instance = null;
    }
}

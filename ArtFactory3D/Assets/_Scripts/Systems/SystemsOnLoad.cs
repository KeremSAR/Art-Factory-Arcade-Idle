using UnityEngine;
using Object = UnityEngine.Object;

namespace ArtFactory._Scripts.Systems
{
    /* Loads the system prefab before on loading scene, it can be generated a subobjects inside System prefab and it will instantiates in every scene*/
    public static class SystemsOnLoad     
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute() => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));

    }
}

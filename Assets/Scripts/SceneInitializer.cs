using System.Linq;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private DataCenter DataCenter; // Drag and drop in Inspector or initialize in code

    void Awake()
    {
        // Find all objects in the scene that implement the IUsesDataCenter interface
        var dependentObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IUsesDataCenter>();

        foreach (var obj in dependentObjects)
        {
            obj.SetDependency(DataCenter);
        }
    }
}

using System;
using System.Linq;
using AI_Integration;
using UnityEngine;


public class DependencyInjector : MonoBehaviour
{
    private void Awake()
    {
        var objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        
        foreach (var dependency in objects.OfType<IDependency<INotifier<CombinationEvent>>>())
        {
            dependency.SetDependency(CombinationEventNotifier.Instance);
        }

        foreach (var dependency in objects.OfType<IDependency<IAIManager>>())
        {
            dependency.SetDependency(AIManager.Instance);
        }
    }
}
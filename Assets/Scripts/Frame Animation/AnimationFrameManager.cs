using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationFrameManager : MonoBehaviour, IAnimationFrameSubject
{
    private readonly List<IAnimationFrameObserver> _observers = new List<IAnimationFrameObserver>();
    [SerializeField] private int frame = 0;
    [SerializeField] private int fps = 4;

    public void SendMessage()
    {
        foreach (IAnimationFrameObserver observer in _observers)
        {
            observer.OnAnimationFrame(frame);
        }
    }

    public void SubscribeToAnimationFrame(IAnimationFrameObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnsubscribeFromAnimationFrame(IAnimationFrameObserver observer)
    {
        _observers.Remove(observer);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitForNextFrame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitForNextFrame()
    {
        yield return new WaitForSeconds(1f / fps);

        frame += 1;
        SendMessage();

        StartCoroutine(WaitForNextFrame());
    }
}

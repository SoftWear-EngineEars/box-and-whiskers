using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationFrameManager : MonoBehaviour, AnimationFrameSubject
{
    private List<AnimationFrameObserver> observers = new List<AnimationFrameObserver>();
    [SerializeField] private int frame = 0;
    [SerializeField] private int fps = 4;

    public void SendMessage()
    {
        foreach (AnimationFrameObserver observer in observers)
        {
            observer.OnAnimationFrame(frame);
        }
    }

    public void SubscribeToAnimationFrame(AnimationFrameObserver observer)
    {
        observers.Add(observer);
    }

    public void UnsubscribeFromAnimationFrame(AnimationFrameObserver observer)
    {
        observers.Remove(observer);
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

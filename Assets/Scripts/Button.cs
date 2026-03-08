using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private float pressSpeed;
    [SerializeField] private float upSpeed;

    [SerializeField] private GameObject platform;
    [SerializeField] private Vector2 platformEndOffset;

    private float _yMin;
    private float _yMax;
    private float _velocity;
    
    private Vector2 _platformStartPoint;
    private Vector2 _platformEndPoint;
    
    
    private void Start()
    {
        _platformStartPoint = platform.transform.position;
        _platformEndPoint = (Vector2)platform.transform.position + platformEndOffset;

        _yMax = transform.position.y;
        _yMin = transform.position.y - GetComponent<BoxCollider2D>().size.y;
    }

    private void Update()
    {
        transform.position =
            new Vector2(transform.position.x, Math.Clamp(transform.position.y + _velocity, _yMin, _yMax));

        platform.transform.position = Vector2.Lerp(_platformEndPoint, _platformStartPoint, 
            (transform.position.y - _yMin) / (_yMax - _yMin));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Box>() == null)
            return;

        _velocity = -pressSpeed;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Box>() == null)
            return;
        
        _velocity = upSpeed;
    }
}
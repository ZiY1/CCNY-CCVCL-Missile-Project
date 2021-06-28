using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    //public float speed;
    public Vector3 velocity;

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _velocity)
    {
        position = _position;
        rotation = _rotation;
        //speed = _speed;
        velocity = _velocity;
    }
}

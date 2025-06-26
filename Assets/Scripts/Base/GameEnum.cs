using UnityEngine;

public enum DirectionType
{
    Forward = 0,
    Right = 1,
    Backward = 2,
    Left = 3,
    Up = 4,
    Down = 5
}
public static class GameEnum 
{
    public static Vector3 GetVector3BByDirection(DirectionType direction)
    {
        switch (direction)
        {
            case DirectionType.Forward:
                return Vector3.forward;
            case DirectionType.Right:
                return Vector3.right;
            case DirectionType.Backward:
                return Vector3.back;
            case DirectionType.Left:
                return Vector3.left;
            case DirectionType.Up:
                return Vector3.up;
            case DirectionType.Down:
                return Vector3.down;
        }

        return Vector3.zero;
    }

    public static Vector3 GetAngleRotateByDirection(DirectionType direction)
    {
        switch (direction)
        {
            case DirectionType.Forward:
                return new Vector3(90, 0, 0);
            case DirectionType.Right:
                return new Vector3(0, 0, -90);
            case DirectionType.Backward:
                return new Vector3(-90, 0, 0);
            case DirectionType.Left:
                return new Vector3(0, 0, 90);
            case DirectionType.Up:
                return new Vector3(0, 0, 0);
            case DirectionType.Down:
                return new Vector3(0, 0, 0);
        }

        return Vector3.zero;
    }
}

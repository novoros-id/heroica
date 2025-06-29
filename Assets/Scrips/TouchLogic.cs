using UnityEngine;

public class TouchLogic_ : MonoBehaviour
{
    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 0;
    const float pinchRatio = 1;
    const float minPinchDistance = 0;

    public static float turnAngleDelta;
    public static float turnAngle;
    public static float pinchDistanceDelta;
    public static float pinchDistance;

    private static Touch lastTouch;

    public static void Calculate()
    {
        pinchDistance = pinchDistanceDelta = 0;
        turnAngle = turnAngleDelta = 0;

        if (Input.touchCount == 1)
        {
            Touch touch1 = Input.touches[0];
            turnAngle = Angle(touch1.position, lastTouch.position);
            float prevTurn = Angle(touch1.position + touch1.deltaPosition,
                                   lastTouch.deltaPosition + lastTouch.position);
            turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

            if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
            {
                turnAngleDelta *= pinchTurnRatio;
            }
            else
            {
                turnAngle = turnAngleDelta = 0;
            }
            lastTouch = touch1;
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                pinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float prevDistance = Vector2.Distance(
                    touch1.position - touch1.deltaPosition,
                    touch2.position - touch2.deltaPosition
                );
                pinchDistanceDelta = pinchDistance - prevDistance;

                if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
                {
                    pinchDistanceDelta *= pinchRatio;
                }

                turnAngle = Angle(touch1.position, touch2.position);
                float prevTurn = Angle(
                    touch1.position + touch1.deltaPosition,
                    touch2.position + touch2.deltaPosition
                );
                turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                {
                    turnAngleDelta *= pinchTurnRatio;
                }
            }
        }
    }

    private static float Angle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        Vector2 to = new Vector2(1, 0);
        float result = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);
        if (cross.z > 0)
        {
            result = 360f - result;
        }
        return result;
    }
}
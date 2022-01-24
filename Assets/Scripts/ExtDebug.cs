using UnityEngine;

public static class ExtDebug {
    //Draws just the box at where it is currently hitting.
    public static void DrawBoxCastOnHit(Vector2 origin, Vector2 halfExtents, Quaternion orientation, Vector2 direction, float hitInfoDistance, Color color) {
        origin = CastCenterOnCollision(origin, direction, hitInfoDistance);
        DrawBox(origin, halfExtents, orientation, color);
    }
    
    //Draws the full box from start of cast to its end distance. Can also pass in hitInfoDistance instead of full distance
    public static void DrawBoxCastBox(Vector2 origin, Vector2 halfExtents, Quaternion orientation, Vector2 direction, float distance, Color color) {
        direction.Normalize();
        Box box = new Box(origin, halfExtents, orientation);
        DrawBox(box, color);
    }
    
    public static void DrawBox(Vector2 origin, Vector2 halfExtents, Quaternion orientation, Color color) {
        DrawBox(new Box(origin, halfExtents, orientation), color);
    }
    public static void DrawBox(Box box, Color color) {
        Debug.DrawLine(box.topLeft,     box.topRight,    color);
        Debug.DrawLine(box.topRight,     box.bottomRight, color);
        Debug.DrawLine(box.bottomRight, box.bottomLeft, color);
        Debug.DrawLine(box.bottomLeft,     box.topLeft, color);
    }
    
    public struct Box
    {
        public Vector2 localTopLeft     {get; private set;}
        public Vector2 localTopRight    {get; private set;}
        public Vector2 localBottomLeft  {get; private set;}
        public Vector2 localBottomRight {get; private set;}

        public Vector2 topLeft     {get {return localTopLeft + origin;}}
        public Vector2 topRight    {get {return localTopRight + origin;}}
        public Vector2 bottomLeft  {get {return localBottomLeft + origin;}}
        public Vector2 bottomRight {get {return localBottomRight + origin;}}

        public Vector2 origin {get; private set;}

        public Box(Vector2 origin, Vector2 halfExtents, Quaternion orientation) : this(origin, halfExtents) {
            Rotate(orientation);
        }

        public Box(Vector2 origin, Vector2 halfExtents) {
            halfExtents /= 2f;
            this.localTopLeft     = new Vector2(-halfExtents.x, halfExtents.y);
            this.localTopRight    = new Vector2(halfExtents.x, halfExtents.y);
            this.localBottomLeft  = new Vector2(-halfExtents.x, -halfExtents.y);
            this.localBottomRight = new Vector2(halfExtents.x, -halfExtents.y);

            this.origin = origin;
        }

        public void Rotate(Quaternion orientation) {
            localTopLeft     = RotatePointAroundPivot(localTopLeft    , Vector2.zero, orientation);
            localTopRight    = RotatePointAroundPivot(localTopRight   , Vector2.zero, orientation);
            localBottomLeft  = RotatePointAroundPivot(localBottomLeft , Vector2.zero, orientation);
            localBottomRight = RotatePointAroundPivot(localBottomRight, Vector2.zero, orientation);
        }
    }

    //This should work for all cast types
    static Vector2 CastCenterOnCollision(Vector2 origin, Vector2 direction, float hitInfoDistance) {
        return origin + (direction.normalized * hitInfoDistance);
    }
    
    static Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, Quaternion rotation) {
        Vector2 direction = point - pivot;
        return (Vector3)pivot + rotation * direction;
    }
}
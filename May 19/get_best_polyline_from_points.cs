
//The Comparer which compares point members from the angle between x
//positive axis and the vector from center point to member point.  
public class PointsSort : IComparer<Point3d>
{
    Point3d centerPoint;
    public PointsSort(Point3d point)
    {
        centerPoint = point;
    }

    public int Compare(Point3d point1, Point3d point2)
    {
        Vector3d vec1 = new Line(centerPoint, point1).Direction;
        Vector3d vec2 = new Line(centerPoint, point2).Direction;

        double angle1 = Vector3d.VectorAngle(vec1, new Vector3d(1, 0, 0));


        if (vec1.Y < 0.0)
        {
            angle1 = (2 * Math.PI - angle1);
        }


        double angle2 = Vector3d.VectorAngle(vec2, new Vector3d(1, 0, 0));
        if (vec2.Y < 0.0)
        {
            angle2 = (2 * Math.PI - angle2);
        }

        return angle1.CompareTo(angle2);
    }
}



//Input a List of Point3d
//Return a polyline go through all points and with no intersection.
PolylineCurve getBestPolyline(List<Point3d> originalPoints)
{
    Point3d centerPoint = new Point3d(0, 0, 0);

    foreach (Point3d point in originalPoints)
    {
        centerPoint += point;
    }
    centerPoint = centerPoint / originalPoints.Count;



    originalPoints.Sort(new PointsSort(centerPoint));
    originalPoints.Add(originalPoints[0]);

    return new PolylineCurve(originalPoints);
}
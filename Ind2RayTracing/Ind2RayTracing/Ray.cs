using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ind2RayTracing
{
    public class Ray
    {
        public static float EPSILON = 0.0001f;

        public Point3D start, dir;

        public Ray(Point3D st, Point3D end)
        {
            start = new Point3D(st);
            dir = Point3D.norm(end - st);

        }

        private Ray() { }

        public Point3D tpos(float t)
        {
            return start + dir * t;
        }

        public static Ray buildRay(Point3D st, Point3D dir)
        {
            Ray r = new Ray();
            r.start = new Point3D(st);
            r.dir = new Point3D(dir);
            return r;
        }


        public bool intersectTriangle(Point3D vertex0, Point3D vertex1, Point3D vertex2, out float t)
        {
            t = -1;
            const float EPSILON = 0.0001f;
            Point3D edge1, edge2, h, s, q;
            float a, f, u, v;
            edge1 = vertex1 - vertex0;
            edge2 = vertex2 - vertex0;
            h = dir * edge2;
            a = Point3D.scalar(edge1, h);
            if (a > -EPSILON && a < EPSILON)
                return false;    // This ray is parallel to this triangle.
            f = 1.0f / a;
            s = start - vertex0;
            u = Point3D.scalar(s, h) * f;
            if (u < 0.0 || u > 1.0)
                return false;
            q = s * edge1;
            v = Point3D.scalar(dir, q) * f;
            if (v < 0.0 || u + v > 1.0)
                return false;
            // At this stage we can compute t to find out where the intersection point is on the line.
            t = Point3D.scalar(edge2, q) * f;
            if (t > EPSILON) // ray intersection
            {

                return true;
            }
            else // This means that there is a line intersection but not a ray intersection.
                return false;
        }
    }
}

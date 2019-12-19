using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ind2RayTracing
{
    public class Hit
    {
        public float hit_point;
        public Ray casted_ray;
        public Point3D normal;
        public Material mat;
        public bool success;

        public Hit(float hp, Ray r, Point3D n, Material m)
        {
            hit_point = hp;
            casted_ray = r;
            normal = new Point3D(n);
            mat = m;
            success = true;
        }

        public Hit()
        {
            success = false;
            hit_point = -1;
            casted_ray = null;
            normal = null;

        }

        public Point3D Shade(Light l, Point3D hp, Point3D eye)
        {
            //  Point3D d = Point3D.norm(hp - l.position);
            //  d = l.clr * Math.Max(Point3D.scalar(d, normal),0) ;

            Point3D l2 = Point3D.norm(l.position - hp);
            Point3D v2 = Point3D.norm(eye - hp);
            Point3D r = reflectVec(l2 * -1, normal);
            Point3D diff = mat.dif_coef * l.clr * Math.Max(Point3D.scalar(normal, l2), 0.0f);
            Point3D spec = mat.spec_coef * l.clr * (float)Math.Pow(Math.Max(Point3D.scalar(r, v2), 0.0f), mat.shine_coef);


            return Point3D.blend(diff, mat.clr);
        }

        public Ray Reflect(Point3D hp)
        {
            return Ray.buildRay(hp, Point3D.norm(casted_ray.dir - 2 * normal * Point3D.scalar(normal, casted_ray.dir)));

        }

        public static Point3D reflectVec(Point3D v, Point3D n)
        {
            return Point3D.norm(v - 2 * n * Point3D.scalar(n, v));

        }

        public Ray Refract(Point3D hp, float eta)
        {
            float nidot = Point3D.scalar(normal, casted_ray.dir);
            float k = 1.0f - eta * eta * (1.0f - nidot * nidot);
            if (k >= 0)
            {
                k = (float)Math.Sqrt(k);
                return Ray.buildRay(hp, Point3D.norm(eta * casted_ray.dir - (k + eta * nidot) * normal));
            }
            else
                return null;


        }



    }
}

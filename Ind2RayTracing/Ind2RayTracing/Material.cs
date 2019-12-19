using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ind2RayTracing
{
    public struct Material
    {
        public Point3D clr;
        public float reflection_coef;
        public float refraction_coef;
        public float env_coef;

        public float amb_coef;
        public float dif_coef;
        public float spec_coef;
        public float shine_coef;

        public Material(Point3D c, float refl, float refr, float ec, float ac, float dc, float sc, float shc)
        {
            clr = new Point3D(c);
            reflection_coef = refl;
            refraction_coef = refr;
            env_coef = ec;
            amb_coef = ac;
            dif_coef = dc;
            spec_coef = sc;
            shine_coef = shc;

        }

        public void SetColor(Color c)
        {
            clr = new Point3D(c.R / 255.0f, c.G / 255.0f, c.B / 255.0f);
        }

    }
}

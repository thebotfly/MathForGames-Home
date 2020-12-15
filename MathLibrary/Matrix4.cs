using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public class Matrix4
    {
        public float m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44;

        public Matrix4()
        {
            m11 = 1; m12 = 0; m13 = 0; m14 = 0;
            m21 = 0; m22 = 1; m23 = 0; m24 = 0;
            m31 = 0; m32 = 0; m33 = 1; m34 = 0;
            m41 = 0; m42 = 0; m43 = 0; m44 = 1;
        }

        public Matrix4(float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13; this.m14 = m14;
            this.m21 = m21; this.m22 = m22; this.m23 = m23; this.m24 = m24;
            this.m31 = m31; this.m32 = m32; this.m33 = m33; this.m34 = m34;
            this.m41 = m41; this.m42 = m42; this.m43 = m43; this.m44 = m44;
        }

        public static Matrix4 operator +(Matrix4 Ihs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    Ihs.m11 + rhs.m11, Ihs.m12 + rhs.m12, Ihs.m13 + rhs.m13, Ihs.m14 + rhs.m14,
                    Ihs.m21 + rhs.m21, Ihs.m22 + rhs.m22, Ihs.m23 + rhs.m23, Ihs.m24 + rhs.m24,
                    Ihs.m31 + rhs.m31, Ihs.m32 + rhs.m32, Ihs.m33 + rhs.m33, Ihs.m34 + rhs.m34,
                    Ihs.m41 + rhs.m41, Ihs.m42 + rhs.m42, Ihs.m43 + rhs.m43, Ihs.m44 + rhs.m44
                );
        }

        public static Matrix4 operator -(Matrix4 Ihs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    Ihs.m11 - rhs.m11, Ihs.m12 - rhs.m12, Ihs.m13 - rhs.m13, Ihs.m14 - rhs.m14,
                    Ihs.m21 - rhs.m21, Ihs.m22 - rhs.m22, Ihs.m23 - rhs.m23, Ihs.m24 - rhs.m24,
                    Ihs.m31 - rhs.m31, Ihs.m32 - rhs.m32, Ihs.m33 - rhs.m33, Ihs.m34 - rhs.m34,
                    Ihs.m41 - rhs.m41, Ihs.m42 - rhs.m42, Ihs.m43 - rhs.m43, Ihs.m44 - rhs.m44
                );
        }

        public static Matrix4 operator *(Matrix4 Ihs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    //Row1
                    Ihs.m11 * rhs.m11 + Ihs.m12 * rhs.m21 + Ihs.m13 * rhs.m31 + Ihs.m14 * rhs.m41,
                    Ihs.m11 * rhs.m12 + Ihs.m12 * rhs.m22 + Ihs.m13 * rhs.m32 + Ihs.m14 * rhs.m42,
                    Ihs.m11 * rhs.m13 + Ihs.m12 * rhs.m23 + Ihs.m13 * rhs.m33 + Ihs.m14 * rhs.m43,
                    Ihs.m11 * rhs.m14 + Ihs.m12 * rhs.m24 + Ihs.m13 * rhs.m34 + Ihs.m14 * rhs.m44,
                    //Row 2
                    Ihs.m21 * rhs.m11 + Ihs.m22 * rhs.m21 + Ihs.m23 * rhs.m31 + Ihs.m24 * rhs.m41,
                    Ihs.m21 * rhs.m12 + Ihs.m22 * rhs.m22 + Ihs.m23 * rhs.m32 + Ihs.m24 * rhs.m42,
                    Ihs.m21 * rhs.m13 + Ihs.m22 * rhs.m23 + Ihs.m23 * rhs.m33 + Ihs.m24 * rhs.m43,
                    Ihs.m21 * rhs.m14 + Ihs.m22 * rhs.m24 + Ihs.m23 * rhs.m34 + Ihs.m24 * rhs.m44,
                    //Row 3
                    Ihs.m31 * rhs.m11 + Ihs.m32 * rhs.m21 + Ihs.m33 * rhs.m31 + Ihs.m34 * rhs.m41,
                    Ihs.m31 * rhs.m12 + Ihs.m32 * rhs.m22 + Ihs.m33 * rhs.m32 + Ihs.m34 * rhs.m42,
                    Ihs.m31 * rhs.m13 + Ihs.m32 * rhs.m23 + Ihs.m33 * rhs.m33 + Ihs.m34 * rhs.m43,
                    Ihs.m31 * rhs.m14 + Ihs.m32 * rhs.m24 + Ihs.m33 * rhs.m34 + Ihs.m34 * rhs.m44,
                    //Row 4
                    Ihs.m41 * rhs.m11 + Ihs.m42 * rhs.m21 + Ihs.m43 * rhs.m31 + Ihs.m44 * rhs.m41,
                    Ihs.m41 * rhs.m12 + Ihs.m42 * rhs.m22 + Ihs.m43 * rhs.m32 + Ihs.m44 * rhs.m42,
                    Ihs.m41 * rhs.m13 + Ihs.m42 * rhs.m23 + Ihs.m43 * rhs.m33 + Ihs.m44 * rhs.m43,
                    Ihs.m41 * rhs.m14 + Ihs.m42 * rhs.m24 + Ihs.m43 * rhs.m34 + Ihs.m44 * rhs.m44
                );
        }

        public static Vector4 operator *(Matrix4 Ihs, Vector4 rhs)
        {
            return new Vector4(Ihs.m11 * rhs.X + Ihs.m12 * rhs.Y + Ihs.m13 * rhs.Z + Ihs.m14 + rhs.W,
                Ihs.m21 * rhs.X + Ihs.m22 * rhs.Y + Ihs.m23 * rhs.Z + Ihs.m24 + rhs.W,
               Ihs.m31 * rhs.X + Ihs.m32 * rhs.Y + Ihs.m33 * rhs.Z + Ihs.m34 * rhs.W,
               Ihs.m41 * rhs.X + Ihs.m42 * rhs.Y + Ihs.m43 * rhs.Z + Ihs.m44 * rhs.W);
        }













        public static Matrix4 CreateRotationZ(float radians)
        {
            return new Matrix4
                (
                    (float)Math.Cos(radians), -(float)Math.Sin(radians), 0, 0,
                    (float)Math.Sin(radians), (float)Math.Cos(radians), 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                );
        }

        public static Matrix4 CreateRotationY(float radians)
        {
            return new Matrix4
                (
                    (float)Math.Cos(radians), 0, (float)Math.Sin(radians), 0,
                    0, 1, 0, 0,
                    -(float)Math.Sin(radians), 0, (float)Math.Cos(radians), 0,
                    0, 0, 0, 1
                );
        }

        public static Matrix4 CreateRotationX(float radians)
        {
            return new Matrix4
                (
                    1, 0, 0, 0,
                    0, (float)Math.Cos(radians), -(float)Math.Sin(radians), 0,
                    0, (float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                    0, 0, 0, 1
                );
        }

        public static Matrix4 CreateTranslation(Vector3 position)
        {
            return new Matrix4
                (
                    1, 0, 0, position.X,
                    0, 1, 0, position.Y,
                    0, 0, 1, position.Z,
                    0, 0, 0, 1
                );
        }

        public static Matrix4 CreateScale(Vector3 scale)
        {
            return new Matrix4
                (
                    scale.X, 0, 0, 0,
                    0, scale.Y, 0, 0,
                    0, 0, scale.Z, 0,
                    0, 0, 0, 1
                );
        }
    }
}

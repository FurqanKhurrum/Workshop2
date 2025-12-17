using System;

namespace MathLibrary
{
    /// <summary>
    /// Represents a 3D vector with x, y, z components
    /// </summary>
    public class Vector3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        /// <summary>
        /// Constructor for Vector3D
        /// </summary>
        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Vector addition
        /// </summary>
        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Vector subtraction
        /// </summary>
        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Dot product of two vectors
        /// </summary>
        public static float DotProduct(Vector3D a, Vector3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// Cross product of two vectors
        /// </summary>
        public static Vector3D CrossProduct(Vector3D a, Vector3D b)
        {
            return new Vector3D(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        /// <summary>
        /// Magnitude (length) of the vector
        /// </summary>
        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Normalize the vector (make it unit length)
        /// </summary>
        public Vector3D Normalize()
        {
            float mag = Magnitude();
            if (mag > 0)
            {
                return new Vector3D(X / mag, Y / mag, Z / mag);
            }
            return new Vector3D(0, 0, 0);
        }

        /// <summary>
        /// String representation of the vector
        /// </summary>
        public override string ToString()
        {
            return $"({X:F2}, {Y:F2}, {Z:F2})";
        }
    }
}
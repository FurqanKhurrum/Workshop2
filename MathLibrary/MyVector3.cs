using System;

namespace MathLibrary
{
    /// <summary>
    /// Represents a 4x4 matrix for 3D transformations
    /// </summary>
    public class Matrix4x4
    {
        private float[,] data;

        /// <summary>
        /// Constructor - creates a zero matrix by default
        /// </summary>
        public Matrix4x4()
        {
            data = new float[4, 4];
        }

        /// <summary>
        /// Constructor with initial data
        /// </summary>
        public Matrix4x4(float[,] values)
        {
            if (values.GetLength(0) != 4 || values.GetLength(1) != 4)
                throw new ArgumentException("Matrix must be 4x4");

            data = new float[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    data[i, j] = values[i, j];
                }
            }
        }

        /// <summary>
        /// Indexer for matrix elements
        /// </summary>
        public float this[int row, int col]
        {
            get { return data[row, col]; }
            set { data[row, col] = value; }
        }

        /// <summary>
        /// Creates an identity matrix
        /// </summary>
        public static Matrix4x4 Identity()
        {
            Matrix4x4 result = new Matrix4x4();
            for (int i = 0; i < 4; i++)
            {
                result[i, i] = 1.0f;
            }
            return result;
        }

        /// <summary>
        /// Creates a scaling matrix
        /// </summary>
        public static Matrix4x4 Scale(float scaleX, float scaleY, float scaleZ)
        {
            Matrix4x4 result = Identity();
            result[0, 0] = scaleX;
            result[1, 1] = scaleY;
            result[2, 2] = scaleZ;
            return result;
        }

        /// <summary>
        /// Creates a rotation matrix around the X axis
        /// </summary>
        public static Matrix4x4 RotationX(float angleInDegrees)
        {
            float rad = angleInDegrees * (float)(Math.PI / 180.0);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);

            Matrix4x4 result = Identity();
            result[1, 1] = cos;
            result[1, 2] = -sin;
            result[2, 1] = sin;
            result[2, 2] = cos;
            return result;
        }

        /// <summary>
        /// Creates a rotation matrix around the Y axis
        /// </summary>
        public static Matrix4x4 RotationY(float angleInDegrees)
        {
            float rad = angleInDegrees * (float)(Math.PI / 180.0);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);

            Matrix4x4 result = Identity();
            result[0, 0] = cos;
            result[0, 2] = sin;
            result[2, 0] = -sin;
            result[2, 2] = cos;
            return result;
        }

        /// <summary>
        /// Creates a rotation matrix around the Z axis
        /// </summary>
        public static Matrix4x4 RotationZ(float angleInDegrees)
        {
            float rad = angleInDegrees * (float)(Math.PI / 180.0);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);

            Matrix4x4 result = Identity();
            result[0, 0] = cos;
            result[0, 1] = -sin;
            result[1, 0] = sin;
            result[1, 1] = cos;
            return result;
        }

        /// <summary>
        /// Creates a translation matrix
        /// </summary>
        public static Matrix4x4 Translation(float x, float y, float z)
        {
            Matrix4x4 result = Identity();
            result[0, 3] = x;
            result[1, 3] = y;
            result[2, 3] = z;
            return result;
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result = new Matrix4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }

        /// <summary>
        /// Transform a vector by this matrix
        /// </summary>
        public Vector3D TransformVector(Vector3D v)
        {
            float w = data[3, 0] * v.X + data[3, 1] * v.Y + data[3, 2] * v.Z + data[3, 3];
            if (Math.Abs(w) < 0.00001f) w = 1.0f; // Avoid division by zero

            return new Vector3D(
                (data[0, 0] * v.X + data[0, 1] * v.Y + data[0, 2] * v.Z + data[0, 3]) / w,
                (data[1, 0] * v.X + data[1, 1] * v.Y + data[1, 2] * v.Z + data[1, 3]) / w,
                (data[2, 0] * v.X + data[2, 1] * v.Y + data[2, 2] * v.Z + data[2, 3]) / w
            );
        }

        /// <summary>
        /// Transform a point (considering translation)
        /// </summary>
        public Vector3D TransformPoint(Vector3D p)
        {
            return TransformVector(p);
        }

        /// <summary>
        /// Transform a direction (ignoring translation)
        /// </summary>
        public Vector3D TransformDirection(Vector3D d)
        {
            return new Vector3D(
                data[0, 0] * d.X + data[0, 1] * d.Y + data[0, 2] * d.Z,
                data[1, 0] * d.X + data[1, 1] * d.Y + data[1, 2] * d.Z,
                data[2, 0] * d.X + data[2, 1] * d.Y + data[2, 2] * d.Z
            );
        }

        /// <summary>
        /// String representation of the matrix
        /// </summary>
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < 4; i++)
            {
                result += "[";
                for (int j = 0; j < 4; j++)
                {
                    result += $"{data[i, j],8:F3}";
                    if (j < 3) result += ", ";
                }
                result += "]\n";
            }
            return result;
        }
    }
}
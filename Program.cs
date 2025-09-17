using System;
using MathLibrary;
using OpenTK.Mathematics;

namespace VectorMatrixDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("    VECTOR AND MATRIX OPERATIONS DEMO");
            Console.WriteLine("==============================================\n");

            // Demonstrate Vector Operations
            DemonstrateVectorOperations();

            // Demonstrate Matrix Operations
            DemonstrateMatrixOperations();

            // Demonstrate Transformations
            DemonstrateTransformations();

            // Comparison with OpenTK (optional)
            CompareWithOpenTK();

            Console.WriteLine("\n==============================================");
            Console.WriteLine("    DEMO COMPLETE - Press any key to exit");
            Console.WriteLine("==============================================");
            Console.ReadKey();
        }

        static void DemonstrateVectorOperations()
        {
            Console.WriteLine(">>> VECTOR OPERATIONS <<<\n");

            // Create test vectors
            Vector3D v1 = new Vector3D(3, 4, 5);
            Vector3D v2 = new Vector3D(1, 2, 3);

            Console.WriteLine($"Vector 1: {v1}");
            Console.WriteLine($"Vector 2: {v2}");
            Console.WriteLine();

            // Addition
            Vector3D vAdd = v1 + v2;
            Console.WriteLine($"Addition (v1 + v2): {vAdd}");

            // Subtraction
            Vector3D vSub = v1 - v2;
            Console.WriteLine($"Subtraction (v1 - v2): {vSub}");

            // Dot Product
            float dotProduct = Vector3D.DotProduct(v1, v2);
            Console.WriteLine($"Dot Product (v1 · v2): {dotProduct:F2}");

            // Cross Product
            Vector3D crossProduct = Vector3D.CrossProduct(v1, v2);
            Console.WriteLine($"Cross Product (v1 × v2): {crossProduct}");

            // Magnitude
            Console.WriteLine($"Magnitude of v1: {v1.Magnitude():F2}");
            Console.WriteLine($"Magnitude of v2: {v2.Magnitude():F2}");

            // Normalization
            Vector3D v1Normalized = v1.Normalize();
            Console.WriteLine($"Normalized v1: {v1Normalized}");
            Console.WriteLine($"Magnitude of normalized v1: {v1Normalized.Magnitude():F2}");

            Console.WriteLine("\n----------------------------------------------\n");
        }

        static void DemonstrateMatrixOperations()
        {
            Console.WriteLine(">>> MATRIX OPERATIONS <<<\n");

            // Identity Matrix
            Matrix4x4 identity = Matrix4x4.Identity();
            Console.WriteLine("Identity Matrix:");
            Console.WriteLine(identity);

            // Scaling Matrix
            Matrix4x4 scale = Matrix4x4.Scale(2, 3, 4);
            Console.WriteLine("Scaling Matrix (2, 3, 4):");
            Console.WriteLine(scale);

            // Rotation Matrices
            Matrix4x4 rotX = Matrix4x4.RotationX(45);
            Console.WriteLine("Rotation Matrix X (45 degrees):");
            Console.WriteLine(rotX);

            Matrix4x4 rotY = Matrix4x4.RotationY(30);
            Console.WriteLine("Rotation Matrix Y (30 degrees):");
            Console.WriteLine(rotY);

            Matrix4x4 rotZ = Matrix4x4.RotationZ(60);
            Console.WriteLine("Rotation Matrix Z (60 degrees):");
            Console.WriteLine(rotZ);

            // Translation Matrix
            Matrix4x4 translation = Matrix4x4.Translation(10, 20, 30);
            Console.WriteLine("Translation Matrix (10, 20, 30):");
            Console.WriteLine(translation);

            // Matrix Multiplication
            Matrix4x4 combined = scale * rotY;
            Console.WriteLine("Combined Matrix (Scale * RotationY):");
            Console.WriteLine(combined);

            Console.WriteLine("\n----------------------------------------------\n");
        }

        static void DemonstrateTransformations()
        {
            Console.WriteLine(">>> VECTOR TRANSFORMATIONS <<<\n");

            // Create a test vector
            Vector3D originalVector = new Vector3D(1, 0, 0);
            Console.WriteLine($"Original Vector: {originalVector}");
            Console.WriteLine();

            // Apply Scaling
            Matrix4x4 scaleMatrix = Matrix4x4.Scale(2, 2, 2);
            Vector3D scaledVector = scaleMatrix.TransformDirection(originalVector);
            Console.WriteLine("After Scaling (2, 2, 2):");
            Console.WriteLine($"  Result: {scaledVector}");
            Console.WriteLine();

            // Apply Rotation around Y axis
            Matrix4x4 rotationMatrix = Matrix4x4.RotationY(90);
            Vector3D rotatedVector = rotationMatrix.TransformDirection(originalVector);
            Console.WriteLine("After Rotation Y (90 degrees):");
            Console.WriteLine($"  Result: {rotatedVector}");
            Console.WriteLine($"  (Note: X-axis vector rotated 90° around Y should point along -Z)");
            Console.WriteLine();

            // Combined Transformation: Scale then Rotate
            Matrix4x4 combinedTransform = rotationMatrix * scaleMatrix;
            Vector3D transformedVector = combinedTransform.TransformDirection(originalVector);
            Console.WriteLine("After Combined Transformation (Scale then Rotate):");
            Console.WriteLine($"  Result: {transformedVector}");
            Console.WriteLine();

            // Transform with Translation
            Vector3D pointToTransform = new Vector3D(1, 1, 1);
            Matrix4x4 fullTransform = Matrix4x4.Translation(5, 0, 0) *
                                      Matrix4x4.RotationZ(45) *
                                      Matrix4x4.Scale(2, 2, 2);
            Vector3D transformedPoint = fullTransform.TransformPoint(pointToTransform);
            Console.WriteLine($"Transform Point {pointToTransform} with Scale(2,2,2), RotZ(45°), Translate(5,0,0):");
            Console.WriteLine($"  Result: {transformedPoint}");

            Console.WriteLine("\n----------------------------------------------\n");
        }

        static void CompareWithOpenTK()
        {
            Console.WriteLine(">>> COMPARISON WITH OpenTK <<<\n");

            // Our library
            Vector3D myVec1 = new Vector3D(3, 4, 5);
            Vector3D myVec2 = new Vector3D(1, 2, 3);
            float myDot = Vector3D.DotProduct(myVec1, myVec2);
            Vector3D myCross = Vector3D.CrossProduct(myVec1, myVec2);

            // OpenTK
            Vector3 otkVec1 = new Vector3(3, 4, 5);
            Vector3 otkVec2 = new Vector3(1, 2, 3);
            float otkDot = Vector3.Dot(otkVec1, otkVec2);
            Vector3 otkCross = Vector3.Cross(otkVec1, otkVec2);

            Console.WriteLine("Dot Product Comparison:");
            Console.WriteLine($"  Our Library: {myDot:F2}");
            Console.WriteLine($"  OpenTK:      {otkDot:F2}");
            Console.WriteLine($"  Match: {(Math.Abs(myDot - otkDot) < 0.001)}");
            Console.WriteLine();

            Console.WriteLine("Cross Product Comparison:");
            Console.WriteLine($"  Our Library: {myCross}");
            Console.WriteLine($"  OpenTK:      ({otkCross.X:F2}, {otkCross.Y:F2}, {otkCross.Z:F2})");
            bool crossMatch = Math.Abs(myCross.X - otkCross.X) < 0.001 &&
                             Math.Abs(myCross.Y - otkCross.Y) < 0.001 &&
                             Math.Abs(myCross.Z - otkCross.Z) < 0.001;
            Console.WriteLine($"  Match: {crossMatch}");

            // Matrix comparison
            Matrix4x4 myMatrix = Matrix4x4.RotationY(45);
            Matrix4 otkMatrix = Matrix4.CreateRotationY((float)(45 * Math.PI / 180));

            Console.WriteLine("\nRotation Matrix Y(45°) Comparison:");
            Console.WriteLine($"  Our [0,0]: {myMatrix[0, 0]:F3}, OpenTK [0,0]: {otkMatrix.M11:F3}");
            Console.WriteLine($"  Our [2,2]: {myMatrix[2, 2]:F3}, OpenTK [2,2]: {otkMatrix.M33:F3}");

            Console.WriteLine("\n----------------------------------------------\n");
        }
    }
}
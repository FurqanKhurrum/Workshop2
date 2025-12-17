using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using MathLibrary;

namespace VectorMatrixDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("    VECTOR AND MATRIX OPERATIONS DEMO");
            Console.WriteLine("==============================================\n");

            // Demonstrate the math operations first (console output)
            DemonstrateVectorOperations();
            DemonstrateMatrixOperations();
            DemonstrateTransformations();

            Console.WriteLine("\n==============================================");
            Console.WriteLine("  Starting Visual Demonstration...");
            Console.WriteLine("==============================================\n");

            // Then launch the visual demo
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Vector & Matrix Transformation Demo",
            };

            using (var game = new Game(GameWindowSettings.Default, nativeWindowSettings))
            {
                game.Run();
            }
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

            // Rotation Matrix (Z-axis)
            Matrix4x4 rotZ = Matrix4x4.RotationZ(45);
            Console.WriteLine("Rotation Matrix Z (45 degrees):");
            Console.WriteLine(rotZ);

            // Matrix Multiplication
            Matrix4x4 combined = scale * rotZ;
            Console.WriteLine("Combined Matrix (Scale * RotationZ):");
            Console.WriteLine(combined);

            Console.WriteLine("\n----------------------------------------------\n");
        }

        static void DemonstrateTransformations()
        {
            Console.WriteLine(">>> APPLYING TRANSFORMATIONS TO VECTORS <<<\n");

            // Create a test vector
            Vector3D originalVector = new Vector3D(1, 0, 0);
            Console.WriteLine($"Original Vector: {originalVector}");
            Console.WriteLine();

            // Apply Scaling
            Matrix4x4 scaleMatrix = Matrix4x4.Scale(2, 2, 2);
            Vector3D scaledVector = scaleMatrix.TransformDirection(originalVector);
            Console.WriteLine("After applying Scaling Matrix (2, 2, 2):");
            Console.WriteLine($"  Result: {scaledVector}");
            Console.WriteLine($"  Expected: (2.00, 0.00, 0.00)");
            Console.WriteLine();

            // Apply Rotation around Z axis
            Matrix4x4 rotationMatrix = Matrix4x4.RotationZ(90);
            Vector3D rotatedVector = rotationMatrix.TransformDirection(originalVector);
            Console.WriteLine("After applying Rotation Matrix Z (90 degrees):");
            Console.WriteLine($"  Result: {rotatedVector}");
            Console.WriteLine($"  Expected: (0.00, 1.00, 0.00) - X-axis rotated 90° around Z points to Y");
            Console.WriteLine();

            // Combined Transformation: Scale then Rotate
            Matrix4x4 combinedTransform = rotationMatrix * scaleMatrix;
            Vector3D transformedVector = combinedTransform.TransformDirection(originalVector);
            Console.WriteLine("After applying Combined Transformation (Scale * Rotation):");
            Console.WriteLine($"  Result: {transformedVector}");
            Console.WriteLine($"  This applies scaling first, then rotation");
            Console.WriteLine();

            // Another example with different vector
            Vector3D testVector = new Vector3D(1, 1, 0);
            Console.WriteLine($"\nApplying transformations to vector {testVector}:");

            Vector3D scaled = scaleMatrix.TransformDirection(testVector);
            Console.WriteLine($"  After Scale (2,2,2): {scaled}");

            Vector3D rotated = rotationMatrix.TransformDirection(testVector);
            Console.WriteLine($"  After Rotation Z (90°): {rotated}");

            Vector3D combined = combinedTransform.TransformDirection(testVector);
            Console.WriteLine($"  After Combined: {combined}");

            Console.WriteLine("\n----------------------------------------------\n");
        }
    }
}
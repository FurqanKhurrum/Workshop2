using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MathLibrary;

namespace VectorMatrixDemo
{
    public class Game : GameWindow
    {
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;
        private int _shaderProgram;

        // Rectangle vertices with colors (position XYZ + color RGB)
        private float[] _vertices = {
            // Positions          // Colors (RGB)
            -0.5f, -0.5f, 0.0f,   1.0f, 0.0f, 0.0f,  // Bottom-left (Red)
             0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,  // Bottom-right (Green)
             0.5f,  0.5f, 0.0f,   0.0f, 0.0f, 1.0f,  // Top-right (Blue)
            -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f   // Top-left (Yellow)
        };

        private uint[] _indices = {
            0, 1, 2,  // First triangle
            2, 3, 0   // Second triangle
        };

        // Transformation properties
        private float _rotation = 0.0f;
        private float _scale = 1.0f;

        // For controlling update rate
        private double _timeSinceLastUpdate = 0.0;
        private const double UpdateInterval = 0.016; // ~60 FPS

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

            // Create VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // Create VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            // Create EBO
            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            // Create and use shader program
            _shaderProgram = CreateShaderProgram();
            GL.UseProgram(_shaderProgram);

            // Configure vertex attributes
            int stride = 6 * sizeof(float); // 3 floats for position + 3 floats for color

            // Position attribute (location = 0)
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, 0);
            GL.EnableVertexAttribArray(0);

            // Color attribute (location = 1)
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, stride, 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            Console.WriteLine("==============================================");
            Console.WriteLine("    VECTOR AND MATRIX VISUAL DEMO");
            Console.WriteLine("==============================================");
            Console.WriteLine("Controls:");
            Console.WriteLine("  Arrow Keys: Rotate the rectangle");
            Console.WriteLine("  +/- Keys: Scale the rectangle");
            Console.WriteLine("  R: Reset transformations");
            Console.WriteLine("  ESC: Exit");
            Console.WriteLine("==============================================\n");
            Console.WriteLine($"Initial - Rotation: {_rotation}°, Scale: {_scale:F2}\n");
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Use shader program
            GL.UseProgram(_shaderProgram);

            // Create transformation matrix using OUR custom math library
            Matrix4x4 transform = CreateTransformationMatrix();

            // Convert to float array and send to shader
            float[] matrixArray = MatrixToArray(transform);
            int transformLocation = GL.GetUniformLocation(_shaderProgram, "transform");
            GL.UniformMatrix4(transformLocation, 1, false, matrixArray);

            // Draw rectangle
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            _timeSinceLastUpdate += args.Time;

            if (_timeSinceLastUpdate < UpdateInterval)
                return;

            _timeSinceLastUpdate = 0;

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            bool changed = false;

            // Rotation controls - MUCH SLOWER (0.5 degrees per update)
            if (input.IsKeyDown(Keys.Left))
            {
                _rotation += 0.5f;
                changed = true;
            }
            if (input.IsKeyDown(Keys.Right))
            {
                _rotation -= 0.5f;
                changed = true;
            }
            if (input.IsKeyDown(Keys.Up))
            {
                _rotation += 0.5f;
                changed = true;
            }
            if (input.IsKeyDown(Keys.Down))
            {
                _rotation -= 0.5f;
                changed = true;
            }

            // Scale controls - MUCH SLOWER (0.01 per update)
            if (input.IsKeyDown(Keys.Equal) || input.IsKeyDown(Keys.KeyPadAdd))
            {
                _scale += 0.01f;
                changed = true;
            }
            if (input.IsKeyDown(Keys.Minus) || input.IsKeyDown(Keys.KeyPadSubtract))
            {
                _scale = Math.Max(0.1f, _scale - 0.01f);
                changed = true;
            }

            // Reset
            if (input.IsKeyPressed(Keys.R))
            {
                _rotation = 0.0f;
                _scale = 1.0f;
                Console.WriteLine("Transformations RESET");
                changed = true;
            }

            if (changed)
            {
                Console.WriteLine($"Rotation: {_rotation:F1}°, Scale: {_scale:F2}");
            }
        }

        private Matrix4x4 CreateTransformationMatrix()
        {
            // Using OUR custom math library!
            Matrix4x4 scaleMatrix = Matrix4x4.Scale(_scale, _scale, 1.0f);
            Matrix4x4 rotationMatrix = Matrix4x4.RotationZ(_rotation);

            // Combine: Scale first, then rotate
            return rotationMatrix * scaleMatrix;
        }

        private float[] MatrixToArray(Matrix4x4 matrix)
        {
            // Convert to column-major order for OpenGL
            float[] result = new float[16];
            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 4; row++)
                {
                    result[col * 4 + row] = matrix[row, col];
                }
            }
            return result;
        }

        private int CreateShaderProgram()
        {
            string vertexShaderSource = @"
#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor;

uniform mat4 transform;

out vec3 fragColor;

void main()
{
    gl_Position = transform * vec4(aPosition, 1.0);
    fragColor = aColor;
}
";

            string fragmentShaderSource = @"
#version 330 core
in vec3 fragColor;
out vec4 FragColor;

void main()
{
    FragColor = vec4(fragColor, 1.0);
}
";

            // Compile vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vSuccess);
            if (vSuccess == 0)
            {
                string log = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine($"VERTEX SHADER ERROR:\n{log}");
            }

            // Compile fragment shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int fSuccess);
            if (fSuccess == 0)
            {
                string log = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine($"FRAGMENT SHADER ERROR:\n{log}");
            }

            // Link program
            int program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int pSuccess);
            if (pSuccess == 0)
            {
                string log = GL.GetProgramInfoLog(program);
                Console.WriteLine($"PROGRAM LINKING ERROR:\n{log}");
            }

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return program;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteBuffer(_elementBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteProgram(_shaderProgram);
        }
    }
}
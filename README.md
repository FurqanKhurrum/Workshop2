## Vector and Matrix Math Library

A custom 3D math library implementation in C# with OpenTK for visual demonstrations.

![20251217-0928-58 1154815](https://github.com/user-attachments/assets/7ce7455b-add2-48c0-8ef2-fab30773df39)

## Features

### Vector3D Class
- **Addition & Subtraction**: Combine and difference vectors
- **Dot Product**: Calculate scalar product between vectors
- **Cross Product**: Compute perpendicular vector
- **Magnitude**: Calculate vector length
- **Normalization**: Convert to unit vectors

### Matrix4x4 Class
- **Identity Matrix**: Base transformation matrix
- **Scaling**: Resize objects uniformly or per-axis
- **Rotation**: Rotate around X, Y, or Z axes
- **Translation**: Move objects in 3D space
- **Matrix Multiplication**: Combine multiple transformations
- **Vector Transformations**: Apply matrices to vectors and points

## Requirements

- .NET 6.0 or higher
- OpenTK 4.8.2 (for rendering)

## Usage

Run the application to see a visual demonstration of matrix transformations applied to a multi-colored rectangle.

### Controls

- **Arrow Keys**: Rotate the rectangle
- **+/- Keys**: Scale the rectangle up/down
- **R**: Reset all transformations
- **ESC**: Exit the application

## How It Works

The demo creates a multi-colored rectangle and applies transformations using the custom `Matrix4x4` class. Transformations are computed using our math library, then passed to OpenGL for rendering, demonstrating the practical application of 3D mathematics in graphics programming.

## Visual Output

The rectangle displays with gradient colors:
- Red (bottom-left)
- Green (bottom-right)
- Blue (top-right)
- Yellow (top-left)

Transformations are applied in real-time as you interact with the keyboard controls.

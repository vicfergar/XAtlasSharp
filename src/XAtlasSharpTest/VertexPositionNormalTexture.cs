using System.Numerics;

namespace XAtlasSharpTest
{
    public struct VertexPositionNormalTexture
    {
        public const int Stride = sizeof(float) * 8;

        public Vector3 Position;

        public Vector3 Normal;

        public Vector2 TexCoord;

        public VertexPositionNormalTexture(Vector3 position, Vector3 normal, Vector2 texCoord)
        {
            this.Position = position;
            this.Normal = normal;
            this.TexCoord = texCoord;
        }
    }
}

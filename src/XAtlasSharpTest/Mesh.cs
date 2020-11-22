using System.Collections.Generic;
using System.Linq;

namespace XAtlasSharpTest
{
    public class Mesh
    {
        public string Name;

        public ushort[] IndexBuffer;

        public VertexPositionNormalTexture[] VertexBuffer;

        public Mesh(string name, IEnumerable<ushort> indices, IEnumerable<VertexPositionNormalTexture> vertices)
        {
            this.Name = name;
            this.IndexBuffer = indices.ToArray();
            this.VertexBuffer = vertices.ToArray();
        }
    }
}

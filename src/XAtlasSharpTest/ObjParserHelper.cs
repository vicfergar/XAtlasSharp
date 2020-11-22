using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Loaders;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace XAtlasSharpTest
{
    public static class ObjParserHelper
    {
        public static IEnumerable<Mesh> GenerateMeshes(LoadResult loadResult)
        {
            var objPositions = loadResult.Vertices.Select(v => v.ToVector()).ToArray();
            var objNormals = loadResult.Normals.Select(v => v.ToVector()).ToArray();
            var objTextures = loadResult.Textures.Select(v => v.ToVector()).ToArray();

            var meshes = new List<Mesh>();
            foreach (var group in loadResult.Groups)
            {
                var indices = new List<ushort>();
                var vertices = new List<VertexPositionNormalTexture>();

                foreach (var face in group.Faces)
                {
                    var verticesCount = vertices.Count;
                    indices.Add((ushort)(verticesCount + 2));
                    indices.Add((ushort)(verticesCount + 1));
                    indices.Add((ushort)(verticesCount + 0));

                    if (face.Count == 4)
                    {
                        indices.Add((ushort)(verticesCount + 2));
                        indices.Add((ushort)(verticesCount + 0));
                        indices.Add((ushort)(verticesCount + 3));
                    }

                    for (int i = 0; i < face.Count; i++)
                    {
                        var vertexFace = face[i];
                        var position = vertexFace.VertexIndex > 0 ? objPositions[vertexFace.VertexIndex - 1] : Vector3.Zero;
                        var normal = vertexFace.NormalIndex > 0 ? objNormals[vertexFace.NormalIndex - 1] : Vector3.Zero;
                        var texCoord = vertexFace.TextureIndex > 0 ? objTextures[vertexFace.TextureIndex - 1] : Vector2.Zero;

                        vertices.Add(new VertexPositionNormalTexture(position, normal, texCoord));
                    }
                }

                meshes.Add(new Mesh(group.Name, indices, vertices));
            }

            return meshes;
        }

        public static Vector3 ToVector(this Vertex input)
        {
            return new Vector3(input.X, input.Y, input.Z);
        }

        public static Vector3 ToVector(this Normal input)
        {
            return new Vector3(input.X, input.Y, input.Z);
        }

        public static Vector2 ToVector(this Texture input)
        {
            return new Vector2(input.X, input.Y);
        }
    }
}

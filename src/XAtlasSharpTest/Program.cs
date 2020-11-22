using ObjLoader.Loader.Loaders;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using XAtlasSharp;

namespace XAtlasSharpTest
{
    class Program
    {
        private static bool s_verbose;
        private static Stopwatch stopwatch;

        static int PrintCallback(string format)
        {
            Console.Write("\r");
            Console.Write(format);
            return 0;
        }

        static bool ProgressCallback(ProgressCategory category, int progress, IntPtr userData)
        {
            // Don't interrupt verbose printing.
            if (s_verbose)
                return true;

            if (progress == 0)
                stopwatch.Restart();

            Console.Write($"\r   {XAtlas.StringForEnum(category)} {progress}%");
            for (int i = 0; i < 10; i++)
                Console.Write(progress / ((i + 1) * 10) > 0 ? "*" : " ");
            Console.Write($" {progress}%]");

            if (progress == 100)
                Console.WriteLine($"\n      {stopwatch.Elapsed.TotalSeconds:0.00} seconds ({stopwatch.ElapsedMilliseconds} ms) elapsed");
            return true;
        }

        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: input_file.obj [options]");
                Console.WriteLine("  Options:");
                Console.WriteLine("    -verbose");
                return 1;
            }
            var filePath = args[0];
            s_verbose = (args.Length >= 2 && string.Compare(args[1], "-verbose") == 0);
            // Load object file.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Specified file not found: {filePath}");
                return 1;
            }

            Console.WriteLine($"Loading {args[0]}...");

            var objLoaderFactory = new ObjLoaderFactory();
            var materialProvider = new MaterialNullStreamProvider();
            var objLoader = objLoaderFactory.Create(materialProvider);

            LoadResult loadResult;
            using (var fileStream = File.OpenRead(args[0]))
            {
                loadResult = objLoader.Load(fileStream);
            }

            Console.WriteLine($"   {loadResult.Groups.Count} shapes");

            // Create empty atlas.
            XAtlas.SetPrint(PrintCallback, s_verbose);
            Atlas atlas = Atlas.Create();

            // Set progress callback.
            stopwatch = new Stopwatch();
            atlas.SetProgressCallback(ProgressCallback, IntPtr.Zero);

            var globalStopwatch = Stopwatch.StartNew();

            // Add meshes to atlas.
            uint totalVertices = 0, totalFaces = 0;
            var modelMeshes = ObjParserHelper.GenerateMeshes(loadResult).ToArray();
            int meshIndex = 0;
            foreach (var mesh in modelMeshes)
            {
                var indexBuffer = mesh.IndexBuffer;
                var vertexBuffer = mesh.VertexBuffer;
                var indexBufferHandle = GCHandle.Alloc(indexBuffer, GCHandleType.Pinned);
                var vertexBufferHandle = GCHandle.Alloc(vertexBuffer, GCHandleType.Pinned);

                var meshDecl = new MeshDecl();
                meshDecl.VertexCount = (uint)vertexBuffer.Length;
                meshDecl.VertexPositionData = vertexBufferHandle.AddrOfPinnedObject();
                meshDecl.VertexPositionStride = VertexPositionNormalTexture.Stride;

                //var normalElement = vertexLayout.Elements.FirstOrDefault(x => x.Semantic == ElementSemanticType.Normal);
                //if (normalElement != default)
                //{
                meshDecl.VertexNormalData = IntPtr.Add(meshDecl.VertexPositionData, sizeof(float) * 3);
                meshDecl.VertexNormalStride = VertexPositionNormalTexture.Stride;
                //}

                //var texCoordElement = vertexLayout.Elements.FirstOrDefault(x => x.Semantic == ElementSemanticType.TexCoord);
                //if (texCoordElement != default)
                //{
                meshDecl.VertexUvData = IntPtr.Add(meshDecl.VertexNormalData, sizeof(float) * 3);
                meshDecl.VertexUvStride = VertexPositionNormalTexture.Stride;
                //}

                meshDecl.IndexCount = (uint)indexBuffer.Length;
                meshDecl.IndexData = indexBufferHandle.AddrOfPinnedObject();
                meshDecl.IndexFormat = IndexFormat.UInt16;

#if !OBJ_TRIANGULATE
                //if (objPositions.Length != indices.Length / 3)
                {
                    //meshDecl.FaceVertexCount = objMesh.num_vertices.data();
                    //meshDecl.FaceCount = (uint32_t)objMesh.num_vertices.size();
                }
#endif
                var error = atlas.AddMesh(meshDecl, 1);
                indexBufferHandle.Free();
                vertexBufferHandle.Free();
                if (error != AddMeshError.Success)
                {
                    atlas.Destroy();
                    Console.WriteLine($"Error adding mesh {meshIndex} '{mesh.Name}': {XAtlas.StringForEnum(error)}\n");
                    return 1;
                }

                meshIndex++;
                totalVertices += meshDecl.VertexCount;
                if (meshDecl.FaceCount > 0)
                    totalFaces += meshDecl.FaceCount;
                else
                    totalFaces += meshDecl.IndexCount / 3; // Assume triangles if MeshDecl.FaceCount not specified.
            }

            atlas.AddMeshJoin(); // Not necessary. Only called here so geometry totals are printed after the AddMesh progress indicator.
            Console.WriteLine($"   {totalVertices} total vertices");
            Console.WriteLine($"   {totalFaces} total faces");

            // Generate atlas.
            Console.WriteLine("Generating atlas");
            atlas.Generate();
            Console.WriteLine($"   {atlas.ChartCount} charts");
            Console.WriteLine($"   {atlas.AtlasCount} atlases");
            int i;
            for (i = 0; i < atlas.AtlasCount; i++)
            {
                Console.WriteLine($"      {i}: {atlas.Utilization[i] * 100.0f:0.00}% utilization");
            }

            Console.WriteLine($"   {atlas.Width}x{atlas.Height} resolution");
            totalVertices = 0;
            i = 0;
            foreach (var mesh in atlas.Meshes)
            {
                totalVertices += mesh.VertexCount;
                // Input and output index counts always match.
                if (mesh.IndexCount != modelMeshes[i++].IndexBuffer.Length)
                {
                    throw new InvalidOperationException("Input and output index counts does not match");
                }
            }

            globalStopwatch.Stop();

            Console.WriteLine($"   {totalVertices} total vertices");
            Console.WriteLine($"{globalStopwatch.Elapsed.TotalSeconds} seconds ({globalStopwatch.ElapsedMilliseconds} ms) elapsed total");

            // Cleanup.
            atlas.Destroy();
            Console.WriteLine("Done");

            return 0;
        }
    }
}

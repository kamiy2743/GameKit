using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace GameKit.RenderGraph.Sample
{
    public sealed class SampleDrawProceduralRenderFeature : ScriptableRendererFeature
    {
        static readonly string ShaderName = "GameKit/Sample/ProceduralCube";
        static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        static readonly RenderPassEvent RenderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        static readonly Vector3 Size = new(10f, 10f, 10f);
        static readonly Color Color = Color.lightGreen;
        const int VertexCount = 36;

        Material cubeMaterial;
        RenderGraphProceduralPass pass;

        public override void Create()
        {
            var shader = Shader.Find(ShaderName);
            cubeMaterial = CoreUtils.CreateEngineMaterial(shader);

            pass = new RenderGraphProceduralPass(cubeMaterial)
            {
                renderPassEvent = RenderPassEvent,
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(pass);
        }

        protected override void Dispose(bool disposing)
        {
            CoreUtils.Destroy(cubeMaterial);
        }

        sealed class RenderGraphProceduralPass : ScriptableRenderPass
        {
            readonly Material material;

            public RenderGraphProceduralPass(Material material)
            {
                this.material = material;
            }

            public override void RecordRenderGraph(UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ContextContainer frameData)
            {
                var resourceData = frameData.Get<UniversalResourceData>();
                using var builder = renderGraph.AddRasterRenderPass<PassData>("RenderGraph Procedural Cube", out var passData);
                passData.Material = material;
                passData.Color = Color;
                passData.Matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Size);
                passData.VertexCount = VertexCount;

                builder.SetRenderAttachment(resourceData.activeColorTexture, 0);
                builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture);

                builder.SetRenderFunc(static (PassData data, RasterGraphContext context) =>
                {
                    data.Material.SetColor(BaseColorId, data.Color);
                    context.cmd.DrawProcedural(data.Matrix, data.Material, 0, MeshTopology.Triangles, data.VertexCount, 1);
                });
            }

            sealed class PassData
            {
                public Material Material;
                public Matrix4x4 Matrix;
                public Color Color;
                public int VertexCount;
            }
        }
    }
}

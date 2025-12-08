using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace GameKit.RenderGraph.Sample
{
    public sealed class SampleDrawMeshRenderFeature : ScriptableRendererFeature
    {
        static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        static readonly RenderPassEvent RenderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        static readonly Vector3 Size = new(10f, 10f, 10f);
        static readonly Color Color = Color.white;

        Mesh unitCubeMesh;
        Material cubeMaterial;
        RenderGraphCubePass pass;

        public override void Create()
        {
            unitCubeMesh = CoreUtils.CreateCubeMesh(-Vector3.one * 0.5f, Vector3.one * 0.5f);
            unitCubeMesh.name = "RenderGraphSampleUnitCube";

            var shader = Shader.Find("Universal Render Pipeline/Unlit");
            cubeMaterial = CoreUtils.CreateEngineMaterial(shader);

            pass = new RenderGraphCubePass(unitCubeMesh, cubeMaterial)
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
            CoreUtils.Destroy(unitCubeMesh);
            CoreUtils.Destroy(cubeMaterial);
        }

        sealed class RenderGraphCubePass : ScriptableRenderPass
        {
            readonly Mesh mesh;
            readonly Material material;

            public RenderGraphCubePass(Mesh mesh, Material material)
            {
                this.mesh = mesh;
                this.material = material;
            }

            public override void RecordRenderGraph(UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ContextContainer frameData)
            {
                var resourceData = frameData.Get<UniversalResourceData>();
                using var builder = renderGraph.AddRasterRenderPass<PassData>("RenderGraph Cube", out var passData);
                passData.Mesh = mesh;
                passData.Material = material;
                passData.Color = Color;
                passData.Matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Size);

                builder.SetRenderAttachment(resourceData.activeColorTexture, 0);
                builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture);

                builder.SetRenderFunc(static (PassData data, RasterGraphContext context) =>
                {
                    data.Material.SetColor(BaseColorId, data.Color);
                    context.cmd.DrawMesh(data.Mesh, data.Matrix, data.Material, 0, 0);
                });
            }

            sealed class PassData
            {
                public Mesh Mesh;
                public Material Material;
                public Matrix4x4 Matrix;
                public Color Color;
            }
        }
    }
}

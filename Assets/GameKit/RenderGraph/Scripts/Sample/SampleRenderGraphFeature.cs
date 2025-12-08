using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace GameKit.RenderGraph.Sample
{
    public sealed class SampleRenderGraphFeature : ScriptableRendererFeature
    {
        static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        [Serializable]
        public class Settings
        {
            public Vector3 size = new(10f, 10f, 10f);
            public Color color = Color.white;
            public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        }

        RenderGraphCubePass pass;
        Mesh unitCubeMesh;
        Material cubeMaterial;

        public Settings settings = new();

        public override void Create()
        {
            unitCubeMesh = CoreUtils.CreateCubeMesh(-Vector3.one * 0.5f, Vector3.one * 0.5f);
            unitCubeMesh.name = "RenderGraphSampleUnitCube";

            var shader = Shader.Find("Universal Render Pipeline/Unlit");
            cubeMaterial = CoreUtils.CreateEngineMaterial(shader);

            pass = new RenderGraphCubePass(unitCubeMesh, cubeMaterial, settings)
            {
                renderPassEvent = settings.renderPassEvent,
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            pass.UpdateSettings(settings);
            pass.renderPassEvent = settings.renderPassEvent;
            renderer.EnqueuePass(pass);
        }

        protected override void Dispose(bool disposing)
        {
            CoreUtils.Destroy(unitCubeMesh);
            CoreUtils.Destroy(cubeMaterial);
        }

        sealed class RenderGraphCubePass : ScriptableRenderPass
        {
            readonly Mesh _mesh;
            readonly Material _material;
            Vector3 _size;
            Color _color;

            sealed class PassData
            {
                public Mesh mesh;
                public Material material;
                public Matrix4x4 matrix;
                public Color color;
            }

            public RenderGraphCubePass(Mesh mesh, Material material, Settings settings)
            {
                _mesh = mesh;
                _material = material;
                UpdateSettings(settings);
            }

            public void UpdateSettings(Settings settings)
            {
                _size = settings.size;
                _color = settings.color;
            }

            public override void RecordRenderGraph(UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ContextContainer frameData)
            {
                if (_mesh == null || _material == null)
                {
                    return;
                }

                var resourceData = frameData.Get<UniversalResourceData>();
                using var builder = renderGraph.AddRasterRenderPass<PassData>("RenderGraph Cube", out var passData);
                passData.mesh = _mesh;
                passData.material = _material;
                passData.color = _color;
                passData.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, _size);

                builder.SetRenderAttachment(resourceData.activeColorTexture, 0);
                builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Write);

                builder.SetRenderFunc(static (PassData data, RasterGraphContext context) =>
                {
                    data.material.SetColor(BaseColorId, data.color);
                    context.cmd.DrawMesh(data.mesh, data.matrix, data.material, 0, 0);
                });
            }
        }
    }
}

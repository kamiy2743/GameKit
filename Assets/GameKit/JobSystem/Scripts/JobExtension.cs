using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Jobs;

namespace GameKit.JobSystem
{
    public static class JobExtension
    {
        public static async UniTask ScheduleAsync<T>(
            this T job,
            JobHandle dependsOn = default,
            CancellationToken ct = default
        )
            where T : struct, IJob
        {
            var handle = job.Schedule(dependsOn);
            await handle.WaitForAsync(ct);
        }
        
        public static async UniTask ScheduleParallelAsync<T>(
            this T job,
            int arrayLength,
            int innerLoopBatchCount,
            JobHandle dependsOn = default,
            CancellationToken ct = default
        )
            where T : struct, IJobFor
        {
            var handle = job.ScheduleParallel(arrayLength, innerLoopBatchCount, dependsOn);
            await handle.WaitForAsync(ct);
        }
        
        public static async UniTask WaitForAsync(this JobHandle handle, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                await UniTask.WaitUntil(() => handle.IsCompleted, cancellationToken: ct);
            }
            finally
            {
                handle.Complete();
            }
        }
    }
}
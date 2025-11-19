using R3;

namespace GameKit.UIFramework.Modal
{
    public interface IUniversalPopModalObservable
    {
        Observable<Unit> OnPopRequest();
    }
}
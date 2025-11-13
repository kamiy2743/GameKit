using R3;

namespace GameKit.UIFramework.Page
{
    public interface IUniversalPopPageObservable
    {
        Observable<Unit> OnPopRequest();
    }
}
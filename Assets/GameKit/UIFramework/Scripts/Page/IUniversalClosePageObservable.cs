using R3;

namespace GameKit.UIFramework.Page
{
    public interface IUniversalClosePageObservable
    {
        Observable<Unit> OnCloseRequest();
    }
}
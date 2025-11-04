namespace GameKit.UIFramework.Page
{
    public sealed class UniversalClosePageSpecification
    {
        readonly PageContainer pageContainer;

        public UniversalClosePageSpecification(PageContainer pageContainer)
        {
            this.pageContainer = pageContainer;
        }
        
        public bool Check()
        {
            if (pageContainer.IsTransitioning())
            {
                return false;
            }
            
            if (!pageContainer.GetActivePage()?.AllowUniversalClose() ?? false)
            {
                return false;
            }
            
            return true;
        }
    }
}
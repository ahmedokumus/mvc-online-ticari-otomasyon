namespace MvcOnlineTicariOtomasyon.Breadcrumb;

internal interface IHierarchyProvider
{
    int GetLevel();
    int GetLevel(string url);
}
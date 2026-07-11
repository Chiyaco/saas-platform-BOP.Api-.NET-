namespace SaaSPlatform.Web.Components.DataGrid;

public class GridColumn<T>
{
    public string Title { get; set; } = "";
    public Func<T, string> Value { get; set; } = default!;
}
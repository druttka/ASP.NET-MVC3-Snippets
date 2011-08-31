/// <summary>
/// Signals that a Controller or Action belongs in the given Area when the MVC Framework
/// might miss it despite area registration
/// </summary>
/// <remarks>Based on http://stackoverflow.com/questions/5128009/mvc-3-area-route-not-working</remarks>
/// <example>
/// <![CDATA[
///     [Area("MyArea")]
///     public class MyAreaController : Controller
///     {
///         // ...
///     }
/// ]]>
/// </example>
public class AreaAttribute : ActionFilterAttribute
{
	private readonly string _areaName;
	private const string DataTokenKey = "area";

	/// <summary>
	/// Constructs a new AreaAttribute with the given areaName
	/// </summary>
	/// <param name="areaName">The target area</param>
	public AreaAttribute(string areaName)
	{
		_areaName = areaName;
	}

	/// <summary>
	/// Forces us into the target area if MVC Framework hasn't already figured it out
	/// </summary>
	/// <param name="filterContext">The executing context</param>
	public override void OnActionExecuting(ActionExecutingContext filterContext)
	{
		base.OnActionExecuting(filterContext);

		var controllerContext = filterContext.Controller.ControllerContext;
		if (!controllerContext.RouteData.DataTokens.ContainsKey(DataTokenKey))
		{
			controllerContext.RouteData.DataTokens.Add(DataTokenKey, _areaName);
		}
	}
}
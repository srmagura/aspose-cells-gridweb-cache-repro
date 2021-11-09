# aspose-cells-gridweb-cache-repro

This repository demonstates an incompatibility between Aspose.Cells.GribWeb and
Zip Deploy a.k.a. "Run from package". This is important because Zip Deploy is
the default deployment method when deploying to an Azure App Service via the
`AzureWebApp` task in Azure Pipelines.

Zip Deployment adds an app setting `WEBSITE_RUN_FROM_PACKAGE=1` to the App
Service, which prevents modifications to the `wwwroot` directory. This causes
Aspose.Cells.GribWeb to throw an exception since it tries to create files and
directories under `wwwroot`.

This happens even if `MainWeb.SessionStorePath` is set to a directory outside of
`wwwroot`. It appears that `SessionStorePath` only affects the location of the
`acwtemp` directory, not the `acwcache` directory.

## Reproduction Steps

1. Open this solution is Visual Studio 2019 or higher.
2. Run the website locally. It displays an Excel file with an image.
3. Create an Azure App Service (Windows) through Azure Portal or Visual Studio.
4. Use Visual Studio to create a publish profile for the App Service. **Check
   "Run from package file".**
5. Use Visual Studio to publish to the App Service via the publish profile you
   created.
6. Open the Azure-hosted web app in your browser.

**Expected behavior:** The Excel file is displayed.

**Actual behavior:** An exception message is displayed:

```text
System.IO.FileNotFoundException: Could not find file 'D:\home\site\wwwroot\acwcache\836e7293-9d7e-f7b9-9b8b-06533d8cc546'.
File name: 'D:\home\site\wwwroot\acwcache\836e7293-9d7e-f7b9-9b8b-06533d8cc546'
   at System.IO.FileSystem.CreateDirectory(String fullPath, Byte[] securityDescriptor)
   at System.IO.Directory.CreateDirectory(String path)
   at    .(String , Boolean ,     , String )
   at Aspose.Cells.GridWeb.MainWeb.()
   at Aspose.Cells.GridWeb.MainWeb. ()
   at Aspose.Cells.GridWeb.MainWeb.OnPreRender()
   at Aspose.Cells.GridWeb.MainWeb.(Boolean )
   at Aspose.Cells.GridWeb.GridWeb2TagHelper.Process(TagHelperContext context, TagHelperOutput output)
   at Microsoft.AspNetCore.Razor.TagHelpers.TagHelper.ProcessAsync(TagHelperContext context, TagHelperOutput output)
   at Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner.RunAsync(TagHelperExecutionContext executionContext)
   at AspNetCore.Views_Home_Index.<ExecuteAsync>b__16_1() in C:\Projects\Compendia\aspose-cells-gridweb-cache-repro\AsposeCells\Views\Home\Index.cshtml:line 19
   at Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext.SetOutputContentAsync()
   at AspNetCore.Views_Home_Index.ExecuteAsync()
   at Microsoft.AspNetCore.Mvc.Razor.RazorView.RenderPageCoreAsync(IRazorPage page, ViewContext context)
   at Microsoft.AspNetCore.Mvc.Razor.RazorView.RenderPageAsync(IRazorPage page, ViewContext context, Boolean invokeViewStarts)
   at Microsoft.AspNetCore.Mvc.Razor.RazorView.RenderAsync(ViewContext context)
   at Microsoft.AspNetCore.Mvc.ViewFeatures.ViewExecutor.ExecuteAsync(ViewContext viewContext, String contentType, Nullable`1 statusCode)
   at Microsoft.AspNetCore.Mvc.ViewFeatures.ViewExecutor.ExecuteAsync(ViewContext viewContext, String contentType, Nullable`1 statusCode)
   at Microsoft.AspNetCore.Mvc.ViewFeatures.ViewExecutor.ExecuteAsync(ActionContext actionContext, IView view, ViewDataDictionary viewData, ITempDataDictionary tempData, String contentType, Nullable`1 statusCode)
   at Microsoft.AspNetCore.Mvc.ViewFeatures.ViewResultExecutor.ExecuteAsync(ActionContext context, ViewResult result)
   at Microsoft.AspNetCore.Mvc.ViewResult.ExecuteResultAsync(ActionContext context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResultFilterAsync>g__Awaited|29_0[TFilter,TFilterAsync](ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultFilters()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextResourceFilter>g__Awaited|24_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResourceExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeFilterPipelineAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)
   at Microsoft.AspNetCore.Session.SessionMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Session.SessionMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)
```

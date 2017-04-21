# Goal
Get metrics from the application and go through ways to analyze them

# References
https://azure.microsoft.com/en-us/services/application-insights/

https://docs.microsoft.com/en-us/azure/application-insights/app-insights-analytics

# Let's code!
## Add App Insights to the web application

Right click on the web project : add -> Application Insights Telemetry...

<img src="Media/img2.png" height="500">

Click on Start Free button

<img src="Media/img1.png" width="500">

Notice that the file ApplicationInsights.config has been added to the project. Click on it to open the node.
There are 

![img3][img3]

Note in the web.config file, you should find the following section:

  ```xml
  <httpModules>
      <add name="ApplicationInsightsWebTracking" type="Microsoft.ApplicationInsights.Web.ApplicationInsightsHttpModule, Microsoft.AI.Web"/>
  </httpModules>
  ```
Now we will add the instrumentation key int the web.config file. We do this so that we can change the key depending on the environment we deploy in. Application Insights will take the key in the Web.config file first.
Add this line in the web.config file:
  ```xml
<add key="ApplicationInsightInstrumentationKey" value="INSTRUMENTAIONKEY" />
```
The instrumentation key is in the ApplicationInsights.config, at the bottom:

```xml
<InstrumentationKey>INSTRUMENTAIONKEY</InstrumentationKey>
```

Copy and paste the Guid in the web.config file in value of the newly added parameter.

Run the applicaiton (even locally) and you can start seeing some metrics in Application Insights (either in Visual Studio or in the Azure Portal)

## Add code to track javascript calls
In file `/Views/Shared/_Layout.cshtml`, copy and paste the following javascript code immediatly befor the `</html>` tag and then copy and paste the instrumentation key.

```javascript

<script type="text/javascript">  
var appInsights=window.appInsights||function(config)
{    
	function i(config){t[config]=function(){var i=arguments;t.queue.push(function(){t[config].apply(t,i)})}}var t={config:config},u=document,e=window,o="script",s="AuthenticatedUserContext",h="start",c="stop",l="Track",a=l+"Event",v=l+"Page",y=u.createElement(o),r,f;y.src=config.url||"https://az416426.vo.msecnd.net/scripts/a/ai.0.js";u.getElementsByTagName(o)[0].parentNode.appendChild(y);try{t.cookie=u.cookie}catch(p){}for(t.queue=[],t.version="1.0",r=["Event","Exception","Metric","PageView","Trace","Dependency"];r.length;)i("track"+r.pop());return i("set"+s),i("clear"+s),i(h+a),i(c+a),i(h+v),i(c+v),i("flush"),config.disableExceptionTracking||(r="onerror",i("_"+r),f=e[r],e[r]=function(config,i,u,e,o){var s=f&&f(config,i,u,e,o);return s!==!0&&t["_"+r](config,i,u,e,o),s}),t    }(

{        instrumentationKey:"YOUR INSTRUMENTATION KEY HERE"    });          

 window.appInsights=appInsights;    appInsights.trackPageView();</script>

```

Note that this code is available in the portal (App Insight -> Getting Started -> Monitor and diagnose client side application)
![img4][img4]

Now you can view the application on IE or Chrome. Open the developper console (F12) and you will notice calls to App Insights in the network tab.  You should see requests to `https://dc.services.visualstudio.com/v2/track`which is App Insights end point to collect data.

At this point, you should be able to see some data in App Insights if you login in the Portal and look at the overview section.

## Tracking Unhandled Exceptions (request failures)
Now we will demonstrate how to track exceptions and how to log them ourselves (custom exception logging)

Add a new folder in the project `Services`
Now add a new cs file and call it `SomeService.cs`
In this file, copy and paste the definition of a custom excection `ServiceException` and the `SomeService` class:

```cs
    public class ServiceException : Exception
    {
        public ServiceException() : base("Service Exception") { }
        public ServiceException(Exception inner) : base("Service Exception", inner) { }
    }
 
    public class SomeService
    {
        public static void ThrowAnExceptionPlease()
        {
            throw new ServiceException();
        }
    }
```

Now add an empty controller to the web applicaiton and call it `ServiceController.cs`
In the `Index` method, add the following line of code:
```cs 
        Services.SomeService.ThrowAnExceptionPlease();
```
Open the related view file  `views\service\index.cshtml` and add
```cshtml
@{
    ViewBag.Title = "ViewService";
}

<h2>Hello!!!!</h2>
```

now in the layout file `views\Shared\_Layout.cshtml`, after line 37, add;
```cshtml
<li>@Html.ActionLink("Service", "Index", "Service")</li>
```

Now run the applicaiton and click on the 'Service' link on the top. You should see and exception stack.
Back to visual studio, under the ApplicationInsights.config file, click on `Search debug session telemetry' and you should see the failed request along with the details.

## Tracking Handled Exceptions
Back to the Index method of the `ServiceController`, replace the code of the method by;

```cs
            try
            {                
                Services.SomeService.ThrowAnExceptionPlease();
            }
            catch(Exception ex)
            {
                var client = new TelemetryClient();
                client.TrackException(ex);
            }
            return View();
```

Add the using line to make it build:

```cs
using Microsoft.ApplicationInsights;
```

Run the application again and click on the `Service` menu item. You should not see the exception stack at this point.

On the Azure Portal, click on Metric Explorer and then add metrics to add the exceptions. We should be able to see the details of the exceptions.

## Explore Application Map to track dependencies
Application Insights will track dependencies calls by default, though you can explicitly track dependencies using the SDK.

First add the following line at the top of the SomeService.cs file:
```cs
    using Microsoft.ApplicationInsights;
```

add the following method to the SomeService class:

```cs
        public static void SomeWorkWithDependency()
        {
            var success = false;
            var telemetry = new TelemetryClient();            
            var startTime = DateTime.UtcNow;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var dependency = new DependencyService();
                success = dependency.DoSomeWork();
            }
            finally
            {
                timer.Stop();
                telemetry.TrackDependency("DependencyService", "DoSomeWork", startTime, timer.Elapsed, success);
            }
        }
```

In the same file, add the following class:

```cs
    public class DependencyService
    {
        public bool DoSomeWork()
        {
            System.Threading.Thread.Sleep(5000);
            return true;
        }
    }

```

Now, change the code in the ServiceController file, Index method by replacing the call to `Services.SomeService.ThrowAnExceptionPlease();` by `Services.SomeService.SomeWorkWithDependency();`

Build and run the application and click on the Service menu item. You can also change the delay in the `DoSomeWork` method (make it very low for instance), rebuild and run again (do not forget to click on the Service menu item).

Go in the Portal and click on the App Insights instance then Application Map. You should be able to see the calls the newly created method.

## Track custom events in the function
Open the function that we created in the step 3
First, we need to add a dependency to Application Insights SDK.

We must add a new file to the function; `project.json` and copy the follwoing content in it.

```json
{
  "frameworks": {
    "net46":{
      "dependencies": {
        "Microsoft.ApplicationInsights": "2.2.0"
      }
    }
  }
}
```

Now, in the `run.csx` file, add the following method and do not forget to put in your instrumentation key.

```cs
public static TelemetryClient AppInsight()

{
     
	var client = new TelemetryClient();
     
	client.InstrumentationKey = "INSTRUMENTATIONKEY";
     
	return client;

}
```

At the end of the main method, add the call to add an event:
```cs
AppInsight().TrackEvent("MessagePost"); 
```

Now, let's send a message using the web app to trigger the function and check in the metrics explorer that we actually logged a new custom event called `MessagePost`

[img1]: Media/img1.png
[img3]: Media/img3.png 
[img4]: Media/img4.png

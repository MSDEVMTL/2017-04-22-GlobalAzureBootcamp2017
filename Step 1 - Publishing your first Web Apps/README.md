# Goal
For this part of the bootcamp, our attendees will create a shiny new website in Visual Studio. We'll add a form to capture a message submitted by the user. Finally, they'll deploy to Azure App Service so the entire world can marvel at their amazing websites!

# Reference
https://www.asp.net/mvc


# Let's code!
## Open website
Fire up Visual Studio. Click `File -> Open  -> Project/Solution` and navigate to the supplied solution in Step 0.

![img1][img1]

### The Model

In the `/Models` folder, add a new class called `QueueMessageModel.cs`:

```cs
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class QueueMessageModel
    {
        [Required]
        public string Text { get; set; }
    }
}
```

### The View

Under the `/Views` folder, create a `Queue` folder [Add -> New Scaffolded item... -> MVC 5 View]. Add a new view called `CreateMessage`. Use the `Create` template, and select the model we just created `QueueMessageModel`. Click `Add` and then open the view for editing.

![img10][img10]

The only edit we need to make is to remove the "Back to List" `ActionLink` near the bottom of the page.

```html
<div>
    @Html.ActionLink("Back to List", "Index")
</div>
```

### The Layout

In file `/Views/Shared/_Layout.cshtml`, uncomment the Create Message link. The line should  look like this:

`<li>@Html.ActionLink("Create message", "CreateMessage", "Queue")</li>`

### The Controller

Under the `/Controllers` folder, add a new empty controller and name it `QueueController`. We'll create two actions to handle the `GET` and `POST` requests for our message form:

```cs
using System;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class QueueController : Controller
    {
        // GET: Queue/CreateMessage
        public ActionResult CreateMessage()
        {
            return View();
        }

        // POST: Queue/CreateMessage
        [HttpPost]
        public ActionResult CreateMessage(QueueMessageModel message)
        {
            if (ModelState.IsValid)
            {
                // TODO: Insert add message to queue logic here

                return RedirectToAction("Index", "Home");
            }

            return View(message);
        }
    }
}
```
### Build and Run!

Hit F5 and PROFIT!!!

## Deploying to Azure

There's no point in building a glorious form like this unless you can show it off. We'll now do a deployment to an Azure App Service.

### Visual Studio Publish

The easiest way to deploy is by having Visual Studio publish it for you. During the course of the publish, you'll be asked for your credentials, and you'll have to fill in some additional deployment details.

Start by right-clicking on the web project and clicking `Publish`. If you haven't logged in with your Microsoft account, you'll be asked for your credentials now. Once you are logged in, you should be presented with this screen:

![img4][img4]

Choose your subscription, and click `New` to setup your App Service:

![img5][img5]

The Web App Name field will be pre-populated with a globally unique name. Unless a Resource Group and App Service Plan have already been created under the subscription, they will need to be added now. Once all the fields have been filled in, click `Create` and our App Service will be provisioned.

Once provisioning is complete, our publishing profile is complete and the Publish dialog will appear to guide us through the rest of the process:

![img6][img6]

At this point we should be able to click Publish, and wait a few minutes for the deploy to complete. We can keep an eye on the Output window to check the status. When the deployment is complete, our browser should open a new tab and display our cloud-powered website!

![img7][img7]

## Check out the portal

Point a browser to https://portal.azure.com. Click on the Hamburger button and select `All resources` from the side menu. The new App Service should show up:

![img8][img8]

Clicking on the App Service will take us to a screen where you can manage and monitor your website.

![img9][img9]

## Addendum

I ran into a couple issues during this deployment. First, stale credentials can prevent subscriptions from appearing when doing a publish. Follow these instruction to remedy: http://stackoverflow.com/questions/24507589/visual-studio-not-finding-my-azure-subscriptions

I also ran into this error during deployment:

```
Warning : Retrying the sync because a socket error (10054) occurred
Retrying operation 'Serialization' on object sitemanifest (sourcePath). Attempt 1 of 10.
```

This issue was resolved by adding an inbound rule to the firewall on port 8172 (TCP)

# End


[img1]: Media/img1.png "New Project"
[img4]: Media/img4.png "Create new App Service"
[img5]: Media/img5.png "Add App Service details"
[img6]: Media/img6.png "Publish website"
[img7]: Media/img7.png "Deployed website in browser"
[img8]: Media/img8.png "Azure Resources screen"
[img9]: Media/img9.png "Web app management screen"
[img10]: Media/img10.png "Add a view"

Goal
======

Create an Azure Function that will be triggered when a new message is added to an Azure storage queue. 
Validate in the debug window that the text for the message is the same as written in the Website textbox.


Reference
=========

- https://docs.microsoft.com/en-us/azure/azure-functions/


Let's code!
===========

Create an Azure Function Domain
-------------------------------

From portal.azure.com, click the big green "+" in the top left corner. Search for "Function", and select **Function App**. Click the create button will open a form to specify the information related to your Function App "domainname".

![SearchFunctionApp][SearchFunctionApp]

- App Name: That the name of the domain, where all your functions will be regrouped. This name must be unique through the web.
- Subscription: The subscription where the Function App will be. 
- Ressource Group: Select the same Ressource Group used for the previous components.
- Hosting plan: Select Consomption Plan.
- Location: Select the same location used for the previous components.
- Create a new Storage Account. Give it a name like [yourname]functions

![CreateFunctionApp][CreateFunctionApp]

Click the create button.

Create The Data processing Azure Function 
-----------------------------------------

1. Click the "+" sign to add a new Function App.
1. Select the Scenario Data processing
1. Select C#
1. Click the *Create this function* button.

![CreateDataFunction][CreateDataFunction]

(Because it's our first function, the portal will do a little tour of the interface)


Configure the Function App
--------------------------

1. From the left section, select **Integrate**.
1. Click on the *New* on the side of *Storage account connection*. 
1. Select the Storage account connection used in Step 2.
1. Enter `my-gab-queue` or the name of the queue created in the previous step. 
1. Click the Save button to keep your changes.

![SetQueueStorage][SetQueueStorage]


Test the Function App
--------------------------

To test our function press the Run button. Go back in your WebApp and save some text (ex: "Hello GLobal Azure Bootcamp!").
The function will automatically get triggered and you should see your message in the logs.

Now let's get the comment inside our message. Copy-Paste the code from the snippet `function_QueueTrigger_final.txt` inside your function.

The `#r` command are to add references to library we need that are already available in Azure. Save your changes.

Clear the Logs windows by clicking the Clear button, and go back save another comment in the WebApp. You should have something similar to that.

![Result][Result]


Bonus
=====

A lot of people think a cloud solution is 100% in the cloud, but most of the actual solution still have some part on premises. In this bonus section, we will create a very simple console application to push a message in our Azure storage queue, and our Function will react.

New Project
-----------

First, Open a new Visual Studio window. Create a new Console App project. 

Nuget Package
-------------

From the Code snippets `Bonus_Console_App.md` section `Nuget Package Required`, execute in the Package Manager Console. From the top screen menu, select Tools > NuGet Package Manager > Package Manager Console. Copy-Paste one by one the Install command.  That will install what's missing for our application.

- Microsoft Azure Storage Client Library for .NET: This package provides programmatic access to data resources in your storage account.
- Microsoft Azure Configuration Manager library for .NET: This package provides a class for parsing a connection string in a configuration file, regardless of where your application is running.

App.config
----------

Now, we need to specify the connectionstring so our application can connect to the Azure storage queue. From the portal.azure.com, select the Storage Account created earlier. In the left panel click the Access keys option.  This will open a new blade where the connectionstring will be available.

![connectionstring][connectionstring]

Use this connectionstring to customize the snippet (`Bonus_Console_App.md` section `App.config`) App.config and copy-paste that new version in the App.config file.

Console App
-----------

Finally, use the snippet `Bonus_Console_App.md` section `Console App to create Message Queue` to modify the Main function of our application. The code should look familiar since it the same use in step 2 inside our Web Application.

Voila. By executing your console application you will add a message in the queue.  Look in the Azure Function logs to see you message text.


[SearchFunctionApp]: Media/SearchFunctionApp.png "Search Function App"
[CreateFunctionApp]: Media/CreateFunctionApp.png "Create a Function App"
[CreateDataFunction]: Media/CreateDataFunction.png "Create Data processing Function"
[SetQueueStorage]: Media/SetQueueStorage.png "Set Queue Storage"
[Result]: Media/Result.png "See Logs"
[connectionstring]: Media/connectionstring.png "Connectionstring available in the portal"
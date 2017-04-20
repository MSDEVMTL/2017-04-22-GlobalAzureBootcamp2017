# Goal
Preserve the sentiment analysis obtained in Step4 and persist it in a DocumentDB. We will also add an additional web page to show the results of our previously processed messages. This will show the basic usage of DocumentDB using the .NET SDK.

# Reference
* [DocumentDB documentation](https://docs.microsoft.com/en-ca/azure/documentdb/)
* [DocumentDB bindings with Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-documentdb)

# Some background
Head to https://docs.microsoft.com/en-us/azure/documentdb/documentdb-get-started for a simple "getting started".

# Let's code!
## Add the DocumentDB
In the Azure portal, click on new and search for "DocumentDB". 

![alt text][img1]

Create a DocumentDB and place it within your existing resource group

![alt text][img2]

Copy the PRIMARY KEY and URI and save them for later

![alt text][img3]

Select the "Add Collection" button accessible from the overview pane of the DocumentDB.

![alt text][img4]

Create the collection and database. Make sure that you used these names as these are reused later on. Also, select the smaller size configuration to minimize costs.

![alt text][img5]

## Update the Function's settings
Return to the function that was created in the previous step. Click on the "Integrate" section and then create a new output (top right corner). Select the Azure DocumentDB Document output.

![alt text][img6]
![alt text][img7]

Configure the output as shown here. In the DocumentDB account connection field, you will have to select "New" to create a connection with your previously created DocumentDB account.

![alt text][img8]

## Create the sentiment analysis model
Ask the attendees to open the file SentimentAnalysisModel.txt.txt in the Code Snippets/Function folder.  Copy the content outside of the function definition to use it as our output.

## Update the signature of the Function
Ask the attendees to open the file FunctionSignature.txt in the Code Snippets/Function folder.  Add this parameters to the signature of the function.

## Output our sentiment analysis model
Ask the attendees to open the file OutputModel.txt in the Code Snippets/Function folder.  Add this at the very and of the main function.

## Update the AppSettings of the WebApp

### If running locally
Ask the attendees to copy the file Web.Deployment.config in the Code Snippets/WebApp folder to the root of the WebApp project and add it to the project in Visual Studio.

* Update the Web.Config to reference that file
```
<appSettings file="Web.Deployment.config">
    ...
</appSettings>
```
* Update the Web.Deployment.config file with your DocumentDB URI and key for example:
```
<appSettings>
  <add key="DocumentDBUri" value="https://dchdocdb.documents.azure.com:443/" />
  <add key="DocumentDBKey" value="myveryprivatekeygoeshere==" />
</appSettings>
```
### If running on Azure

Ask the attendees to go the Application Settings of the WebApp which was deployed previously.
![alt text][img9]

Ask the attendeeds to Update the settings to include the DocumentDB URI and key.
![alt text][img10]

## Update the WebApp to add our new view

* In Visual Studio, ask the attendees to create a Sentiment folder under the View folder and copy the file ViewSentimentAnalysis.cshtm from the Code Snippets/WebApp folder

* In Visual Studio, ask the attendees to copy the file SentimentAnalysisModel.cs from the Code Snippets/WebApp folder to the Models folder

* In Visual Studio, ask the attendees to copy the file SentimentController.cs from the Code Snippets/WebApp folder to the Controllers folder

* In Visual Studio, ask the attendees to create a Repositories folder at the root of the project and copy the file SentimentAnalysisRepository.cs from the Code Snippets/WebApp folder

* Ask the attendees to update the file "Views\Shared\_Layout.cshtml" to add a reference to our new View. 

```
<li>@Html.ActionLink("Home", "Index", "Home")</li>
<li>@Html.ActionLink("Create message", "CreateMessage", "Queue")</li>
```
Becomes:
```
<li>@Html.ActionLink("Home", "Index", "Home")</li>
<li>@Html.ActionLink("Create message", "CreateMessage", "Queue")</li>
<li>@Html.ActionLink("Sentiment analysis", "ViewSentimentAnalysis", "Sentiment")</li>
```

Load your web app and navigate to the new page to view your sentiment analysis results!

#End


[img1]: Media/1-SearchDocDB.PNG "Search for DocumentDB"
[img2]: Media/2-CreateDB.PNG "Create the DocumentDB"
[img3]: Media/3-ObtainKeys.PNG "Obtain the key and url"
[img4]: Media/4-NewCollection.PNG "Select Add Collection"
[img5]: Media/5-CreateCollectionAndDB.PNG "Create the new collection and database"
[img6]: Media/6-NewOutput.PNG "Add a new output binding"
[img7]: Media/7-NewDocumentDBOutput.PNG "Select DocumentDB"
[img8]: Media/8-ConfigureOutputBinding.PNG "Configure the binding"
[img9]: Media/9-AppSettingsOnWebApp.PNG "Go to the App Settings on the Web App"
[img10]: Media/10-UpdateAppSettingsOnWebApp.PNG "Add the DocumentDB URI and Key to the App Settings"


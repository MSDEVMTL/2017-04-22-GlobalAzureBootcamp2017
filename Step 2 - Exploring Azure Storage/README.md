# Goal
In this tutorial, we will capture text that is entered in a web form. Next we will encapsulate the text in a message that is then be submitted to an Azure Storage Queue. This way, we can accept an unpredictable amount of messages. Each message then will be processed asynchronously for sentiment analysis.  

# Reference
* [Azure Storage documentation](https://azure.microsoft.com/en-us/services/storage/)
* [Get started with Azure Queue storage using .NET](https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-queues)
* [Azure Storage Explorer](http://storageexplorer.com/)

# Nuget Package Required

Install-Package Microsoft.WindowsAzure.ConfigurationManager

Install-Package WindowsAzure.Storage

Install-Package Newtonsoft.Json
	
# Configure your storage connection string

To configure your connection string, open the web.config file from Solution Explorer in Visual Studio. Add the contents of the <appSettings> element shown below. Replace account-name with the name of your storage account, and account-key with your account access key:

```xml
<add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=account-name;AccountKey=account-key" />
```
To target the storage emulator, you can use a shortcut that maps to the well-known account name and key. In that case, your connection string setting is:

```xml
<add key="StorageConnectionString" value="UseDevelopmentStorage=true;" />
```
# Locate QueueController.cs

Locate QueueController.cs.  This is where we will add our code to save the text to a queue.

# Add using directives

Add the following using directives to the top of the QueueController.cs file:

```csharp
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue; // Namespace for Queue storage types
using Newtonsoft.Json; 
```
# Parse the connection string
The Microsoft Azure Configuration Manager Library for .NET provides a class for parsing a connection string from a configuration file. The CloudConfigurationManager class parses configuration settings regardless of whether the client application is running on the desktop, on a mobile device, in an Azure virtual machine, or in an Azure cloud service. 

In the CreateMessage method add the following statement: 

```csharp
// Retrieve storage account from connection string.
CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
```
# Create the Queue service client

The CloudQueueClient class enables you to retrieve queues stored in Queue storage. Here's one way to create the service client:

```csharp
// Create the queue client.
CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
```

# Make sure that the queue exist

```csharp
// Retrieve a reference to a queue.
CloudQueue queue = queueClient.GetQueueReference("my-gab-queue");

// Create the queue if it doesn't already exist
queue.CreateIfNotExists();
```
# Convert the message to a CloudQueueMessage object
```csharp
// Convert the message to a CloudQueueMessage object
var messageAsJson = JsonConvert.SerializeObject(message);

var cloudQueueMessage = new CloudQueueMessage(messageAsJson);
```
# Add the message to the queue.
```csharp
queue.AddMessage(cloudQueueMessage);
```
# Final result

```csharp
// POST: Queue/CreateMessage
[HttpPost]
public ActionResult CreateMessage(QueueMessageModel message)
{
    if (ModelState.IsValid)
    {
        // Retrieve storage account from connection string.
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

        // Create the queue client.
        CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

        // Retrieve a reference to a queue.
        CloudQueue queue = queueClient.GetQueueReference("my-gab-queue");

        // Create the queue if it doesn't already exist
        queue.CreateIfNotExists();

        // Convert the message to a CloudQueueMessage object
        var messageAsJson = JsonConvert.SerializeObject(message);

        var cloudQueueMessage = new CloudQueueMessage(messageAsJson);

        // Create a message and add it to the queue.
        queue.AddMessage(cloudQueueMessage);

        return RedirectToAction("Index", "Home");
    }

    return View(message);
}
```

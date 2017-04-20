### Console App to create Message Queue

        static void Main(string[] args)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            var queue = queueClient.GetQueueReference("my-gab-queue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            var msg = new QueueMessageModel { Text = "Hello GLobal Azure Bootcamp!" };

            // Convert the message to a CloudQueueMessage object
            var messageAsJson = JsonConvert.SerializeObject(msg);

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(messageAsJson);
            queue.AddMessage(message);
        }


### App.config

    <configuration>
        <startup> 
            <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
        </startup>
    <appSettings>
        <add key="StorageConnectionString" value="ConnectionStringFromAzurePortal" />
    </appSettings>
    </configuration>


# Nuget Package Required

    Install-Package Microsoft.WindowsAzure.ConfigurationManager

    Install-Package WindowsAzure.Storage


# Goal
Analyze the sentiment of the text entered by the user to see if it’s positive or negative by sending it to a REST API that will analyze it and also detect the language used by the user.  For doing that, we'll modify the function already created by calling the Text Analytics API of the Microsoft Cognitive Services.
We'll be making two API calls.  The first one will return the language of the sentence entered by the user and the second call will return the sentiment of the sentence as a number between 0 and 1 (0 meaning negatitve and 1 meaning positive).

Have the audience type what they feel about the local team making it to this year's finals.  This should be fun  :-)

# Reference
https://www.microsoft.com/cognitive-services

# Some background
Head to https://www.microsoft.com/cognitive-services/en-us/text-analytics-api
Use the demo in the middle of the page to demonstrate the use of the API.  Make sure you show the JSON being returned.  This is a great way for the attendees to visualize what will be called and what will be returned by the API.

# Let's code!
## Add the Text Analytics API
In the Azure portal, click on new and search for "Cognitive". 

![alt text][img1]

Create a Cognitive Service APIs by making sure to select the Text Analytics API in the API type dropdown.

![alt text][img2]

Copy the API key.

![alt text][img5]

## Add the API key to the Function's App Settings
Return to the function that was created in the previous step.  Click on *Function app Settings* in the lower left corner.

![alt text][img3]

Create a new App Settings key called *textAnalysisApiKey* and paste the Api key.  Make sure to click on the Save button.

![alt text][img4]

## Add the required usings
Ask the attendees to open the file 1.txt in the Code Snippets folder.  Copy an paste the code in the function editor.

## Add the detect language code
Ask the attendees to open the file 2.txt in the Code Snippets folder.  Modify the Run task to add code to detect the language that the sentence is written in.
Ask the attendees to open the file 3.txt in the Code Snippets folder.  Add the DetectLanguage() function below the Run task.

## Add the detect sentiment code
Ask the attendees to open the file 4.txt in the Code Snippets folder.  Add the DetectSentiment() function below the DetectLanguage() function.
In the Run task, add a call to the DectectSentiment() function below the call to the DetectLanguage() function.
```
var _sentiment = await DetectSentiment(comment, BaseUrl, AccountKey);
log.Info("Sentiment: " + _sentiment);
```
#End


[img1]: https://raw.githubusercontent.com/alainvezina/GlobalAzureBootcamp/master/2017/Step%204%20-%20Using%20the%20Cognitive%20Services%20APIs/Media/2017-04-07%2013_33_47-New%20-%20Microsoft%20Azure.png "Search for Cognitive"
[img2]: https://github.com/alainvezina/GlobalAzureBootcamp/blob/master/2017/Step%204%20-%20Using%20the%20Cognitive%20Services%20APIs/Media/2017-04-07%2013_35_29-Create%20-%20Microsoft%20Azure.png?raw=true "Specify Text Analytics"
[img3]: https://github.com/alainvezina/GlobalAzureBootcamp/blob/master/2017/Step%204%20-%20Using%20the%20Cognitive%20Services%20APIs/Media/2017-04-07%2013_38_33-testCognitiveApi%20-%20Microsoft%20Azure.png?raw=true "Click on Function app Settings"
[img4]: https://github.com/alainvezina/GlobalAzureBootcamp/blob/master/2017/Step%204%20-%20Using%20the%20Cognitive%20Services%20APIs/Media/2017-04-07%2013_42_53-Application%20settings%20-%20Microsoft%20Azure.png?raw=true "New App Settings key"
[img5]: https://github.com/alainvezina/GlobalAzureBootcamp/blob/master/2017/Step%204%20-%20Using%20the%20Cognitive%20Services%20APIs/Media/2017-04-07%2014_19_11-Manage%20keys%20-%20Microsoft%20Azure.png?raw=true "API key"

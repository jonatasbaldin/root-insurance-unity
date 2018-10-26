# Root Insurance Unity
Unity scripts for the [Root Insurance API](https://app.root.co.za/docs/insurance/api). Only supports the Gadget module at this moment.

These scripts are API wrappers, returning an object each. By default, they just output the object to the console, so you need to adapt them for your use case.

### Installing and Using
* Go to the [releases](https://github.com/jonatasbaldin/root-insurance-unity/releases) and download the latest version (`.unitypackage` file).
![Download Latest Version](https://i.imgur.com/523ObR3.png)

* Double click the file to import the scripts into your project. After imported, the scripts will be available at `Assets > RootAPIGadgets` folder.
![Scripts available at Assets in Unity](https://i.imgur.com/ePndUl0.png)

* Open the `Auth.cs` file and add your Root API Key to the `APIKey` variable.
```csharp
string APIKey = "your Root api key here";
```

* To demonstrate the script usage, let's add it to a button! First of all create a new Canvas in the menu `Game Object > UI > Canvas`.
![Adding a Canvas](https://i.imgur.com/LuhdTLn.png)

* In the Hierarchy menu, add a Button inside the Canvas by clicking right button, `UI > Button`. The button will appear in the screen.
![Adding a Button](https://i.imgur.com/eVCo8Lt.png)

* Select the Button and Drag and Drop the `Quotes` file to its script area.
![Adding Quotes script to button](https://i.imgur.com/lCCbpF2.png)

* Then drag and drop the Button from the Hierarchy menu its own `On Click()` area on the Inspector menu. After that, select the `Quotes.GetQuote` in the function menu and add a `Gadget` name, such as `iPhone 5S`.
![Add Function to Button](https://i.imgur.com/Kt6grlX.png)

* Now it's time to test! Select the Console menu at the bottom and hit the Play button at the top. When the game starts to run, click on the Button. You'll see in the console the Quotes we got from the Root API!
![Testing the Script](https://i.imgur.com/RumppOk.png)

* You'll notice that the output is a JSON, which is related to the `Debug.Log(JsonUtility.ToJson(quote));` line from the `Quotes.cs` file. Now you can use the data from this `quote`, such as `quote.base_premium` to build your project!

* Finally, all the scripts works in the same way, by being attached to objects and outputting JSON to the console. Go ahead and edit them to fit your needs :)

### License
[MIT](https://github.com/jonatasbaldin/root-insurance-unity/blob/master/LICENSE).

# Lincoln Tourism Chatbot
Jan - May 2016

A chatbot which uses Skype to give tourism information to people visiting Lincoln. It is written in C# using Windows Forms and was submitted for the Group Project module for Computer Science at the University of Lincoln.

The bot can give the following information:
* Directions to local landmarks, such as the Cathedral and Castle
* Information about local bars, restaurants, clubs, places to stay, etc.
* Opening times and directions (using Google API)

The bot uses AIML (Artificial Intelligence Markup Language) to process the input and generate a response. The main goal of the project was to make an AI which seemed real, so features such as a delay depending on the length of reply and spelling correction were added.

## Installation

To use the bot, you will need Visual Studio and at least 2 different Skype accounts. Launch the sln file, run the program and login with your Skype credentials. You may need to login with an "old type" Skype account (one with a username and password) instead of a Microsoft account. Once logged in, the bot will automatically respond to any incoming messages.

### Dependencies
The project requires a few DLL files and AIML files which are included in the project.
* `AIMLbot.dll` - For processing the AIML files - available on [SourceForge](http://aimlbot.sourceforge.net/) (Source is slightly modified to allow additional features)
* `Skype4Sharp.dll` - For communication with Skype - see [the GitHub repo](https://github.com/lin-e/Skype4Sharp)
* `Newtonsoft.Json.dll` - Needed by Skype DLL
* AIML and config files (in `aiml\` and `config\`) - Holds the chatbot responses and config info

### Old Version

Since submission the method used to connect to Skype was broken. To see the submitted version, see the [Skype4COM branch](https://github.com/NevilleKing/Lincoln-Tourism-Chatbot/tree/Skype4COM), although you will need an old version of the Skype client.

## Team Members
* Callum Dixon
* Charlie Volland-Butler
* Jacob Ellis
* James Middleton
* Neville King
* Nikita Dmitriev
* Toby Field
* Tom Garton

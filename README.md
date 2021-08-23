# CharacterManager
Work in-progress Blazor Electron .NET app for the Wrath &amp; Glory table top RPG.

I'll add a better README eventually. But here's a short summary:

This is a desktop app that has a local SQLite database. The user creates and saves info about their character in the local SQLite database. 
All models are stored as seralized JSON so that they can be easily synced with a .NET API that I'm hosting in Azure that also uses a SQLite database. 
With everything being synced in Azure, users can see changes that each other make.

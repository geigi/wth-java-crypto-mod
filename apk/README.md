# What\_the\_Hack Android Mod Template

This repository contains a template to create an addon app for What_the_Hack.
An addon app contains one or more Mods for the base game. There is nearly no other functionality to this app.

# How does it work
Unity cannot access provided files by a native Android app. So this is a bit of a workaround but it's working very well. 

The base game searches for apps that export an `ContentProvider` containing the name `WTHModProvider`. This happens on startup before the user can interact with the app. For new found apps all of it's asset content (in our case the `Mods` directory) will be copied to `Android/data/com.github.geigi.wth/files/Mods/`. To ensure that Mods cannot override each other each app gets it's own directory.

For each mod the apk version number is saved. When the installed app version differs from the one saved in the directory the mod will be reinstalled (removed and freshly copied). If an app is uninstalled the copied content of this app is also going to be removed from the `Mods` directory.

# How to get started
**Important**: Every App needs their own unique name and `ContentProvider` name, otherwise the user won't be able to install it.

- Open the project in `Android Studio`
- Right-click on the project `com.github.geigi.wth_example_mod_apk` and select `Refactor > Rename`
- Select `rename package`
- Make sure the first line in `WTHModProvider` contains your new package name
- Check your `AndroidManifest.xml` for the new package name. Also have a look at the `provider` section which should also contain the new name
- Copy the exported `Mods` directory with your mod to `app/src/main/assets/`
- Build the apk, install it and run What_the_Hack!#
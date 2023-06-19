# Onboarding
Read the onboarding guids for [Server](Server/Onboarding.md) and [Client](Client/Onboarding.md) for detailed instructions

# Architecture

Our main alt:V resource is called core and split into a server and client part.  
The server part is written in C# using the [alt:V C# module](https://github.com/FabianTerhorst/coreclr-module)  
while the client part is written in Typescript using the [alt:V JS module](https://github.com/altmp/altv-types)
and [SWC](https://swc.rs/) to transpile to Javascript.

The build job of both of these parts copy the built resource into the resource folder that is used by alt:V.

## Overlapping Types and Models

Types that are used in server and client should be placed in the shared project/folder for easier identification of such types.  
Due to the difference in script language those types must be updated manually in both projects. (Until a build job is written for shared stuff)  
Shared Naming is done with c# conventions in both projects!
Shared can be types, event names, models or enums. Some utility classes and functions can be written on both sites to have a similar behavior. 
In this case they should be placed in shared as well.

## Webviews

There are internal Webviews that will be deployed as resource by the [InteralUI's build process](https://github.com/RudelRP/InternalUI) or 
by a localhosted dev version.  
Internal Webviews include most of the UI that is not available through the browser (e.g. Inventory, HUD, ...).  
External Webviews include every UI page that must be available through the browser (e.g. Faction Panels). These webviews are hosted on the server
and developed seperated from the internal UI. 
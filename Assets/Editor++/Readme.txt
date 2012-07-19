Thank you for downloading Enhanced Editor++!

If you have any comments, questions, suggestions (including new features), or bug reports, please email me at walt@waltdestler.com. I appreciate your feedback!

Regardless of how you like Enhanced Editor++, please leave a review! Both positive and negative reviews are appreciated. (If you're having a particular problem, please email me and give me a chance to fix it or offer you a refund.)

The website for Enhanced Editor++ is at http://www.waltdestler.com/editorplusplus

------------
INTRODUCTION

Enhanced Editor++ is a powerful extension that fixes some of the most annoying issues and ommisions of the out-of-the-box Unity3D editor. As of the current version, there are five main features:

1. Transform Copy & Paste - Allows copy & paste of a game object's position, rotation, and scale, using either local or global coordinates. Position, rotation, and scale can either be pasted all at once or individually.

2. Component Copy & Paste - Allows copy & paste of entire components at once, and works with both built-in components and custom scripts. An advanced "Clipboard" window allows you to select which individual variables will be pasted and even allows you to alter the values of those variables before they are pasted.

3. The Rememberer - Solves one of the most annoying aspects of the Unity3D editor, which is that any changes made to an object while the game is being played are forgotten as soon as the game stops. The Remember solves that by allowing you to "remember" the state of any object while playing and then "apply" the remembered state of those objects after the game is stopped.

4. Usages Finder - Quickly find all usages of any Unity object, component, prefab, script, or asset by other objects in either the scene or the Assets folder.

5. Component Reorderer - This is an experimental feature currently in BETA that allows you to reorder the components on a game object.

Below you will find detailed instructions describing how to use each feature.

---------------
VERSION CHANGES

2.6
- Unity 3.5 components, such as the new particle system, are now fully supported.
- Added feature to right-click on an object in the inspector and remember it.
- Added feature to copy multiple components of the same type.
- Menu commands are now stored in an easily-modifyable source file.
- Removed the Inspector Batching and Import Settings Copier features because they are now native features of Unity3D 3.5.
- Per Unity's request moved menu items to sub-items of existing menus:
  - Transform Copy & Paste commands are now located at the bottom of the Edit menu.
  - Find Usages commands are now located at the bottom of the Assets menu.
  - The Create Empty Child command is now located at the bottom of the GameObject menu.
2.5
- Removed most source code files in favor of a single compiled .dll file. This should eliminate bugs with conflicting filenames. The old source code files will be automatically removed if detected.
- Added preliminary support for most new Unity 3.5 components. Not all variables of all components are supported yet. Full support will come shortly after Unity 3.5 is officially released.
2.4
- Fixed bug where batch modifications to prefabs sometimes were not saved.
2.3
- Added "Select All" and "Select None" buttons to the Clipboard window.
2.2
- Added feature to quickly find all usages of any Unity object, component, script, or asset by other objects in either the scene or the Assets folder.
2.1
- The Import Settings Copier for textures now properly copies platform-specific settings.
2.0
- Can right click on texture, model, and audio importers to copy import settings en masse to all selected textures, models, and audio clips.
- Added Component Reorderer.
- Added a "Paste New" button to each component in the Clipboard that adds a new component instead of replacing the values in an existing component.
- Added support for many previously-unsupported variables of built-in Unity components.
- Added batching support for most types of assets.
- Remembering of materials and meshes is now disabled by default. They can be re-enabled by selecting a check box in the Rememberer window.
- Miscellaneous bug fixes.
- The Batcher no longer has its own window that has to be open for batching to work. Simple use the new Batcher menu.
1.11
- Updated to support Unity 3.4.
1.10
- Fixed a bug when applying remembered variables.
1.9
- Fixed a bug where arrays weren't properly batched.
1.8
- Fixed an issue where switching scenes while playing caused remembered objects to be forgotten.
1.7
- Unity 3.2's Rigidbody.constraints property is now handled.
1.6
- Fixed a bug preventing build from working on mobile platforms.
1.5
- Fixed a bug where some custom components weren't being properly "remembered".
1.4
- Fixed an exception bug with the Batcher.
- The Batcher will now remember its enabled/disabled setting.
- User interface for the Batcher moved to its own window instead of a menu.
- Added a Remember Transform button to the Rememberer.
- Remembering, copying, and batching Animations now works properly.
- Extendable via static attributed methods.
1.3
- Fixed a few issues related to undo functionality.
1.2
- Added a Readme.txt file with detailed instructions.
1.1
- An object's "active" property is now remembered by the Remeberer and is now also batchable.
1.0
- Initial version.

------------
KNOWN ISSUES

- A few variables of a couple built-in components cannot be remembered, batched, or copy-and-pasted. This is because they are not exposed by Unity to scripting.
- Copying import settings of Materials, GuiSkins, and Cubemaps are not supported due to limitations and bugs in Unity.
- If you switch scenes while playing the game, and then "remember" an object in that new scene, it will be impossible to apply changes made to that object after the game has stopped.
- The Component Reorderer is currently in BETA has has some known deficiencies. Please see "COMPONENT REORDERER" below.

----------------------
TRANSFORM COPY & PASTE

Transform Copy & Paste allows you to copy & paste a game object's position, rotation, and/or scale, using either local or global coordinates. Position, rotation, and scale can either be pasted either all at once or individually.

*NEW:* Transform Copy & Paste commands are now located at the bottom of the edit menu.

How to use Transform Copy & Paste:
1. Select a single object whose global or local position, rotation, and/or scale you want to copy.
2. Click the Edit menu and then click Copy Transform. The selected object's local position, global position, local rotation, global rotation, and local scale will each be copied into a special "transform clipboard".
3. Select one or more objects into which you want to paste any of those copied values.
4. Click the Edit menu and then click one of the Paste commands, as detailed here:
   - Paste Local Transform: Pastes the copied local position, local rotation, and local scale into all of the selected objects.
   - Paste Global Transform: Pastes the copied global position and global rotation into all of the selected objects.
   - Paste Local Position: Pastes the copied local position into all of the selected objects.
   - Paste Global Position: Pastes the copied global position into all of the selected objects.
   - Paste Local Rotation: Pastes the copied local rotation into all of the selected objects.
   - Paste Global Rotation: Pastes the copied global rotation into all of the selected objects.
   - Paste Local Scale: Pastes the copied local scale into all of the selected objects.

Additional Notes:
- It's okay to select multiple objects before clicking Copy, but only the transform of the active object (the one shown in the inspector) will be copied.
- Copying an object's transform will not erase anything in your operating system's clipboard, nor will it erase anything in the component clipboard described in the next section.
- Because of a limitation with Unity3D, you cannot copy & paste global scale.

----------------------
COMPONENT COPY & PASTE

Component Copy & Paste allows you to copy & paste entire components at once. It works with both built-in components and custom scripts. An advanced "Clipboard" window allows you to select which individual variables will be pasted and even alter the values of those variables before they are pasted.

How to use Component Copy & Paste (basic):
1. Select a single object whose component you want to copy.
2. Right-click on the bold name of the desired component in the Inspector and then click Copy. All of the variables for that component will be copied into a special clipboard specific to that type of component.
3. Select an object whose component into which you want to paste copied variables.
4. Right-click on the bold name of the desired component in the Inspector and then click Paste. All of the variables copied from the first component will be pasted into this second component.

Using the Clipboard window (advanced):
1. Click the Window menu and then click Clipboard. The Clipboard window will appear. If you have already copied any components via the above method, you will see those components listed in the window. Otherwise, you will see an empty window except for short instruction text at the top.
2. If you have not already done so, copy a component into the clipboard using steps 1 and 2 of the basic instructions.
3. Click the little + button to the right of the name of any component in the Clipboard window to expand that component. You will now see all of the variables and their values copied from that component.
4. On the right side of the window next to every variable is a small check box. Unchecking a box will prevent its variable from being pasted when you select Paste in step 4 of the basic instructions.
5. The values of most copied variables can be edited directly in the Clipboard window, just as if the variable was shown in the inspector. Of course, editing these values won't change any components until you paste those edited values into a component.

Additional Notes:
- It's okay to select multiple objects before clicking Copy, but only the component of the active object (the one shown in the inspector) will be copied.
- Every time you copy a component, it adds a new entry into the Clipboard window and sets it as the "default" component to paste.
- Click the Default button next to a copied component's name to change the default component to paste.
- Click the Remove button near a copied component's name to remove that copied component from the Clipboard.
- Copying an object's transform will not erase anything in your operating system's clipboard, nor will it erase anything in the transform clipboard described in the previous section.
- If you want to paste a copied component into the same component of multiple selected objects, simply turn on Inspector Batching as explained later.
- Clicking the "Paste New" in the Clipboard window button will create a new component on the active game object and paste the copied values into that new component.

--------------
THE REMEMBERER

The Rememberer solves one of the most annoying aspects of the Unity3D editor, which is that any changes made to an object while the game is being played are forgotten as soon as the game stops. The Remember solves that by allowing you to "remember" the state of any object while playing and then "applying" the remembered state of those objects after the game is stopped.

How to use The Rememberer (basic):
1. Click the Play button to start playing your game.
2. Click the Window menu and then click Rememberer. A window with some instructions and four buttons will appear.
3. Select any object in the scene and modify it, such as moving its position or changing a variable of one of its components.
4. In the Rememberer window, click the Remember button. The name of the selected object will now be listed in the Rememberer window.
5. Repeat steps 3 & 4 for any number of other objects. You will see each object listed in the Rememberer window.
6. Stop the game. All of your modified objects will now return to their original states... But the Rememberer window will still list any objects you told it to remember.
7. Click the Apply All button. The state of any objects you "remembered" will now be restored.
8. Click the Forget All button. All remembered objects will now be forgotten. You should usually click this after applying so that you don't accidentally re-apply some changes in the future when you didn't mean to.

Advanced Usage:
- Double-click an object's name in the Rememberer window to select that object.
- Each object has an Apply button and a Forget button, allowing you to apply or forget changes made to an individual object.
- Click on the little + button to the right of an object to expand your view of that object. Using this expanded view you can apply or forget changes made to individual components or variables, as well as edit the remembered values of any variables, similar to how variables can be edited using the Clipboard window. The components themselves also have little + buttons, allowing you to apply, forget, or edit the remembered variables in those components.

Additional Notes:
- If you want to remember only an object's transform, click the Remember Transform button instead. This is easier than remembering an object and then forgetting all of its other components and variables.
- Selecting multiple objects and clicking Remember will remember the state of all of those objects.
- The remembered state of an object is the object's state as of the time you clicked the Remember button. Any future changes made will be forgotten, unless you again click the Remember button after making those additional changes.
- Any children of the selected objects will also be automatically remembered. You can see these children by clicking the little + button of the parent. You can individually apply or forget these children as well.
- Adding components or children to an object is not currently supported by the Rememberer. Such changes will be forgotten.

-------------
USAGES FINDER

The Usages Finder quickly finds all usages of any Unity object, component, prefab, script, or asset by other objects in either the scene or the Assets folder.

*NEW:* Usages Finder commands are now located at the bottom of the Assets menu.

How to find the objects that "use" a particular object:
1. Select the object (in either the scene or the project assets list) whose usages you want to find.
2. Click on the "Assets" menu and then click either "Find Usages In Scene" or "Find Usages In Assets":
   - In Scene: The Usages Finder will search for all objects and components in the currently-open scene that have a variable that refers to the selected object.
   - In Assets: The Usages Finder will search for all objects, components, and assets in the Assets folder that have a variable that refers to the selected object.
3. The Usages Finder window will open and it will start searching for usages of the selected object. This process may take a while, especially when searching "In Assets".

Additional Notes:
- When you have selected an object in the scene, you cannot search for assets that use the selected object since it is impossible for an asset to refer to an object in a scene.
- When searching for usages of a prefab in the scene, any prefab instances will be displayed in the Usages Finder window.
- When searching for usages of a GameObject, the Usages Finder will also automatically search for usages of any of its components, children, and children's components.
- You can right-click on a component's bold name in the Inspector and select "Find Usages in Scene" or "Find Usages in Assets" to find usages of that particular component.
- Due to restrictions in Unity, the Usages Finder cannot search inactive game objects.

-------------------
COMPONENT REORDERER

The Component Reorderer is an experimental feature that allows you to change the order in which components are defined on a game object. You may want to do this just for organizational purposes, but there are also some cases where the order of components matters, such as with image effects.

WARNING: Use with extreme caution! Reording components is currently in BETA and may not work perfectly or have unexpected side effects. Please make sure to back up the object or prefab you are modifying in case Unity's "undo" function doesn't work. See the Known Deficiencies section below.

How to reorder components:
1. Click the Window menu and then click Component Reorderer. A window with some text and buttons will appear.
2. Select the game object (or prefab) whose components you want to reorder.
3. Click the Up and Down buttons next to component names to move those components up and down in the order.
4. When satisfied with the new order, click the Apply button to make the changes.
5. If everything works, the components should now be in the new order. If there were errors, the Component Reorderer will *attempt* to undo the changes it made.

Additional Notes:
- Click the Revert button to change the order of components back to its original order. Note that once you hit Apply, the order cannot be reverted!
- Some components require other components to be ordered before. In this case, you will get a little error message underneath a component if it requires another component to be ordered before.

Known Deficiencies:
- Batching does not work when reordering components.
- Some components, such as the Flare Layer, cannot be reordered and may cause errors.
- Any variable references that point *to* reordered components will be lost.
- Some custom components may lose data when reordering.
- Changes to prefab assets cannot be undone!

------------------
EXTENDING EDITOR++

Though Editor++ works with all built-in components and any custom components using public fields, some Editor++ functionality will not work with some advanced custom components because it doesn't know how to copy the data in those components.

Luckily, it's possible to extend Editor++ to work with these advanced custom components. Take a look at the Editor++/DefaultObjectVariables.cs code file. It contains a static method for each type that Editor++ knows how to handle. Each of these methods returns an IEnumerable<ObjectVariableBase> and has an [ObjectVariables(typeof(SomeType))] attribute. You can define your own such methods anywhere in the project to handle custom types.

Each of the objects enumerated by these methods must extend from the ObjectVariableBase class, which is an abstract layer through which a particular "variable" of an object is accessed. If you simply need to handle a component's properties, you can return one ObjectPropertyVariable object for each such property. Or if you need extra-special behavior, you can extend the ObjectVariableBase class yourself.

-----------------------
GETTING THE SOURCE CODE

As of version 2.5, Enhanced Editor++ no longer comes with its source code included in the downloadable package, except for the DefaultObjectVariables.cs file. This change is simply to solve a bug that some users were having with conflicting filenames. However, you bought Enhanced Editor++, and I believe that you should have access to the source code. If you want the source code, please simply email me at walt@waltdestler.com and include your invoice number so that I can verify your purchase.
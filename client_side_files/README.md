This folder contains the source code files for the Xamarin application. These files are meant for a Xamarin.Forms project called SPOT_App -- they should be stored in "..\repos\SPOT_App\SPOT_App\SPOT_App".  
To create such a project (which you should only have to do once):  
- In Visual Studio navigate to: "File -> New" and click on "Project..."  
- On the left panel, under "Visual C#", you should see "Cross-Platform".  
- Select this and then select "Mobile App (Xamarin.Forms)" in the center panel.  
- Change the name and solution name to "SPOT_App". I left "Create directory for solution" checked and "Add to Source Control" unchecked.
- Click OK.
- Select a "Blank" template.
- Under "Platform" make sure that "Android" and "iOS" are checked.
- Under "Code Sharing Strategy" select ".NET Standard".
- Click OK.
- At this point it should create a new project called "SPOT_App".
- On the right, in the "Solution Explorer", right click on "SPOT_App" and select "Open Folder in File Explorer".
- File Explorer should now open with the directory "..\repos\SPOT_App\SPOT_App\SPOT_App".
- Paste the source code files from this repository into the directory.

When I did pasted the files, Visual Studio automatically recognized and added them to the project. I was then able to compile and run the program after selecting "Debug", "Any CPU", and "SPOT_App.Android" from the three fields to the left of the Compile/Run button on Visual Studio.

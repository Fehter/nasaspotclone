# NASA-SPOT

The "test_xamarin_app_v_1" folder contains -- among other files -- the files for the basic GUI. The GUI files are as follows:  

- App.xaml and App.xaml.cs: These are the overarching application files that cause all other files to be used.  
- HomePage.xaml and HomePage.xaml.cs: These files define how the homepage (which is visible after the user logs in) will look.  
- LoginPage.xaml and LoginPage.xaml.cs: These files define how the login page will look.  
- Request.cs: For now this is a dummy class -- I created it to stay in line with the MVVM pattern -- it might be completely unnecessary. 
- RequestDetailPage.xaml and RequestDetailPage.xaml.cs: These files defines how a request will be displayed when a user "taps" on them in the requests list.  
- RequestViewModel.cs: This file defines a class that contains data members and functions to represent a single "request" by a user.  
- RequestsPage.xaml and RequestsPage.xaml.cs: These files define how the list of requests (that the user can "tap") will appear.  

All other files/folders are either from templates (like GridDemoPage.xaml), automatically generated by Visual Studio, or have something to do with compilation/execution.

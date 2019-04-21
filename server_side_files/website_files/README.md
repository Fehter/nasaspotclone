This folder contains the following files:  

- request_form.html: This is the HTML file for the create request page.  

- style.css: This file is used by request_form.html to improve the visual aspects of the page.  

- handle_form.php: This is the data-handling file that is run when the user clicks "submit" on the request_form page.  
                   This file contains primarily testing code (print the contents of the POST, some validation of user input, connect to a local database, query the local database).  

- jquery-3.3.1.min.js: This filed is used by the request_form.html file -- it defines several jQuery functions. This was used to avoid copy-pasting several entry descriptions in the form.  

Place all of these files into the same folder. You can open the request_form.html file with a browser to see the page.  
However, unless you have PHP installed (and perhaps an Apache server), the request_form.html file will not function as intended (you will see the raw PHP code rather than the output it should display).  

In practice, all of these files will be hosted on the server computer (when we get it).  
Some of the server computer's requirements are as follows:  

- A version of PHP installed.  

- Apache server software (or an equivalent) to handle HTTP requests.  

- MySQL server/database software.  
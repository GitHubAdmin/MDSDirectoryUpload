MDSDirectoryUpload is an OpenSource tool to provide Providigm's customers with a means of submitting a directory full of MDS files to the abaqis MDS web service.

The tool is written using C#/.NET, so it should be usable on any modern Windows operating system.

It provides a simple user interface to allow the user specify the required information:

* Account ID*: the identifier of your abaqis account. This identifier will be provided by Providigm upon request.
* Account password*: the web service password for your abaqis account. This password will be provided by Providigm upon request.
* The directory from which MDS files should be identified and uploaded.

NOTE *: The account ID and password is not associated with any user account within abaqis. It is specific to access/use of the MDS web service itself.

The application will provide information on the status of each file upload. Within the directory you specified, the application will create sub-directories to which the application will move the files as they are processed. If there are errors processing files, those files will be moved to an 'Error' subdirectory. The following illustrates the layout of the subdirectories:

<pre>
+ User-specified directory
          |
          +-- Processed
          |       |
          |       +-- 20120101
          |       |       |
          |       |       +-- <processed MDS files>
          |       |
          |       +-- 20120102
          |               |
          |              ...
          |
          +-- Errored
                  |
                  +-- 20120101
                          |
                          +-- <MDS files that had errors during upload>
</pre>

The application supports a configuration file that allows you to default some of the inputs. In the development environment, the configuration file is called 'app.config'. With the executable, it is located with the exe file and is called MDSDirectoryUpload.exe.config.

If you prefer to modify the application to your own purposes, feel free to fork this Git repository and make the application your own.

For those of you that don't have a .NET development environment, I recommend using SharpDevelop. It works well and it's free.


 

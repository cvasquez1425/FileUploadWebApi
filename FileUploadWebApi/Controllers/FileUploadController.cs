using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class FileUploadController : Controller
{
    //Instead of the hard-coded constant for the file path to write the uploaded file to, you should take advantage of the ASP.NET Core configuration system to store the path.
    const string FILE_PATH = @"C:\Samples\";

     // GET This is a test.
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

    [HttpPost]
    public IActionResult Post([FromBody]FileToUpload theFile){
        //Create the full path and file name to store the uploaded file into. Use the FILE_PATH constant, followed by the FileName property, without the
        // file extension, from the file object uploaded. To provide some uniqueness to the file name, add on the current date and time. Remove any characters that aren’t valid
        // for a file by using the Replace() method. Finish the file name by adding on the file extension from the file object uploaded.
        var filePathName = FILE_PATH + 
        Path.GetFileNameWithoutExtension(theFile.FileName) + "-" + DateTime.Now.ToString().Replace("/", "")
        .Replace(":", "").Replace(" ", "") +
        Path.GetExtension(theFile.FileName);
        

        //Write the following code to strip off the file type.
        if (theFile.FileAsBase64.Contains(","))
            {
                theFile.FileAsBase64 = theFile.FileAsBase64
                .Substring(theFile.FileAsBase64
                .IndexOf(",") + 1);
            }

        // Don’t store the file uploaded as a Base64-encoded string. You want the file to be useable on the server just like it was on the user’s hard drive.
        //Convert the file data into a byte array using the FromBase64String() method on the .NET Convert class. 
        // Store the results of calling this method in to the FileAsByteArray property on the FileToUpload object.
        theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);

        // Write to a File.
        //You’re finally ready to write the file to disk on your server. Create a new FileStream object and pass the byte array to the Write() method of this method.
        using (var fs = new FileStream(
            filePathName, FileMode.CreateNew)) {
            fs.Write(theFile.FileAsByteArray, 0,
            theFile.FileAsByteArray.Length);
            }

        return Ok();
    }    
}

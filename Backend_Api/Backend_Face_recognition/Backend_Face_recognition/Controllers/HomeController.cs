using Backend_Face_recognition.ImageFunction;
using Backend_Face_recognition.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace Backend_Face_recognition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public dynamic RegisterUser(Users u )
        {
            string strm = u.face;

            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            string base64File = regex.Replace(strm, string.Empty);

            //this is a simple white background image
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            var bytess = Convert.FromBase64String(base64File);

            System.IO.File.Create(@"D:\\Python_FaceRecog\main.py").Dispose();
            var data = System.IO.File.ReadAllLines(@"D:\\Python_FaceRecog\main.py").ToList();

            data.Insert(0, "import os\n"
              + "import cv2\n"
              + "import face_recognition\n"
              + $"img = cv2.imread(\"D:\\\\Images\\\\{finalString}\\\\{finalString}.jpeg\")\n"
              + "rgb_img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)\n"
              + "img_encoding = face_recognition.face_encodings(rgb_img)[0]\n"
              + $"img2 = cv2.imread(\"D:\\\\Images\\\\m.jpeg\")\n"
              + "rgb_img2 = cv2.cvtColor(img2, cv2.COLOR_BGR2RGB)\n"
              + "img_encoding2 = face_recognition.face_encodings(rgb_img2)[0]\n"
              + "result = face_recognition.compare_faces([img_encoding], img_encoding2)\n"
              + "print(result)\n"
              + "r = str(result[0])\n"
              + $"file = open(os.getcwd() + '\\\\mangal.txt', 'w+')\n"
              + "file.write(r)\n"
              + "file.close()\n"
              );
            System.IO.File.WriteAllLines(@"D:\\Python_FaceRecog\main.py", data);


           

            if (!Directory.Exists($@"D:\Images\{finalString}"))
            {

                Directory.CreateDirectory($@"D:\Images\{finalString}");
                string filepath = @"D:\Images\" + finalString + $"\\{finalString}.jpeg";
                using (var imageFile = new FileStream(filepath, FileMode.Create))
                {
                    imageFile.Write(bytess, 0, bytess.Length);
                    imageFile.Flush();
                }
            }
           

            Process p = new Process();
            p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.StandardInput.WriteLine(@"cd /d D:\Python_FaceRecog");
            p.StandardInput.WriteLine($@"main.py");
            p.StandardInput.Flush();
            p.StandardInput.Close();
            p.WaitForExit();
            string output = p.StandardOutput.ReadToEnd();
            p.Close();

            var data1 = System.IO.File.ReadAllLines(@"D:\Python_FaceRecog\mangal.txt");


            return new { name = data1 };
        }
    }
}

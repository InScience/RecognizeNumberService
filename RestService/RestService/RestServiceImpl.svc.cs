using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using openalprnet;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Drawing;
using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient; // reikia framework versijos 4.5.1 (o default yra 4)

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestServiceImpl" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestServiceImpl.svc or RestServiceImpl.svc.cs at the Solution Explorer and start debugging.
    public class RestServiceImpl : IRestServiceImpl
    {
        #region IRestService Members

        public string XMLData(string id)
        {
            return "Irasytas skaicius" + id;
        }

        public string JSONData(string id)
        {
            return "Irasytas skaicius" + id;
        }

        public string ADD(string A, string B)
        {
           int sum = Convert.ToInt32(A) + Convert.ToInt32(B);

            string result = sum.ToString();


            return result; }

        public string CarNumber()
        {
            string CarNum="";


            return CarNum;
        }




        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public string Upload(System.IO.Stream data)
        {
           
            string response = "response";
            //MultipartParser.ParseFiles(
            //       data,
            //       WebOperationContext.Current.IncomingRequest.ContentType,
            //       MyProcessMethod);


            using (Stream file = File.Create(@"C:\Users\Laurynas\Desktop\RestTest.txt"))
            {
                CopyStream(data, file);
            }


            return response;
        }

        public User JsonGet(String id)
        {
            return new User { Vardas = "Jonas", Pavarde = "Jonaitis", id = 1 };
        }

       public void SaveUser(User user)
        {
         
            try
            {
                using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@"C:\Users\Laurynas\Desktop\RestTest.txt"))

                    {
                   file.WriteLine("test");
                if( user != null)
                {

                        file.WriteLine("test");
                        file.WriteLine(user.Vardas + user.Pavarde + user.id + "test");


                    }
                    }

            }
            catch (Exception){ throw; }


        }


        public void SaveUserStream(Stream str)
        {
            try
            {
                if (str != null)
                {

                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User[]));
                    var result = (User[])json.ReadObject(str);
   using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Laurynas\Desktop\RestTest.txt"))
                        {
                    foreach (var item in result)
                    {                      
                        file.WriteLine(item.Vardas.ToString() + item.Pavarde.ToString() + item.id.ToString());
                    }
                      }
                    //StreamReader strReader = new StreamReader(str); // alternative
                    //string text = strReader.ReadToEnd();

                    //// parse
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

       public User SaveUserNoObject(String Vardas, String Pavarde, int id)
        {

            try
            {
                using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@"C:\Users\Laurynas\Desktop\RestTest.txt"))

                {
                 
                    if (Vardas != null)
                    {

                        file.WriteLine(Vardas + " "+ Pavarde +" " + id.ToString() );


                    }
                }

            }
            catch (Exception) { throw; }


            return new User { Vardas = Vardas, Pavarde = Pavarde, id = id };
        }


        public String UploadPic(String PicByt)
        {

            Repository Rep = new Repository();

            byte[] bytes = System.Convert.FromBase64String(PicByt);

            Image Img = byteArrayToImage(bytes);

            string filename = GenerateFileName("RaspFoto");

            string res = "Klaida";


            List<string> Plates = Recognize(bytes);

            if (Plates.Count > 0) { 

            
            //res = Plates.ElementAt(0);
            res = Plates.ElementAt(1);


            string Characters = Plates.ElementAt(1);

            Characters = Characters.Replace('O', '0');
            res = res.Replace('O', '0');

                float Confidence = float.Parse(Plates.ElementAt(2));
            string DbNumbers;

            Img.Save(@"C:\Users\Laurynas\Documents\Visual Studio 2015\Projects\RestService\images\" + filename + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Rep.UploadPicDB(filename);
            string id = Rep.GetPicId(filename);
          //  Rep.CreateEvent(id, Characters, Confidence);
            DbNumbers = Rep.CheckRegVehicles(Characters);
                string vehid;
                if (DbNumbers != "-1")
                {
                    vehid = Rep.GetVehicleIdByLicense(Characters);
                    Rep.CreateEvent2(vehid,id, Characters, Confidence);
                    res = res + " at";
                }
                else
                {
                    Rep.CreateEvent(id, Characters, Confidence);
                    res = res + " neat";
                }
            }
            
            return res;
        }

       
        #endregion

 public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public List<string> Recognize(byte[] byteArr)
        {
            List<string> RecInfo = new List<string>();

            var alpr = new AlprNet("eu", @"C:\Users\Laurynas\Documents\Visual Studio 2015\Projects\ConsoleAlpr\ConsoleAlpr\bin\Debug\openalpr.conf", @"C:\Users\Laurynas\Documents\Visual Studio 2015\Projects\ConsoleAlpr\ConsoleAlpr\bin\Debug\runtime_data");

            if (!alpr.IsLoaded())
            {
                string fail = "OpenAlpr failed to load!";
                RecInfo.Add(fail);
                return RecInfo;
            }

            var results = alpr.Recognize(byteArr);
       
            string platenumber;
            foreach (var result in results.Plates)
            {
                foreach (var plate in result.TopNPlates)
                {
                    platenumber = "Plate: " + plate.Characters + " Confidence: " + plate.OverallConfidence;
                    RecInfo.Add(platenumber);
                    RecInfo.Add(plate.Characters);
                    RecInfo.Add(plate.OverallConfidence.ToString());
                }
            }



            return RecInfo;
        }


        public string GenerateFileName(string context)
        {
            return context + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString("N");
        }

       

       
     
    }



       

    }


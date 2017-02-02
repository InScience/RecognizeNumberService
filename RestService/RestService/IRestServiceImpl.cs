using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestService
{
   
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestServiceImpl" in both code and config file together.
    [ServiceContract]
    public interface IRestServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "xml/{id}")]
        string XMLData(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/{id}")]
        string JSONData(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "sum/{sum1};{sum2}")]
        string ADD(string sum1, string sum2);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "Recognize/RecCar")]
        string CarNumber();

        //[OperationContract]
        //[WebInvoke(UriTemplate = "/PlaceOrder",
        //   RequestFormat = WebMessageFormat.Json,
        //   ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        //string PlaceOrder(string OrderID, string OrderTotal);

        [OperationContract]
        [WebInvoke(Method = WebRequestMethods.Http.Post, UriTemplate = "/PlaceOrder")]
        string Upload(System.IO.Stream data);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "jsonUser/{id}")]
        User JsonGet(String id);

        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           // ResponseFormat =WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           UriTemplate = "SaveUser")]
        void SaveUser(User user);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            // ResponseFormat =WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SaveUserStream")]
        void SaveUserStream(Stream str);

        [OperationContract]
        [WebInvoke(Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat =WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.WrappedRequest,
          UriTemplate = "SaveUserNoObj")]
        User SaveUserNoObject(String Vardas, String Pavarde, int id);

        [OperationContract]
        [WebInvoke(Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.WrappedRequest,
          UriTemplate = "UploadPic")]
        String UploadPic(String PicByt);


    }
        [DataContract]
         public class User
        {

        private String _Vardas;
        private String _Pavarde;
        private int _id;

            [DataMember(Name ="Vardas")]
           public String Vardas { get { return _Vardas; } set { _Vardas = value; } }

            [DataMember(Name = "Pavarde")]
           public String Pavarde { get { return _Pavarde; } set { _Pavarde = value; } }

           [DataMember(Name = "id")]
           public int id { get { return _id; } set { _id = value; } }
    }

    [DataContract]
    public class Pic
    {
        private byte[] _PicByt;

        [DataMember(Name = "PicByt")]
        public byte[] PicByt { get { return _PicByt; } set { _PicByt = value; } }


    }


}

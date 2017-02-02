using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data;

namespace RestService
{
    public class Repository
    {
        public void UploadPicDB(string filename)
        {

            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            DateTime now = DateTime.Now;

            MySqlConnection conn = new MySqlConnection(MyConString);
            MySqlCommand cmd;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO raspfoto(date, imagepath)VALUES(@date,@imagepath)";
                cmd.Parameters.AddWithValue("@date", now);
                cmd.Parameters.AddWithValue("@imagepath", filename);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            conn.Close();
        }

        public void CreateEvent(string id, string plate, float confidence)
        {

            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            DateTime now = DateTime.Now;

            MySqlConnection conn = new MySqlConnection(MyConString);
            MySqlCommand cmd;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO event(rpID, confidence, recNumber)VALUES(@rpID,@confidence,@recNumber)";
                cmd.Parameters.AddWithValue("@rpID", id);
                cmd.Parameters.AddWithValue("@confidence", confidence);
                cmd.Parameters.AddWithValue("@recNumber", plate);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            conn.Close();
        }

        public void CreateEvent2(string vehid , string id, string plate, float confidence)
        {
            int one = 1;
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            DateTime now = DateTime.Now;

            MySqlConnection conn = new MySqlConnection(MyConString);
            MySqlCommand cmd;
            conn.Open();
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO event(sID, gaID, vID, rpID, confidence, recNumber)VALUES(@sID,@gaID,@vID,@rpID,@confidence,@recNumber)";
                cmd.Parameters.AddWithValue("@sID", one);
                cmd.Parameters.AddWithValue("@gaID", one);
                cmd.Parameters.AddWithValue("@vID", vehid);
                cmd.Parameters.AddWithValue("@rpID", id);
                cmd.Parameters.AddWithValue("@confidence", confidence);
                cmd.Parameters.AddWithValue("@recNumber", plate);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            conn.Close();
        }



        public string GetPicId(string filename)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string id = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT rpid from raspfoto where imagepath = @filename";
                // cmd.CommandText = "SELECT rpid from raspfoto where imagepath = 'RaspFoto_20161204175847549_b0030a17fec34ccb9322cbf03b8db290'";
                cmd.Parameters.AddWithValue("@filename", filename);
                id = cmd.ExecuteScalar().ToString();

                return id;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return id;
        }
        //------------------------------------------------ extra
        public string GetPicdate(string filename)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string date = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT date from raspfoto where imagepath = @filename";
                cmd.Parameters.AddWithValue("@filename", filename);
                date = cmd.ExecuteScalar().ToString();

                return date;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return date;
        }

        public string GetPicName(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string name = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT imagepath from raspfoto where rpid = @id";
                cmd.Parameters.AddWithValue("@id", id);
                name = cmd.ExecuteScalar().ToString();

                return name;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return name;
        }

        public string GetEventNumber(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT recNumber from event where eID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetEventConfidence(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT confidence from event where eID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetEventStateid(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT sID from event where eID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetEventGateid(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT gaID from event where eID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetEventVehicleid(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT vID from event where eID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetEventphotoId(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT rpID from event where eID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetStateCode(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT code from states where sID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetStateDesc(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT sname from states where sID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }


        public DataSet GetAllOwners()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * FROM owner";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);              
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {              
            }

            return ds;
        }

        public DataSet GetAllVehicles()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * FROM vehicle";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public DataSet GetAllEvents()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * FROM event";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public DataSet GetAllStates()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * FROM states";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public DataSet GetAllAdministrators()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * FROM administrator";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public DataSet GetAllGates()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * FROM gate";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public DataSet GetAllEventsInformation()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT event.confidence, event.recNumber, raspfoto.date, vehicle.brand, vehicle.model, owner.name, owner.surname, states.sname FROM event  LEFT JOIN raspfoto ON event.rpID=raspfoto.rpID  LEFT JOIN vehicle ON event.vID=vehicle.vID  LEFT JOIN owner ON vehicle.dID=owner.dID LEFT JOIN states ON event.sID=states.sID ";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {
            }

            return ds;
        }


        public DataSet GetAllVehicleInformation()
        {
            DataSet ds = new DataSet();
            string MyConString = "Server=localhost;Database=symfony;Uid=root;Pwd=";

            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT owner.dID, owner.name, owner.surname, vehicle.vID, vehicle.licenseNum, vehicle.brand, vehicle.model FROM owner INNER JOIN vehicle ON owner.dID=vehicle.dID";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                adap.Fill(ds, "Table");
            }
            catch (Exception)
            {
            }

            return ds;
        }

        public string GetOwnerName(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT name from owner where dID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetOwnerSurname(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT surname from owner where dID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetOwnerPhoneNumber(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT phoneNumber from owner where dID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetOwnerAddress(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT phoneNumber from owner where dID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetOwnerregDate(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT regDate from owner where dID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetVehicleOwnerId(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT dID from vehicle where vID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetVehiclelicenseNumber(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT dID from vehicle where vID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetVehicleIdByLicense(string License)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT vID from vehicle where licenseNum = @num";
                cmd.Parameters.AddWithValue("@num", License);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }



        public string GetVehiclebrand(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT brand from vehicle where vID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string GetVehiclemodel(string id)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT model from vehicle where vID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }

        public string CheckRegVehicles(string license)
        {
            string MyConString = "Server=localhost;Database=gate_admin;Uid=root;Pwd=";
            string number = "-1";
            MySqlConnection conn = new MySqlConnection(MyConString);
            conn.Open();
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT dID from vehicle where licenseNum = @license";
                cmd.Parameters.AddWithValue("@license", license);
                number = cmd.ExecuteScalar().ToString();

                return number;

            }
            catch (Exception)
            {
            }
            conn.Close();
            return number;

        }


    }
}
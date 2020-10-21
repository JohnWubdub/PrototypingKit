using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

//last edit by John Wanamaker
public class EmailTest : MonoBehaviour
{
    //attach this to a button to run
    //WARNING!! THIS IS NOT SECURE PLEASE DO NOT USE THIS IN A REAL GAME THAT SOMEONE CAN DATA MINE OR USE A PUBLIC STRING THAT IS ASSIGNED IN THE EDITOR TO HIDE IT WHICH IS PROBABLY NOT SECURE EITHER
    //make sure the from_emailaddress allows less secure apps to access it
    //make sure there is an internet connection

    public void SendMail()
    {
        MailMessage m = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;
        SmtpServer.EnableSsl = true;

        m.From = new MailAddress("from_emailaddress");
        m.To.Add(new MailAddress("to_emailaddress"));

        m.Subject = "Subject";
        m.Body = "Body";

        SmtpServer.Credentials = new System.Net.NetworkCredential("from_emailaddress","password");
        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors){
            return true;
        };

        m.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(m);
    }
    

    
}

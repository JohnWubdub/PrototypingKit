using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UE.Email;

public class EmailTest : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SendMail(){
        Email.SendEmail("poemcollecting@gmail.com", "poemcollecting@gmail.com", "subject", "body", "smtp.gmail.com", "poemcollecting@gmail.com", "technodramatists");
        /*
        MailMessage m = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;
        SmtpServer.EnableSsl = true;

        m.From = new MailAddress("casuallyhardcoremail@gmail.com");
        m.To.Add(new MailAddress("casuallyhardcoremail@gmail.com"));

        m.Subject = "FUCK YOU IT WORKS!!";
        m.Body = "fuck yeah";

        SmtpServer.Credentials = new System.Net.NetworkCredential("casuallyhardcoremail@gmail.com","madison1998");
        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors){
            return true;
        };

        m.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(m);
        */
    }
    

    
}

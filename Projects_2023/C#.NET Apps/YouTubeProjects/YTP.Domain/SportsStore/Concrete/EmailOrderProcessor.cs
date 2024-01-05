using System.Net;
using System.Net.Mail;
using System.Text;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Domain.SportsStore.Concrete {

    public class EmailSettings {
        public string MailToAddress = "dummyemailto@email.com";
        public string MailFromAddress = "dummyemailfrom@email.com";
        public bool UseSSL = true;
        public string Username = "dummyusername";
        public string Password = "dummypassword";
        public string ServerName = "smtp.office365.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\Users\admin\emaillogs";


    }
    public class EmailOrderProcessor : IOrderProcessor {
        private readonly EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings settings) {
            _emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo) {
            using (var smtpClient = new SmtpClient()) {
                smtpClient.EnableSsl = _emailSettings.UseSSL;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = 
                    new NetworkCredential(
                        _emailSettings.Username, 
                        _emailSettings.Password);
                        
                if(_emailSettings.WriteAsFile ) {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .Append("A new order has been submitted")
                    .Append("-----")
                    .Append("Items:");

                foreach ( var item in cart.Lines ) {
                    var subtotal = item.Product.ProductPrice * item.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c})", item.Quantity, item.Product.ProductName, subtotal);


                }

                body.AppendFormat("Total Order Value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("----")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingInfo.CustomerName)
                    .AppendLine(shippingInfo.AddressLine1)
                    .AppendLine(shippingInfo.AddressLine2 ?? "")
                    .AppendLine(shippingInfo.AddressLine3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.PostalCode)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Phone)
                    .AppendLine("------")
                    .AppendFormat("GiftWrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailmessage = new MailMessage(
                        _emailSettings.MailFromAddress,
                        _emailSettings.MailToAddress,
                        "New Order Submitted!",
                        body.ToString());

                if(_emailSettings.WriteAsFile) {
                    mailmessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailmessage);
            }
        }
    }
}

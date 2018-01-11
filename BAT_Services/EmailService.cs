using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BAT_Models;

namespace BAT_Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string from, string fromPW, string SMTPHost, string subject, string body);
        string GetLoadingEmailBody(Parameters parameters);
    }//IEmailService

    /// <summary>
    /// Contains methods associated with emailing application, trade, and algorithm information.
    /// </summary>
    public class EmailService : IEmailService
    {
        private IInformationService _informationService;

        public EmailService(IInformationService informationService)
        {
            _informationService = informationService;
        }//EmailService

        public void SendEmail(string to, string from, string fromPW, string SMTPHost, string subject, string body)
        {
            MailMessage mail = new MailMessage(from, to);
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(from, fromPW);
            client.Host = SMTPHost;
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = body;
            client.Send(mail);
        }//SendEmail

        public string GetLoadingEmailBody(Parameters parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html><div style=\"font-size:10.5px; font-family:Tahoma;\">");
            sb.AppendLine("BAT Loading - " + DateTime.Now + " - Algorithm Type: <b>" + parameters.Algo + "</b>");
            sb.AppendLine("<br />");
            sb.AppendLine("Connecting to: " + parameters.BinanceAPIAddress);
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");

            if (parameters.Algo == Parameters.TradingAlgorithms.REBALANCE)
            {

                //TODO Verify that base currency can be mapped from all other currencies and display prices.

                sb.AppendLine("**********************************************************");
                sb.AppendLine("<br />");
                sb.AppendLine("REBALANCE Settings");
                sb.AppendLine("<br />");
                sb.AppendLine("<br />");
                sb.AppendLine("Currency Deviance Allowed: " + parameters.RebalanceThreshold + "%");
                sb.AppendLine("<br />");
                sb.AppendLine("Base Currency: " + parameters.BaseCurrency + " (" + parameters.BaseCurrencyAllocation + "% Allocation)");
                sb.AppendLine("<br />");
                sb.AppendLine("Currency 1: " + parameters.Currency1 + " (" + parameters.Currency1Allocation + "% Allocation)");
                sb.AppendLine("<br />");
                if (!string.IsNullOrWhiteSpace(parameters.Currency2))
                {
                    sb.AppendLine("Currency 2: " + parameters.Currency2 + " (" + parameters.Currency2Allocation + "% Allocation)");
                    sb.AppendLine("<br />");
                }//if

                if (!string.IsNullOrWhiteSpace(parameters.Currency3))
                {
                    sb.AppendLine("Currency 3: " + parameters.Currency3 + " (" + parameters.Currency3Allocation + "% Allocation)");
                    sb.AppendLine("<br />");
                }//if

                if (!string.IsNullOrWhiteSpace(parameters.Currency4))
                {
                    sb.AppendLine("Currency 4: " + parameters.Currency4 + " (" + parameters.Currency4Allocation + "% Allocation)");
                    sb.AppendLine("<br />");
                }//if
                sb.AppendLine("**********************************************************");
                sb.AppendLine("<br />");
                sb.AppendLine("<br />");
                
                sb.AppendLine("Connectivity Verification and Current Pricing");
                sb.AppendLine("<br />");

                try
                {
                    var basePriceUSD = _informationService.GetPrice(parameters.BaseCurrency + "USDT");
                    var currency1Price = _informationService.GetPrice(parameters.Currency1 + parameters.BaseCurrency);
                    sb.AppendLine(parameters.BaseCurrency + " (Base) => " + basePriceUSD.ToString("C2"));
                    sb.AppendLine("<br />");
                    sb.AppendLine(parameters.Currency1 + " => " + currency1Price + " " + parameters.BaseCurrency + " => " + (currency1Price * basePriceUSD).ToString("C2"));
                    sb.AppendLine("<br />");
                    if (!string.IsNullOrWhiteSpace(parameters.Currency2))
                    {
                        var currency2Price = _informationService.GetPrice(parameters.Currency2 + parameters.BaseCurrency);
                        sb.AppendLine(parameters.Currency2 + " => " + currency2Price + " " + parameters.BaseCurrency + " => " + (currency2Price * basePriceUSD).ToString("C2"));
                        sb.AppendLine("<br />");
                    }//if

                    if (!string.IsNullOrWhiteSpace(parameters.Currency3))
                    {
                        var currency3Price = _informationService.GetPrice(parameters.Currency3 + parameters.BaseCurrency);
                        sb.AppendLine(parameters.Currency3 + " => " + currency3Price + " " + parameters.BaseCurrency + " => " + (currency3Price * basePriceUSD).ToString("C2"));
                        sb.AppendLine("<br />");
                    }//if

                    if (!string.IsNullOrWhiteSpace(parameters.Currency4))
                    {
                        var currency4Price = _informationService.GetPrice(parameters.Currency4 + parameters.BaseCurrency);
                        sb.AppendLine(parameters.Currency4 + " => " + currency4Price + " " + parameters.BaseCurrency + " => " + (currency4Price * basePriceUSD).ToString("C2"));
                        sb.AppendLine("<br />");
                    }//if
                    sb.AppendLine("<br />");
                    sb.AppendLine("<br />");
                }
                catch(Exception e)
                {
                    string errorMessages = e.Message + "<br/>";

                    Exception loopEx = e;

                    while (loopEx.InnerException != null)
                    {
                        loopEx = loopEx.InnerException;
                        errorMessages += loopEx.Message + "<br/>";
                    } 


                    sb.AppendLine("<b>Aborting run! An exception occurred while connecting to the markets and retrieving prices.  Ensure that all currency keys are valid and all trade into the base currency.</b> <br /> <i>Message</i>: " + errorMessages);
                    sb.AppendLine("<br />");
                }
                

            }//if

            if (parameters.UseCircuitBreaker)
            {
                sb.AppendLine("Circuit breakers are active.  <b><i>Trading will not occur if " + parameters.CircuitBreakerTrades + " or more trading batches occurred within the prior " + parameters.CircuitBreakerHours + " hours.</i></b>");
                sb.AppendLine("<br />");
            }
            else
            {
                sb.AppendLine("Cricuit breakers are inactive.  <b><i>No trading frequency will halt this programs operation.</i></b>");
                sb.AppendLine("<br />");
            }

            sb.AppendLine();

            if (parameters.SendAlgorithmEmail)
            { sb.AppendLine("Emails will be sent once algorithm execution has happened."); sb.AppendLine("<br />"); }//if

            if (parameters.SendTradeExecutionEmail)
            { sb.AppendLine("Emails will be sent once trade execution has happened."); }//if

            if(!parameters.FailOnError)
            { sb.AppendLine("The system will not halt trading due to error messages within the trading logs."); sb.AppendLine("<br />"); }//if
         
            sb.AppendLine(" </ div ></ html > ");

            return sb.ToString();
        }//GetLoadingEmailBody

    }//Email Service
}

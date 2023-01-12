using System.Globalization;
using System.Text;
using BERlogic.CallCenter.Models;

namespace BERlogic.CallCenter.Templates
{
    public static class TemplateGenerator
    {
        public static string GetHtmlRebookingPdfString(ServiceOperationsModel m, string logopath)
        {
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><html><head><meta charset = \'utf-8\'></head>");
            sb.Append("<body><div style=\'background-color: white\'>");
            sb.Append($"<img src ='{logopath}' alt='test' style='height: 200px; width: 500px'></p><table>");
            sb.Append($"<tbody><tr><td style ='width: 120px;'></td><td style ='width: 800px'></td><td style ='text-align: left; width: 150px; font-weight: bold'>{m.AgencyCode}</td>");
            sb.Append("<td style =\'width: 250px\'></td></tr><tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td>");
            sb.Append("<td style=\'text-align: left; min-width: 25px\'>Kundencenter</td><td style =\'min-width: 25px\'></td></tr><tr><td colspan =\'2\' style =\'text-align: left; min-width: 25px\'> test GmbH • test Str. 01 • 0001 Berlin</td>");
            sb.Append("<td colspan=\'2\' style=\'text-align: left; min-width: 25px\'>Bitte wenden Sie sich mit allen Fragen jederzeit an:</td></tr><tr><td style =\'min-width: 25px\'></td>");
            sb.Append("<td style=\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Tel:</td><td style =\'min-width: 25px\'>+49 30 208981350</td></tr><tr>");
            sb.Append("<td style=\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Mo-Fr: 09:00 Uhr - 17:00 Uhr (CET)</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>EMail:</td><td style =\'min-width: 25px\'>test@test.de</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Website:</td><td style =\'min-width: 25px\'>www.test.de</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>USt.- ID Nr.:</td><td style =\'min-width: 25px\'>DE0000001</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>Kunde:</td><td style ='min-width: 25px; font-weight: bold'>{m.CustomerFullName}</td><td style ='min-width: 25px'>Buchungsdatum:</td><td style ='min-width: 25px; font-weight: bold' >{m.BookingDate.ToShortDateString()}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>E-mail:</td><td style ='min-width: 25px; font-weight: bold'>{m.CustomerEmail}</td><td style ='min-width: 25px'>Referenznummer:</td><td style ='min-width: 25px; font-weight: bold'>{m.BookingNumber}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>Zahlungsart:</td><td style ='min-width: 25px; font-weight: bold'>{m.PaymentMethod}</td><td style ='min-width: 25px'> Buchungscode:</td><td style ='min-width: 25px; font-weight: bold'>{m.BookingCode}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'></td><td style ='min-width: 25px'></td><td style = 'min-width: 25px'>Umbuchungsdatum:</td><td style ='min-width: 25px; font-weight: bold'>{m.ChangeServiceDate.ToShortDateString()}</td></tr>");
            sb.Append("</tbody></table><h2 style=\'margin-top: 150px\'><u><strong>Umbuchungsbestätigung</strong></u></h2><br/><table style=\'width: 800px\'><thead style=\'text-align: center; background-color: beige\'>");
            sb.Append("<tr><td style =\'width: 150px\'>Datum</td><td style =\'width: 75px\'>Flug</td><td style =\'width: 100px\'>FlugNr.</td><td style =\'width: 100px\'>Von</td><td style =\'width: 100px\'>Abflug</td><td style =\'width: 100px\'>Nach</td><td style =\'width: 100px\'>Ortsankunft</td></tr></thead><tbody style=\'text-align: center\'>");
            foreach (var route in m.LineRoutes)
            {
                sb.Append($"<tr><td style ='width: 150px; font-weight: bold'>{route.DepartureDate.ToShortDateString()}</td><td style ='width: 75px; font-weight: bold'>{route.FlightAirline}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.FlightNumber}</td><td style ='width: 100px; font-weight: bold'>{route.DepartureAirport}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.DepartureTime.ToShortTimeString()}</td><td style ='width: 100px; font-weight: bold'>{route.ArrivalAirport}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.ArrivalTime.ToShortTimeString()}</td></tr>");
            }
            sb.Append("</tbody></table><br/><p>Gemäß der Ihnen bekannten Tarifbedingungen berechnen wir folgendes:</p><table style=\'width: 800px\'><tbody><tr><td style =\'width: 100px\'></td><td style=\'width: 500px\'></td><td style =\'width: 200px\'><strong>Brutto</strong></td></tr>");
            for (int i = 0; i < m.PassangerModels.Count; i++)
            {
                sb.Append("<tr><td>");
                if (i == 0) sb.Append("<span>Passagier(e)</span>");
                sb.Append($"</td><td style ='font-weight: bold'>{m.PassangerModels[i].FullName}</td><td></td></tr>");
            }
            sb.Append("</tbody></table><br/><br/><table style=\'width: 800px\'><tbody>");
            sb.Append($"<tr><td style ='width: 300px'>Preisdifferenz:</td><td style ='width: 300px'></td><td style ='width: 200px; font-weight: bold; text-align: right' >{m.PriceDifference.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td style = 'border-bottom:2pt solid black'>Umbuchungsgebühren:</td><td style ='border-bottom:2pt solid black'></td><td style ='border-bottom: 2pt solid black; font-weight: bold; text-align: right'>{m.RebookingFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td>Zwischensumme Airline:</td><td></td><td style ='font-weight: bold; text-align: right'>{m.AirlineFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td>Bearbeitungsgebühren test:</td><td></td><td style ='font-weight: bold; text-align: right; border-bottom: 2pt solid black; height: 50px'>{m.BerlogicFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td></td><td>Rechnungsbetrag EURO</td><td style ='font-weight: bold; text-align: right; border-bottom: 4pt solid black;'>{m.TotalFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append("</tbody></table><br/><br/><br/><br/><br/>");
            sb.Append("<p style=\'text-align: center; margin-top: 100px\'>Die BERlogic GmbH ist zum Inkasso berechtigt.Vielen Dank für Ihre Buchung.</p><br/><br/>");
            sb.Append("</div></body></html>");
            return sb.ToString();
        }

        public static string GetHtmlRebookingMailString(ServiceOperationsModel model)
        {
            var sb = new StringBuilder();
            //sb.Append($"<!DOCTYPE html><html><head><meta charset = 'utf-8'></head><body>");
            sb.Append("<div style=\'background - color: beige; font - family: \'Calibri Light\',serif\'>");
            sb.Append("<p>Sehr geehrte Damen und Herren,<br/>vielen Dank für Ihre Anfrage.</p>");
            sb.Append("<p>Anbei übersenden wir Ihnen die <strong>Umbuchungsbestätigung</strong>.</p>");
            if (model.TotalFee > 0)
            {
                sb.Append($"<p>Der Betrag in Höhe von <span style='font - weight: bold; font - size: larger'>{model.TotalFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</span> wird von der <span style='font - weight: bold; font - size: larger'>Kreditkarte</span> abgebucht.</p>");
            }
            sb.Append("<p>Bitte prüfen Sie alle Angaben auf ihre Richtigkeit und <br/>teilen Sie uns diese bei Unstimmigkeiten umgehend mit.</p>");
            sb.Append("<p>Für Rückfragen stehen wir Ihnen jederzeit gerne zur Verfügung.</p>");
            sb.Append("<p style=\'font - weight: bold; font - size: larger\'>Wir wünschen Ihnen einen guten Flug!</p></div>");
            return sb.ToString();
        }
        public static string GetHtmlSignatureMailString(ApplicationUser task)
        {
            var sb = new StringBuilder();
            sb.Append("<div style=\'font - family: monospace\'><hr/>");
            sb.Append($"<p>Mit freundlichen Grüßen,<br/><span style ='font-weight: bold; font-size: larger'>{task.FirstName} {task.LastName}</span ></p>");
            sb.Append("<p>Kundencenter<br/>test GmbH<br/><a href =\'http://www.test.de/\'>www.test.de</a></p>");
            sb.Append("<p>Mo-Fr: 09:00 Uhr - 17:00 Uhr (CET)<br/>Telefonnummer: +49 30 000 - 000 - 000(Festnetzkonditionen)<br/>E - mail: <a href = \'mailto:test@test.de\'>test@test.de</a></p></div>");
            //sb.Append($"</body></html>");
            return sb.ToString();
        }

        public static string GetHtmlCancellationPdfString(ServiceOperationsModel m, string logopath)
        {
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><html><head><meta charset = \'utf-8\'></head>");
            sb.Append("<body><div style=\'background-color: white\'>");
            sb.Append($"<img src ='{logopath}' alt='test' style='height: 200px; width: 500px'></p><table>");
            sb.Append($"<tbody><tr><td style ='width: 120px;'></td><td style ='width: 800px'></td><td style ='text-align: left; width: 150px; font-weight: bold'>{m.AgencyCode}</td>");
            sb.Append("<td style =\'width: 250px\'></td></tr><tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td>");
            sb.Append("<td style=\'text-align: left; min-width: 25px\'>Kundencenter</td><td style =\'min-width: 25px\'></td></tr><tr><td colspan =\'2\' style =\'text-align: left; min-width: 25px\'> test GmbH • test Str. 01 • 100000 Berlin</td>");
            sb.Append("<td colspan=\'2\' style=\'text-align: left; min-width: 25px\'>Bitte wenden Sie sich mit allen Fragen jederzeit an:</td></tr><tr><td style =\'min-width: 25px\'></td>");
            sb.Append("<td style=\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Tel:</td><td style =\'min-width: 25px\'>+49 30 208981350</td></tr><tr>");
            sb.Append("<td style=\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Mo-Fr: 09:00 Uhr - 17:00 Uhr (CET)</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>EMail:</td><td style =\'min-width: 25px\'>test@test.de</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Website:</td><td style =\'min-width: 25px\'>www.test.de</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>USt.- ID Nr.:</td><td style =\'min-width: 25px\'>DE00000001</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>Kunde:</td><td style ='min-width: 25px; font-weight: bold'>{m.CustomerFullName}</td><td style ='min-width: 25px'>Buchungscode:</td><td style ='min-width: 25px; font-weight: bold' >{m.BookingCode}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>E-mail:</td><td style ='min-width: 25px; font-weight: bold'>{m.CustomerEmail}</td><td style ='min-width: 25px'>Referenznummer:</td><td style ='min-width: 25px; font-weight: bold'>{m.BookingNumber}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>Zahlungsart:</td><td style ='min-width: 25px; font-weight: bold'>{m.PaymentMethod}</td><td style ='min-width: 25px'></td><td style ='min-width: 25px; font-weight: bold'></td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style = \'min-width: 25px\'></td><td style =\'min-width: 25px; font-weight: bold\'></td></tr>");
            sb.Append("</tbody></table><h2 style=\'margin-top: 150px\'><u><strong>Stornobestätigung</strong></u></h2><br/><table style=\'width: 800px\'><thead style=\'text-align: center; background-color: beige\'>");
            sb.Append("<tr><td style =\'width: 150px\'>Datum</td><td style =\'width: 75px\'>Flug</td><td style =\'width: 100px\'>FlugNr.</td><td style =\'width: 100px\'>Von</td><td style =\'width: 100px\'>Abflug</td><td style =\'width: 100px\'>Nach</td><td style =\'width: 100px\'>Ortsankunft</td></tr></thead><tbody style=\'text-align: center\'>");
            foreach (var route in m.LineRoutes)
            {
                sb.Append($"<tr><td style ='width: 150px; font-weight: bold'>{route.DepartureDate.ToShortDateString()}</td><td style ='width: 75px; font-weight: bold'>{route.FlightAirline}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.FlightNumber}</td><td style ='width: 100px; font-weight: bold'>{route.DepartureAirport}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.DepartureTime.ToShortTimeString()}</td><td style ='width: 100px; font-weight: bold'>{route.ArrivalAirport}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.ArrivalTime.ToShortTimeString()}</td></tr>");
            }
            sb.Append("</tbody></table><br/><p>Gemäß der Ihnen bekannten Tarifbedingungen berechnen wir folgendes:</p><table style=\'width: 800px\'><tbody><tr><td style =\'width: 100px\'></td><td style=\'width: 500px\'></td><td style =\'width: 200px\'><strong>Flugpreis</strong></td></tr>");
            for (int i = 0; i < m.PassangerModels.Count; i++)
            {
                sb.Append("<tr><td>");
                if (i == 0) sb.Append("<span>Passagier(e)</span>");
                sb.Append($"</td><td style ='font-weight: bold'>{m.PassangerModels[i].FullName}</td><td>{m.PassangerModels[i].PassangerFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            }
            sb.Append("</tbody></table><br/><br/><table style=\'width: 800px\'><tbody>");
            sb.Append($"<tr><td style ='width: 400px'>Flugpreis inkl. Gebühren:</td style ='width: 200px'><td></td><td style ='width: 200px; font-weight: bold; text-align: right' >{m.AirlineTotalPrice.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td>Nicht erstattbare Taxen:</td><td></td><td style ='font-weight: bold; text-align: right'>{m.NonRefundableTaxes.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td>Zusatzleistung/Gepäck:</td><td></td><td style ='font-weight: bold; text-align: right'>{m.AdditionalServicesAndBaggage.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td style ='border-bottom:2pt solid black'>Stornogebühren der Airline:</td><td style ='border-bottom:2pt solid black'></td><td style ='border-bottom:2pt solid black; font-weight: bold; text-align: right'>{m.CancellationAirlineFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td>Bearbeitungsgebühren test:</td><td></td><td style ='font-weight: bold; text-align: right'>{m.BerlogicFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td>Bereits bezahlte Gebühren-test:</td><td></td><td style ='border-bottom:2pt solid black; font-weight: bold; text-align: right'>{m.PrepaidBerlogicFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td></td><td style ='text-align: left'>Gutschrift EURO:</td><td style ='font-weight: bold; text-align: right; border-bottom: 4pt solid black;'>{m.Credit.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append("</tbody></table><br/><br/><br/><br/><br/>");
            sb.Append("<p style=\'text-align: center; margin-top: 100px\'>Die test GmbH ist zum Inkasso berechtigt.Vielen Dank für Ihre Buchung.</p><br/><br/>");
            sb.Append("</div></body></html>");
            return sb.ToString();
        }

        public static string GetHtmlCancellationMailString(ServiceOperationsModel model)
        {
            var sb = new StringBuilder();
            //sb.Append($"<!DOCTYPE html><html><head><meta charset = 'utf-8'></head><body>");
            sb.Append("<div style=\'background - color: beige; font - family: \'Calibri Light\',serif\'>");
            sb.Append("<p>Sehr geehrte Damen und Herren,<br/>vielen Dank für Ihre Anfrage.</p>");
            sb.Append("<p>Wie von Ihnen gewünscht haben wir Ihren Flug <strong>storniert</strong>.</p>");
            if (model.Credit != 0)
            {
                sb.Append("<p style=\'font - weight: bold; font - size: larger\'>Im Anhang übersenden wir Ihnen die Stornobestätigung.</p>");
                sb.Append($"<p>Der Betrag in Höhe von <span style='font - weight: bold; font - size: larger'>{model.Credit.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</span> wird auf Ihre <span style='font - weight: bold; font - size: larger'>Kreditkarte</span> erstattet.</p>");
                sb.Append("<p>Bitte beachten Sie die Überweisungszeiten zwischen den Banken, dieser <br/>Vorgang kann bis zu zwei Wochen dauern.</p>");
            }
            sb.Append("<p>Für Rückfragen stehen wir Ihnen jederzeit gerne zur Verfügung.</p></div>");
            return sb.ToString();
        }

        public static string GetHtmlAddLuggagePdfString(ServiceOperationsModel m, string logopath)
        {
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE html><html><head><meta charset = \'utf-8\'></head>");
            sb.Append("<body><div style=\'background-color: white\'>");
            sb.Append($"<img src ='{logopath}' alt='test' style='height: 200px; width: 500px'></p><table>");
            sb.Append($"<tbody><tr><td style ='width: 120px;'></td><td style ='width: 800px'></td><td style ='text-align: left; width: 150px; font-weight: bold'>{m.AgencyCode}</td>");
            sb.Append("<td style =\'width: 250px\'></td></tr><tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td>");
            sb.Append("<td style=\'text-align: left; min-width: 25px\'>Kundencenter</td><td style =\'min-width: 25px\'></td></tr><tr><td colspan =\'2\' style =\'text-align: left; min-width: 25px\'> test GmbH • test Str. 01 • 100000 Berlin</td>");
            sb.Append("<td colspan=\'2\' style=\'text-align: left; min-width: 25px\'>Bitte wenden Sie sich mit allen Fragen jederzeit an:</td></tr><tr><td style =\'min-width: 25px\'></td>");
            sb.Append("<td style=\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Tel:</td><td style =\'min-width: 25px\'>+49 30 208981350</td></tr><tr>");
            sb.Append("<td style=\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Mo-Fr: 09:00 Uhr - 17:00 Uhr (CET)</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>EMail:</td><td style =\'min-width: 25px\'>test@test.de</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>Website:</td><td style =\'min-width: 25px\'>www.test.de</td></tr>");
            sb.Append("<tr><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'></td><td style =\'min-width: 25px\'>USt.- ID Nr.:</td><td style =\'min-width: 25px\'>DE0000001</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>Kunde:</td><td style ='min-width: 25px; font-weight: bold'>{m.CustomerFullName}</td><td style ='min-width: 25px'>Buchungsdatum:</td><td style ='min-width: 25px; font-weight: bold' >{m.BookingDate.ToShortDateString()}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>E-mail:</td><td style ='min-width: 25px; font-weight: bold'>{m.CustomerEmail}</td><td style ='min-width: 25px'>Referenznummer:</td><td style ='min-width: 25px; font-weight: bold'>{m.BookingNumber}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'>Zahlungsart:</td><td style ='min-width: 25px; font-weight: bold'>{m.PaymentMethod}</td><td style ='min-width: 25px'> Buchungscode:</td><td style ='min-width: 25px; font-weight: bold'>{m.BookingCode}</td></tr>");
            sb.Append($"<tr><td style ='min-width: 25px'></td><td style ='min-width: 25px'></td><td style = 'min-width: 25px'>Änderungsdatum:</td><td style ='min-width: 25px; font-weight: bold'>{m.ChangeServiceDate.ToShortDateString()}</td></tr>");
            sb.Append("</tbody></table><h2 style=\'margin-top: 150px\'><u><strong>Reisegepäck - Rechnung</strong></u></h2><br/><table style=\'width: 900px\'><thead style=\'text-align: center; background-color: beige\'>");
            sb.Append("<tr><td style =\'width: 150px\'>Datum</td><td style =\'width: 75px\'>Flug</td><td style =\'width: 100px\'>FlugNr.</td><td style =\'width: 100px\'>Von</td><td style =\'width: 100px\'>Abflug</td><td style =\'width: 100px\'>Nach</td><td style =\'width: 100px\'>Ortsankunft</td><td style =\'width: 100px\'>Gepäck</td></tr></thead><tbody style=\'text-align: center\'>");
            foreach (var route in m.LineRoutes)
            {
                sb.Append($"<tr><td style ='width: 150px; font-weight: bold'>{route.DepartureDate.ToShortDateString()}</td><td style ='width: 75px; font-weight: bold'>{route.FlightAirline}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.FlightNumber}</td><td style ='width: 100px; font-weight: bold'>{route.DepartureAirport}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.DepartureTime.ToShortTimeString()}</td><td style ='width: 100px; font-weight: bold'>{route.ArrivalAirport}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.ArrivalTime.ToShortTimeString()}</td>");
                sb.Append($"<td style ='width: 100px; font-weight: bold'>{route.Baggage}</td></tr>");
            }
            sb.Append("</tbody></table><br/><p>Gemäß der Ihnen bekannten Tarifbedingungen berechnen wir folgendes:</p><table style=\'width: 800px\'><tbody><tr><td style =\'width: 100px\'></td><td style=\'width: 500px\'></td><td style =\'width: 200px\'><strong>Brutto</strong></td></tr>");
            for (int i = 0; i < m.PassangerModels.Count; i++)
            {
                sb.Append("<tr><td>");
                if (i == 0) sb.Append("<span>Passagier(e)</span>");
                sb.Append($"</td><td style ='font-weight: bold'>{m.PassangerModels[i].FullName}</td><td>{m.PassangerModels[i].PassangerFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            }
            sb.Append("</tbody></table><br/><br/><table style=\'width: 800px\'><tbody>");
            sb.Append($"<tr><td style ='width: 300px; border-bottom:2pt solid black'>Zwischensumme:</td><td style ='width: 300px; border-bottom:2pt solid black'></td><td style ='width: 200px; font-weight: bold; text-align: right; border-bottom:2pt solid black' >{m.PriceDifference.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td>Bearbeitungsgebühren test:</td><td></td><td style ='font-weight: bold; text-align: right; border-bottom: 2pt solid black; height: 50px'>{m.BerlogicFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append($"<tr><td></td><td>Rechnungsbetrag EURO</td><td style ='font-weight: bold; text-align: right; border-bottom: 4pt solid black;'>{m.TotalFee.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}</td></tr>");
            sb.Append("</tbody></table><br/><br/><br/><br/><br/>");
            sb.Append("<p style=\'text-align: center; margin-top: 100px\'>Die test GmbH ist zum Inkasso berechtigt.Vielen Dank für Ihre Buchung.</p><br/><br/>");
            sb.Append("</div></body></html>");
            return sb.ToString();
        }

        public static string GetHtmlAddLuggageMailString(ServiceOperationsModel model)
        {
            var sb = new StringBuilder();
            //sb.Append($"<!DOCTYPE html><html><head><meta charset = 'utf-8'></head><body>");
            sb.Append("<div style=\'background - color: beige; font - family: \'Calibri Light\',serif\'>");
            sb.Append("<p>Sehr geehrte Damen und Herren,<br/>vielen Dank für Ihre Anfrage.</p>");
            sb.Append("<p>Das Gepäck wurde erfolgreich gebucht.</p>");
            sb.Append("<p>Anbei übersenden wir Ihnen die <strong>Gepäckbuchungsbestätigung</strong>.</p>");
            sb.Append("<p>Für Rückfragen stehen wir Ihnen jederzeit gerne zur Verfügung.</p>");
            sb.Append("<p style=\'font - weight: bold; font - size: larger\'>Wir wünschen Ihnen einen guten Flug!</p></div>");
            return sb.ToString();
        }
    }
}
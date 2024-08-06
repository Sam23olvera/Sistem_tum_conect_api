using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp;
using System;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using Humanizer;
using System.Drawing;

namespace ConectDB.Models
{
    public class Email
    {
        public bool EnvioCorreo(string NombreUsuario, string rol, JObject JScorreos)
        {
            bool retorno = false;
            string NoTicket = string.Empty;
            string Equipo = string.Empty;
            string ObsOperacion = string.Empty;
            string TipoFalla = string.Empty;
            string UnidadNegos = string.Empty;
            string Correo_Destino = string.Empty;

            try
            {
                JArray data = JScorreos["data"] as JArray;
                if (data != null && data.Count > 0)
                {
                    JObject emailData = data[0] as JObject;
                    JArray correosArray = emailData["CorreoFalla"] as JArray;
                    if (correosArray != null)
                    {
                        foreach (JObject emailObj in correosArray)
                        {
                            Correo_Destino = emailObj["correo"]?.ToString();
                            NoTicket = emailObj["NoTicket"].ToString();
                            Equipo = emailObj["Equipo"].ToString();
                            ObsOperacion = emailObj["ObsOperacion"].ToString();
                            TipoFalla = emailObj["TipoFalla"].ToString();
                            UnidadNegos = emailObj["UNE"].ToString();
                        }
                    }
                }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
                if (!string.IsNullOrEmpty(Correo_Destino))
                {
                    string[] correos = Correo_Destino.Split(';');
                    foreach (string correo in correos)
                    {
                        if (!string.IsNullOrWhiteSpace(correo))
                        {
                            mmsg.To.Add(correo.Trim());
                        }
                    }
                }
                mmsg.Subject = "Alerta de Registro de Ticket";
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                string Body = "<html>"
                + "<head>"
                + "    <link href='https://fonts.googleapis.com/css?family=Fresca' rel='stylesheet' />"
                + "</head>"
                + "<body>"
                + "    <center>"
                + "        <div>"
                + "            <center>"
                + "                <table>"
                + "                    <tr>"
                + "                        <td style=\"background-color: #f0f0f0; border-radius: 10px 10px 0px 0px;\">"
                + "                            <table>"
                + "                                <tr>"
                + "                                    <td>"
                + "                                        <img src=\"https://webportal.tum.com.mx/wsstest/imag/logo_difuminado.png\" width=\"50\"/>"
                + "                                    </td>"
                + "                                    <td>"
                + "                                        <div style=\"padding-right: 5px; \"></div>"
                + "                                    </td>"
                + "                                    <td>"
                + "                                        <table>"
                + "                                            <tr>"
                + "                                               <td>"
                + "                                                    <div style=\"padding-top: 0px;\"></div>"
                + "                                                </td>"
                + "                                            </tr>"
                + "                                           <tr>"
                + "                                               <td>"
                + "                                                   <span style=\"color:#3C4855; font-family: 'Belleza'; font-size: 18px; font-weight: normal; letter-spacing: 1px; text-align: center;\">"
                + "                                                         Se Acaba de Realizar un Ticket de " + Equipo
                + "                                                    </span>"
                + "                                                    <span style=\"color: #3C4855; font-size: 20px; text-align: left; width: 100%; font-family: 'Belleza'; font-weight: bold; text-shadow: 0px 0px 3px rgba(60, 72, 85, 1.0);\">"
                + "                                                       | En Portal TUM Control Reparaciones"
                + "                                                   </span>"
                + "                                               </td>"
                + "                                           </tr>"
                + "                                       </table>"
                + "                                   </td>"
                + "                               </tr>"
                + "                           </table>"
                + "                       </td>"
                + "                   </tr>"
                + "                   <tr>"
                + "                       <td>"
                + "                          <div style=\"padding-top: 0px;\"></div>"
                + "                          <div style=\"height: 1px; width: 100 %; background-color: #3C4855; box-shadow: 0px 0px 2px #3C4855;\"> </div>"
                + "                          <div style=\"padding-bottom: 5px;\"></div>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td>"
                + "                        <table>"
                + "                            <tr>"
                + "                                <td>"
                + "                                    <span style=\"color: #6F869D; font-family: 'Belleza'; font-size: 17px; font-weight: normal; text-align: center;\">"
                + "                                        Estimad@s:"
                + "                                    </span>"
                + "                                </span>"
                + "                            </td>"
                + "                        </tr>"
                + "                        <tr>"
                + "                            <td>"
                + "                                <span style=\"color:#3C4855; font-family: 'Belleza'; font-size: 17px; font-weight: normal; text-align: center;\">"
                + "                                    Recientemente el usuario <strong> " + NombreUsuario + "</strong> con el rol de " + rol + " acaba de registrar un ticket."
                + "                                </span>"
                + "                            </td>"
                + "                        </tr>"
                + "                        <tr>"
                + "                        <td>"
                + "                             <span style = \"color:#3C4855; font-family: 'Belleza'; font-size: 17px; font-weight: normal; text-align: center;\">"
                + "                                Con las Siguientes Caracteristicas: <strong></strong>"
                + "                             </span>"
                + "                         </td>"
                + "                     </tr>"
                + "                     <tr>"
                + "                         <td>"
                + "                             <span style = \"color:#3C4855; font-family: 'Belleza'; font-size: 17px; font-weight: normal; text-align: center;\">"
                + "                                 -Numero de Ticket: <strong></strong>" + NoTicket
                + "                             </span>"
                + "                         </td>"
                + "                     </tr>"
                + "                     <tr>"
                + "                        <td>"
                + "                            <span style = \"color:#3C4855; font-family: 'Belleza'; font-size: 17px; font-weight: normal; text-align: center;\">"
                + "                                 -Observaciones: <strong></strong>" + ObsOperacion
                + "                         </span>"
                + "                     </td>"
                + "                 </tr>"
                + "                 <tr>"
                + "                    <td>"
                + "                        <span style = \"color:#3C4855; font-family: 'Belleza'; font-size: 17px; font-weight: normal; text-align: center;\">"
                + "                            -Tipo Falla: <strong> </strong>" + TipoFalla
                + "                        </span>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td>"
                + "                        <span style = \"color:#3C4855; font-family: 'Belleza'; font-size: 17px; font-weight: normal; text-align: center;\">"
                + "                            -Unidad de Negocios: <strong> </strong>" + UnidadNegos
                + "                        </span>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td>"
                + "                        <div style=\"padding-top: 10px;\"></div>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td>"
                + "                        <div style=\"padding-top: 10px;\"></div>"
                + "                        <div style=\"height: 1px; width: 100 %; background-color:  #999999; box-shadow: 0px 0px 2px  #999999;\"></div>"
                + "                        <div style=\"padding-bottom: 10px;\"></div>"
                + "                   </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td>"
                + "                        <div style=\"padding - top: 30px;\"></div>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td>"
                + "                        <span style=\"color:#6F869D; font-family: 'Belleza'; font-size: 15px; font-weight: normal; text-align: center;\">"
                + "                           El Ticket se envío el "
                + "                            <span style=\"color:#3C4855; font-family: 'Belleza'; font-size: 15px; font-weight: bold; text-align: center;\">"
                + DateTime.Now.ToShortDateString()
                + "                            </span> a las"
                + "                            <span style=\"color:#3C4855; font-family: 'Belleza'; font-size: 15px; font-weight: bold; text-align: center;\">"
                + DateTime.Now.ToShortTimeString()
                + "                            </span>"
                + "                       </span>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                   <td>"
                + "                        <div style=\"padding-top: 70px;\"></div>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td>"
                + "                        <div style=\"height: 1px; width: 100 %; background-color: #dcdcdc; box-shadow: 0px 0px 2px #dcdcdc;\"></div>"
                + "                        <div style=\"padding-bottom: 10px;\"></div>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td align=\"center\">"
                + "                        <span style=\"color: #999999; font-family: 'Belleza'; font-size: 12px; font-weight: normal; text-align: center;\">"
                + "                           No es necesario responder este correo ya que se genera de manera automática."
                + "                        </span>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td align=\"center\">"
                + "                       <span style=\"color: #999999; font-family: 'Belleza'; font-size: 12px; font-weight: normal; text-align: center;\">"
                + "                            <strong>Portal TUM Control Reparaciones.</strong>"
                + "                        </span>"
                + "                    </td>"
                + "                </tr>"
                + "                <tr>"
                + "                    <td align=\"center\">"
                + "                        <span style=\"color: #999999; font-family: 'Belleza'; font-size: 12px; font-weight: normal; text-align: center;\">"
                + "                            <strong> GRUPO TUM Transportistas Unidos Mexicanos</strong>"
                + "                        </span>"
                + "                    </td>"
                + "                </tr>"
                + "           </table>"
                + "         </td>"
                + "      </tr>"
                + " </table>"
                + " </center>"
                + " </div>"
                + " </center>"
                + " </body>"
                + " </html>";
                mmsg.Body = Body;
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true;
                mmsg.From = new System.Net.Mail.MailAddress("reclutamientorh@tum.com.mx");
                
                SmtpClient cliente = new SmtpClient
                {
                    Credentials = new NetworkCredential("reclutamientorh@tum.com.mx", "P0rJ@w1/B3n2d4kl*FeAS"),
                    Port = 587,
                    EnableSsl = true,
                    Host = "smtp.office365.com",
                    TargetName = "STARTTLS/smtp.office365.com"
                };
                cliente.Send(mmsg);
                return retorno = true;
            }
            catch (SmtpException ex)
            {
                return retorno = false;
            }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BS;
using System.Text;

namespace PingMyNetwork
{
    public partial class _default : System.Web.UI.Page
    {
        List<Hosts.host> ScanNetworkListHosts = new List<Hosts.host>();
        List<Hosts.host> MainListHosts = new List<Hosts.host>();



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //SetMyselfCheckNetwork();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('There are " + CheckOnlineHosts().ToString() + " out of " + MainListHosts.Count + " devices online.', 2000)});", true);


            }
            /// Asign values in the page load
            LoadPushoverConfiguration();

            // Fill main host list

            MainListHosts = new Hosts().GetHostFromDataBase();
            if (MainListHosts.Count > 0)
            {
                GenerateMainHostList();
            }
        }



        #region VARIABLE VALUES 

        /// <summary>
        /// Method that assign values to Pushover configuration
        /// </summary>
        private void LoadPushoverConfiguration()
        {
            ///Get Pushover User from SERVER << TODO
            txtBox_UserPushover.Text = "u7mqewhsxe5rh6yr4ddbweunuv96t4";
            ///Get Pushover Token from SERVER << TODO
            txtBox_TokenPushover.Text = "ap7n7r56xkuttprqi2yyvzuzebcp4w";

            if (txtBox_UserPushover.Text != "")
            {
                ToggleActiveLabel(1, lbl_UserPushover);
                ToggleActiveTextBox(1, txtBox_UserPushover);
            }
            if (txtBox_TokenPushover.Text != "")
            {
                ToggleActiveLabel(1, lbl_TokenPushover);
                ToggleActiveTextBox(1, txtBox_TokenPushover);
            }
            ///Get Pushover Notification Configuration from SERVER << TODO
            chkbox_ScheduledNotifications.Checked = true;
        }

        #endregion

        #region STYLE METHODS

        /// <summary>
        /// Method that change the textbox style 
        /// </summary>
        /// <param name="c">1- Active 2-Not active</param>
        private void ToggleActiveTextBox(int c, TextBox txtbox)
        {
            switch (c)
            {
                case 1:
                    txtbox.Attributes.Add("class", "active");
                    txtbox.Attributes.Add("style", "border-bottom: 1px solid green");
                    break;

                case 2:
                    txtbox.Attributes.Add("style", "border-bottom: 1px solid red");
                    break;
            }
        }

        /// <summary>
        /// Method that change the label style 
        /// </summary>
        /// <param name="c">1- Active 2-Not active</param>
        private void ToggleActiveLabel(int c, Label label)
        {
            switch (c)
            {
                case 1:
                    label.Attributes.Add("class", "active");
                    break;

                case 2:
                    label.Attributes.Remove("class");
                    break;
            }
        }

        protected void linkbtnHeaderRefresh_Click(object sender, EventArgs e)
        {
            string TextToPush = "There are " + CheckOnlineHosts().ToString() + " out of " + MainListHosts.Count + " devices online.";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('There are " + CheckOnlineHosts().ToString() + " out of " + MainListHosts.Count + " devices online.', 2000)});", true);

        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateMainHostList()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class=\"collection\" style=\"border: 0px solid #e0e0e0; \">");
            string status = "red";
            MainListHosts = new Hosts().GetHostFromDataBase();
            NetworkScan a = new NetworkScan();
            for (int i = 0; i < MainListHosts.Count; i++)
            {
                if (a.PingNetwork(MainListHosts[i].ip))
                {
                    sb.AppendFormat("<li class=\"collection-item hostsMain avatar animated fadeIn\" style=\"border: 1px solid grey; margin-bottom:10px;\">");
                    status = "green";
                }
                else
                {
                    sb.AppendFormat("<li class=\"collection-item hostsMain avatar animated fadeIn\" style=\"border: 1px solid grey; margin-bottom:10px;\">");
                    status = "red";
                }
                if (MainListHosts[i].device == "desktop")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">desktop_windows</i>", status);

                }
                if (MainListHosts[i].device == "server")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">desktop_mac</i>", status);

                }
                if (MainListHosts[i].device == "smartphone")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">phone_android</i>", status);

                }
                if (MainListHosts[i].device == "tablet")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">tablet</i>", status);

                }
                if (MainListHosts[i].device == "television")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">tv</i>", status);

                }
                if (MainListHosts[i].device == "smartwatch")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">watch</i>", status);

                }
                if (MainListHosts[i].device == "chromecast")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">cast</i>", status);

                }
                if (MainListHosts[i].device == "console")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">videogame_asset</i>", status);

                }
                if (MainListHosts[i].device == "other")
                {
                    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">devices_other</i>", status);

                }
                //else
                //{
                //    sb.AppendFormat("<i style=\"margin-top: 10px; color: white; margin-bottom: 5px;\" class=\"material-icons circle {0}\">desktop_mac</i>", status);
                //}
                sb.AppendFormat("<span class=\"title\">Hostname: {0}</span>", MainListHosts[i].hostname);
                sb.AppendFormat("<p>");
                sb.AppendFormat("IP Address: {0}", MainListHosts[i].ip);
                sb.AppendFormat("<br>");
                sb.AppendFormat("MAC Address: {0}", MainListHosts[i].mac);
                sb.AppendFormat("</p>");
                //sb.AppendFormat("<div class=\"secondary-content\">");
                //sb.AppendFormat("<a class=\"btn-floating btn-large waves-effect waves-light red\" runat=\"server\"><i class=\"material-icons\">clear</i></a>");
                //sb.AppendFormat("</div>");
                sb.AppendFormat("</li>");
            }
            sb.Append("</ul>");

            mainHostList.InnerHtml = sb.ToString();

        }

        private void DeleteHost(object sender, EventArgs e)
        {

            string ip = "";

            foreach (Hosts.host host in MainListHosts)
            {
                if (host.ip == ip)
                {
                    MainListHosts.Remove(host);
                }

            }

        }

        #endregion

        #region NEW HOST FORM
        /// <summary>
        /// Method that test IP introduced in the ip address textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTestIp_Click(object sender, EventArgs e)
        {
            if (txtBox_ipaddress.Text == "")
            {
                ToggleActiveTextBox(2, txtBox_ipaddress);
                //txtBox_ipaddress.Text = "No IP detected.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('No IP detected!', 2000)});", true);

            }
            else
            {
                NetworkScan a = new NetworkScan();
                if (a.PingNetwork(txtBox_ipaddress.Text))
                {
                    ToggleActiveTextBox(1, txtBox_ipaddress);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Host is online!', 2000)});", true);
                }
                else
                {
                    ToggleActiveTextBox(2, txtBox_ipaddress);
                    //txtBox_ipaddress.Text = "IP is not online.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('IP is not online!', 2000)});", true);

                }
            }

            ActiveLabelsNewHostForm();
        }

        /// <summary>
        /// Method that reads IP address introduced, check hostname and change textBox with hostname.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetHostnameByIP_Click(object sender, EventArgs e)
        {
            NetworkScan a = new NetworkScan();
            if (txtBox_ipaddress.Text == "" || txtBox_ipaddress.Text == "No IP detected." || txtBox_ipaddress.Text == "IP is not online.")
            {
                ToggleActiveTextBox(2, txtBox_hostname);
                //txtBox_hostname.Text = "No IP detected.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('No IP detected!', 2000)});", true);

            }
            else
            {
                string h = a.CheckHostname(txtBox_ipaddress.Text);
                if (h == "")
                {
                    ToggleActiveTextBox(2, txtBox_hostname);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Error obtaining hostname!', 2000)});", true);

                }
                else
                {
                    ToggleActiveTextBox(1, txtBox_hostname);
                    txtBox_hostname.Text = h;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Successfully obtained hostname!', 2000)});", true);

                }
            }

            ActiveLabelsNewHostForm();
        }

        /// <summary>
        /// Method that reads IP address introduced, check MAC Addres and change checkbox with MAC address.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetMACByIP_Click(object sender, EventArgs e)
        {
            if (txtBox_ipaddress.Text == "" || txtBox_ipaddress.Text == "No IP detected." || txtBox_ipaddress.Text == "IP is not online.")
            {
                ToggleActiveTextBox(2, txtBox_macaddress);
                //txtBox_macaddress.Text = "No IP detected.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('No IP detected!', 2000)});", true);
            }
            else
            {
                string mac = MacResolver.FormatMac(MacResolver.GetRemoteMAC(txtBox_ipaddress.Text.ToString()), ':');
                if (txtBox_ipaddress.Text != "")
                {
                    if (mac == "00-00-00-00-00-00")
                    {
                        ToggleActiveTextBox(1, txtBox_macaddress);
                        //txtBox_macaddress.Text = "Can't get the MAC.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Can't get the mac!', 2000)});", true);

                    }
                    else
                    {
                        ToggleActiveTextBox(1, txtBox_macaddress);
                        txtBox_macaddress.Text = mac.ToUpper();
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$( '#mainHostList' ).load( 'default.aspx #mainHostList' );", true);

                    }
                }
            }
            ActiveLabelsNewHostForm();
        }

        /// <summary>
        /// Restart all inputs when closing form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddFormNewHost_Click(object sender, EventArgs e)
        {
            if (txtBox_ipaddress.Text != "")
            {
                NetworkScan a = new NetworkScan();
                if (a.PingNetwork(txtBox_ipaddress.Text) && txtBox_hostname.Text != "" && txtBox_macaddress.Text != "")
                {
                    //Response.Redirect(Request.RawUrl);
                    /// Add a host with textbox parameters
                    new Hosts().AddHost(txtBox_ipaddress.Text, txtBox_hostname.Text, txtBox_macaddress.Text, select_device.Value.ToLower());
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Host added successfully!', 2000)});", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Failed to add host!', 2000)});", true);

                }
                GenerateMainHostList();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ReloadHostList", "$( '#mainHostList' ).load( 'default.aspx #mainHostList' );", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Nothing was added!', 2000)});", true);
            }

            RestartValuesNewHostForm();
        }



        /// <summary>
        /// Restart all inputs when closing form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelFormNewHost_Click(object sender, EventArgs e)
        {
            RestartValuesNewHostForm();
        }

        /// <summary>
        /// Reset values from textboxes
        /// </summary>
        private void RestartValuesNewHostForm()
        {
            txtBox_ipaddress.Text = "";
            txtBox_hostname.Text = "";
            txtBox_macaddress.Text = "";
            select_device.Value = "";
            DisableLabelsNewHostForm();
            DisableTextBoxActiveNewHostForm();
        }

        /// <summary>
        /// Add active class when there is something in the textbox
        /// </summary>
        private void ActiveLabelsNewHostForm()
        {
            if (txtBox_ipaddress.Text != "")
            {
                ToggleActiveLabel(1, lbl_ipaddress);
            }
            if (txtBox_hostname.Text != "")
            {
                ToggleActiveLabel(1, lbl_hostname);
            }
            if (txtBox_macaddress.Text != "")
            {
                ToggleActiveLabel(1, lbl_macaddress);
            }
        }

        /// <summary>
        /// Reverse ActiveLabelsNewHostForm()
        /// </summary>
        private void DisableLabelsNewHostForm()
        {
            ToggleActiveLabel(2, lbl_ipaddress);
            ToggleActiveLabel(2, lbl_hostname);
            ToggleActiveLabel(2, lbl_macaddress);
        }

        /// <summary>
        /// Reverse ActiveLabelsNewHostForm()
        /// </summary>
        private void DisableTextBoxActiveNewHostForm()
        {
            txtBox_ipaddress.Attributes.Add("style", "border-bottom: 1px solid grey");
            txtBox_hostname.Attributes.Add("style", "border-bottom: 1px solid grey");
            txtBox_macaddress.Attributes.Add("style", "border-bottom: 1px solid grey");
        }

        protected void btn_ClearTextBoxNewHost_Click(object sender, EventArgs e)
        {
            RestartValuesNewHostForm();
        }

        #endregion

        #region NOTIFICATION SETTINGS - ACCOUNT CONFIGURATION

        protected void btn_SendTestPushover_Click(object sender, EventArgs e)
        {
            if (txtBox_UserPushover.Text == "")
            {
                ToggleActiveTextBox(2, txtBox_UserPushover);
            }
            else
            {
                ToggleActiveTextBox(1, txtBox_UserPushover);
            }
            if (txtBox_TokenPushover.Text == "")
            {
                ToggleActiveTextBox(2, txtBox_TokenPushover);
            }
            else
            {
                ToggleActiveTextBox(1, txtBox_TokenPushover);
            }
            if (txtBox_UserPushover.Text != "" && txtBox_TokenPushover.Text != "")
            {
                //string onlinehosts = CheckOnlineHosts().ToString();
                string TextToPush = "There are " + CheckOnlineHosts().ToString() + " out of " + MainListHosts.Count + " devices online.";

                Push p = new Push(txtBox_TokenPushover.Text, txtBox_UserPushover.Text, TextToPush);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Push message has been sent!', 2000)});", true);

                ActiveLabelsPushoverAccount();

            }
        }

        protected void btn_SaveSettingsPushover_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Pushover settings has been updated!', 2000)});", true);

        }

        /// <summary>
        /// Add active class when there is something in the textbox
        /// </summary>
        private void ActiveLabelsPushoverAccount()
        {
            if (lbl_UserPushover.Text != "")
            {
                ToggleActiveLabel(1, lbl_UserPushover);
            }
            if (lbl_TokenPushover.Text != "")
            {
                ToggleActiveLabel(1, lbl_TokenPushover);
            }
        }


        protected void btn_UpdatePassword_Click(object sender, EventArgs e)
        {

        }

        private int CheckOnlineHosts()
        {
            int res = 0;
            NetworkScan a = new NetworkScan();

            foreach (Hosts.host host in MainListHosts)
            {

                if (a.PingNetwork(host.ip))
                {
                    res++;
                }

            }

            return res;

        }

        #endregion

        #region NOTIFICATION SETTINGS - SCHEDULED NOTIFICATIONS

        protected void btn_SaveSettingsScheduledNotifications_Click(object sender, EventArgs e)
        {
            if (true)
            {

                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Pushover settings has been updated!', 2000)});", true);
            }


        }

        /// <summary>
        /// Method that send a scheduled notification push with account configuration user and token
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_SendTestScheduledNotificationPushover_Click(object sender, EventArgs e)
        {
            if (txtBox_UserPushover.Text != "" && txtBox_TokenPushover.Text != "")
            {
                Push p = new Push(txtBox_TokenPushover.Text, txtBox_UserPushover.Text, "This is a scheduled notification push from PingMyNetwork!");

                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Push message has been sent!', 2000)});", true);
                ActiveLabelsPushoverAccount();

            }
        }


        #endregion

        #region CHECK NETWORK

        /// <summary>
        /// Pings entire network and retrieve a list with all connected devices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ScanNetwork_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class=\"collection\">");

            ScanNetworkListHosts = new Hosts().FillListWithHost();

            for (int i = 0; i < ScanNetworkListHosts.Count; i++)
            {
                sb.AppendFormat("<li class=\"collection-item avatar\" style=\"width: 100% !important;float: none !important;\">");
                sb.AppendFormat("<i style=\"margin-top: 10px;\" style=\" color: white\" class=\"material-icons circle green\">devices_other</i>");
                sb.AppendFormat("<span class=\"title\">Hostname: {0}</span>", ScanNetworkListHosts[i].hostname);
                sb.AppendFormat("<p>");
                sb.AppendFormat("IP Address: {0}", ScanNetworkListHosts[i].ip);
                sb.AppendFormat("<br>");
                sb.AppendFormat("MAC Address: {0}", ScanNetworkListHosts[i].mac);
                sb.AppendFormat("<p>");
                sb.AppendFormat("</li>");
            }
            sb.Append("</ul>");
            CheckNetworkContainer.InnerHtml = sb.ToString();
            GenerateSelectionScanNetwork(ScanNetworkListHosts);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){Materialize.toast('Network scan finished!', 4000)});", true);

        }

        private void GenerateSelectionScanNetwork(List<Hosts.host> list)
        {

            if (list.Count == 0)
            {
                list = new Hosts().FillListWithHost();
            }

            foreach (Hosts.host host in list)
            {
                ListItem nhost = new ListItem(host.ip, host.ip);
                select_hostscannetwork.Items.Add(nhost);
            }


            //select_device.Items.Add()
        }

        /// <summary>
        /// Method that gets your IP-HOSTNAME-MAC ADDRESS and retrieve a list in html
        /// </summary>
        private void SetMyselfCheckNetwork()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class=\"collection\">");

            NetworkScan a = new NetworkScan();

            string my_ip = a.GetIPAddress();
            string my_hostname = a.CheckHostname(my_ip);
            string my_mac = MacResolver.FormatMac(MacResolver.GetRemoteMAC(my_ip), ':');

            sb.AppendFormat("<li class=\"collection-item avatar\">");
            sb.AppendFormat("<i style=\"margin-top: 10px;\" style=\" color: white\" class=\"material-icons circle green\">desktop_windows</i>");
            sb.AppendFormat("<span class=\"title\">Hostname: {0}</span>", my_hostname);
            sb.AppendFormat("<p>");
            sb.AppendFormat("IP Address: {0}", my_ip);
            sb.AppendFormat("<br>");
            sb.AppendFormat("MAC Address: {0}", my_mac);
            sb.AppendFormat("<p>");
            sb.AppendFormat("</li>");
            sb.Append("</ul>");
            CheckNetworkContainer.InnerHtml = sb.ToString();
        }

        /// <summary>
        /// Clears scan network list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ClearScan_Click(object sender, EventArgs e)
        {
            CheckNetworkContainer.InnerHtml = "";
            ScanNetworkListHosts.Clear();
        }


        #endregion

        protected void refreshselect_Click(object sender, EventArgs e)
        {
            GenerateSelectionScanNetwork(ScanNetworkListHosts);
            select_hostscannetwork.Style.Add("display", "initial");
        }
    }
}
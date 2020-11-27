using DevExpress.XtraEditors;
using Microsoft.Win32;
using PreciousUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Internals
{
    public sealed class ProgramSecurity
    {
        public ProgramSecurity(string[] productKeys, string registryPath)
        {
            this.ProductKey = productKeys;
            this.RegistryPath = registryPath;
        }

        public string[] ProductKey { get; private set; }
        public string RegistryPath { get; set; }

        private void FirstTime()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(RegistryPath); //path

            DateTime dt = DateTime.Now;
            string onlyDate = dt.ToShortDateString(); // get only date not time

            regkey.SetValue("Install", onlyDate); //Value Name,Value Data
            regkey.SetValue("Use", onlyDate); //Value Name,Value Data
        }

        private String CheckFirstDate()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(RegistryPath); //path
            string Br = (string)regkey.GetValue("Install");
            if (regkey.GetValue("Install") == null)
                return "First";
            else
                return Br;
        }

        private bool CheckPassword(String[] pass)
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(RegistryPath); //path
            string Br = (string)regkey.GetValue("Password");
            if (pass.Any(item => item == Br))
                return true; //good
            else
                return false;//bad
        }

        private String GetDayDifPutPresent()
        {
            // get present date from system
            DateTime dt = DateTime.Now;
            string today = dt.ToShortDateString();
            DateTime presentDate = Convert.ToDateTime(today);

            // get instalation date
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(RegistryPath); //path
            string Br = (string)regkey.GetValue("Install");
            DateTime installationDate = Convert.ToDateTime(Br);

            TimeSpan diff = presentDate.Subtract(installationDate); //first.Subtract(second);
            int totaldays = (int)diff.TotalDays;

            // special check if user chenge date in system
            string usd = (string)regkey.GetValue("Use");
            DateTime lastUse = Convert.ToDateTime(usd);
            TimeSpan diff1 = presentDate.Subtract(lastUse); //first.Subtract(second);
            int useBetween = (int)diff1.TotalDays;

            // put next use day in registry
            regkey.SetValue("Use", today); //Value Name,Value Data

            if (useBetween >= 0)
            {

                if (totaldays < 0)
                    return "Error"; // if user change date in system like date set before installation
                else if (totaldays >= 0 && totaldays <= 15)
                    return Convert.ToString(15 - totaldays); //how many days remaining
                else
                    return "Expired"; //Expired
            }
            else
                return "Error"; // if user change date in system
        }

        private void BlackList()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(RegistryPath); //path

            regkey.SetValue("Black", "True");

        }

        private bool CheckBlackList()
        {
            RegistryKey regkey = Registry.CurrentUser;
            regkey = regkey.CreateSubKey(RegistryPath); //path
            string Br = (string)regkey.GetValue("Black");
            if (regkey.GetValue("Black") == null)
                return false; //No
            else
                return true;//Yes
        }

        public bool Algorithm()
        {
            bool chpass = CheckPassword(ProductKey);
            if (chpass == true) //execute
                return true;
            else
            {
                bool block = CheckBlackList();
                if (block == false)
                {
                    string chinstall = CheckFirstDate();
                    if (chinstall == "First")
                    {
                        FirstTime();// installation date
                        DialogResult ds = XtraMessageBox.Show("You are using trial Pack! Would you Like to Activate it Now!", "Product key", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (ds == DialogResult.Yes)
                        {
                            using (ProductKeyForm f1 = new ProductKeyForm(this))
                            {
                                f1.ShowDialog();
                                return true;
                            }
                        }
                        else
                            return true;
                    }
                    else
                    {
                        string status = GetDayDifPutPresent();
                        if (status == "Error")
                        {
                            BlackList();
                            DialogResult ds = XtraMessageBox.Show("Application Can't be loaded, Unauthorized Date Interrupt Occurred! Without activation it can't run! Would you like to activate it?", "Terminate Error-02", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (ds == DialogResult.Yes)
                            {
                                using (ProductKeyForm f1 = new ProductKeyForm(this))
                                {
                                    DialogResult ds1 = f1.ShowDialog();
                                    if (ds1 == DialogResult.OK)
                                        return true;
                                    else
                                        return false;
                                }
                            }
                            else
                                return false;
                        }
                        else if (status == "Expired")
                        {
                            DialogResult ds = XtraMessageBox.Show("The trial version is now expired! Would you Like to Activate it Now!", "Product key", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (ds == DialogResult.Yes)
                            {
                                using (ProductKeyForm f1 = new ProductKeyForm(this))
                                {
                                    DialogResult ds1 = f1.ShowDialog();
                                    if (ds1 == DialogResult.OK)
                                        return true;
                                    else
                                        return false;
                                }
                            }
                            else
                                return false;
                        }
                        else // execute with how many day remaining
                        {
                            DialogResult ds = XtraMessageBox.Show("You are using trial Pack, you have " + status + " days left to Activate! Would you Like to Activate it now!", "Product key", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (ds == DialogResult.Yes)
                            {
                                using (ProductKeyForm f1 = new ProductKeyForm(this))
                                {
                                    f1.ShowDialog();
                                    return true;
                                }
                            }
                            else
                                return true;
                        }
                    }
                }
                else
                {
                    DialogResult ds = XtraMessageBox.Show("Application Can't be loaded, Unauthorized Date Interrupt Occurred! Without activation it can't run! Would you like to activate it?", "Terminate Error-01", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (ds == DialogResult.Yes)
                    {
                        using (ProductKeyForm f1 = new ProductKeyForm(this))
                        {
                            DialogResult ds1 = f1.ShowDialog();
                            if (ds1 == DialogResult.OK)
                                return true;
                            else
                                return false;
                        }
                    }
                    else
                        return false;
                    //return "BlackList";
                }
            }
        }

        public bool PasswordEntry(String pass)
        {
            if (ProductKey.Any(item => item == pass))
            {
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(RegistryPath); //path
                if (regkey != null)
                {
                    regkey.SetValue("Password", pass); //Value Name,Value Data
                }
                return true;
            }
            else
                return false;
        }
    }
}
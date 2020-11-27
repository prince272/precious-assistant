using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Serializing;

namespace PreciousUI.Forms
{
    public partial class UserProfileForm : FormBase
    {
        public UserProfileForm(UserProfile existingUser)
        {
            InitializeComponent();
            RestoreCurrentUser(existingUser);
        }

        public UserProfileForm()
            : this(null)
        {

        }

        public UserProfile CurrentUser { get; set; }

        void RestoreCurrentUser(UserProfile existingUser)
        {
            CurrentUser = existingUser != null ? existingUser : new UserProfile();
            firstNameTextEdit.EditValue = CurrentUser.FirstName;
            lastNameTextEdit.EditValue = CurrentUser.LastName;
            genderComboBoxEdit.Properties.Items.AddRange(Enum.GetValues(typeof(UserGender)));
            genderComboBoxEdit.EditValue = CurrentUser.Gender;
            dobDateEdit.EditValue = CurrentUser.DateOfBirth;
            emailTextEdit.EditValue = CurrentUser.EmailAddress;
            areaTextEdit.EditValue = CurrentUser.AreaName;
            cityTextEdit.EditValue = CurrentUser.City;
            countryTextEdit.EditValue = CurrentUser.Country;
            notesMemoEdit.EditValue = CurrentUser.Notes;
        }

        void SaveCurrentUser()
        {
            CurrentUser.FirstName = (string)firstNameTextEdit.EditValue;
            CurrentUser.LastName = (string)lastNameTextEdit.EditValue;
            CurrentUser.Gender = (UserGender)genderComboBoxEdit.EditValue;
            CurrentUser.DateOfBirth = (DateTime)dobDateEdit.EditValue;
            CurrentUser.EmailAddress = (string)emailTextEdit.EditValue;
            CurrentUser.AreaName = (string)areaTextEdit.EditValue;
            CurrentUser.City = (string)cityTextEdit.EditValue;
            CurrentUser.Country = (string)countryTextEdit.EditValue;
            CurrentUser.Notes = (string)notesMemoEdit.EditValue;
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider.Validate()) return;
            SaveCurrentUser();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class UserProfile : IXtraSerializable
    {
        public UserProfile()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Gender = Forms.UserGender.Male;
            AreaName = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            EmailAddress = string.Empty;
            Notes = string.Empty;
        }

        #region IXtraSerializable Members

        void IXtraSerializable.OnEndDeserializing(string restoredVersion) { }
        void IXtraSerializable.OnEndSerializing() { }
        void IXtraSerializable.OnStartDeserializing(DevExpress.Utils.LayoutAllowEventArgs e) { }
        void IXtraSerializable.OnStartSerializing() { }

        #endregion

        [XtraSerializableProperty]
        public string FirstName { get; set; }
        [XtraSerializableProperty]
        public string LastName { get; set; }
        [XtraSerializableProperty]
        public UserGender Gender { get; set; }
        [XtraSerializableProperty]
        public string AreaName { get; set; }
        [XtraSerializableProperty]
        public string City { get; set; }
        [XtraSerializableProperty]
        public string Country { get; set; }
        [XtraSerializableProperty]
        public string EmailAddress { get; set; }
        [XtraSerializableProperty]
        public DateTime DateOfBirth { get; set; }
        [XtraSerializableProperty]
        public string Notes { get; set; }

        public void Assign(UserProfile userProfile)
        {
            this.FirstName = userProfile.FirstName;
            this.LastName = userProfile.LastName;
            this.Gender = userProfile.Gender;
            this.AreaName = userProfile.AreaName;
            this.City = userProfile.City;
            this.Country = userProfile.Country;
            this.EmailAddress = userProfile.EmailAddress;
            this.DateOfBirth = userProfile.DateOfBirth;
            this.Notes = userProfile.Notes;
        }
    }

    public enum UserGender { Male, Female }
}
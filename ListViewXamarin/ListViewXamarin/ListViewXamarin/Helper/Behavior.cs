using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    #region Behavior
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        SfListView ListView;
        Button AddButton;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            AddButton = bindable.FindByName<Button>("addButton");
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "ContactName",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as Contacts);
                    return item.ContactName[0].ToString();
                }
            });
            AddButton.Clicked += AddButton_Clicked;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            AddButton.Clicked -= AddButton_Clicked;
            AddButton = null;
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region CallBack
        private void AddButton_Clicked(object sender, EventArgs e)
        {
            var viewModel = (sender as Button).BindingContext as ContactsViewModel;
            viewModel.ContactsInfo.Add(new Contacts() { ContactName = "John" , ContactImage = ImageSource.FromResource("ListViewXamarin.Images.Image" + 5 + ".png"), ContactNumber="721-821"});

            Device.BeginInvokeOnMainThread(() =>
            {
                var groupCount = ListView.DataSource.Groups.Count;
                ListView.DataSource.Groups.MoveTo(groupCount - 1, 0);
                ListView.RefreshView();
            });
        }
        #endregion
    }
    #endregion
}
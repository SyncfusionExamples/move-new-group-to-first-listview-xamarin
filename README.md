# How to move a new group to the top of the Xamarin.Forms ListView (SfListView)

The Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview) allows you to reorder the groups added at run time to top. By default, when a new group is created at run time, it will be added as the last group.

You can also refer the following article.

https://www.syncfusion.com/kb/11920/how-to-add-a-new-group-to-the-top-of-the-xamarin-forms-listview-sflistview

**XAML**

Add [Button](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/button) to add new item which will create a new group at run time.

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage">
    <ContentPage.BindingContext>
        <local:ContactsViewModel/>
    </ContentPage.BindingContext>
    <ContentPage.Behaviors>
        <local:Behavior/>
    </ContentPage.Behaviors>
	 <ContentPage.Content>
        <StackLayout>
            <Button Text="Add item" x:Name="addButton" HeightRequest="50"/>
            <syncfusion:SfListView x:Name="listView" ItemSize="60" ItemsSource="{Binding ContactsInfo}">
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        ...
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

**C#**

New data added to **ViewModel** collection in the [Button.Clicked](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.button.clicked) event which creates a new group. You can move any group to particular index using [MoveTo](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.Data.Portable~Syncfusion.Data.Extensions.FunctionalExtensions~MoveTo.html) method from [Syncfusion.Data.Extensions.FunctionalExtensions](https://help.syncfusion.com/cr/xamarin/Syncfusion.Data.Portable~Syncfusion.Data.Extensions.FunctionalExtensions_members.html) helper.

By refreshing using [SfListView.RefreshView](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~RefreshView.html), you can see the changes in the UI.

``` c#
public class Behavior : Behavior<ContentPage>
{
    SfListView ListView;
    Button AddButton;

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

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        var viewModel = (sender as Button).BindingContext as ContactsViewModel;
        viewModel.ContactsInfo.Add(new Contacts() { ContactName = "John" , ContactImage = ImageSource.FromResource("ListViewXamarin.Images.Image" + 5 + ".png")});

        Device.BeginInvokeOnMainThread(() =>
        {
            var groupCount = ListView.DataSource.Groups.Count;
            ListView.DataSource.Groups.MoveTo(groupCount - 1, 0);
            ListView.RefreshView();
        });
    }
}
```

**Output**

![NewGrouptoTop](https://github.com/SyncfusionExamples/move-new-group-to-first-listview-xamarin/blob/master/ScreenShot/NewGrouptoTop.gif)

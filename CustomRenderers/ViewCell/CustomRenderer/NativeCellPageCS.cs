﻿using Xamarin.Forms;

namespace CustomRenderer
{
	public class NativeCellPageCS : ContentPage
	{
		ListView listView;

		public NativeCellPageCS()
		{
			listView = new ListView(ListViewCachingStrategy.RecycleElement)
			{
				ItemsSource = DataSource.GetList(),
				ItemTemplate = new DataTemplate(() =>
				{
					var nativeCell = new NativeCell();
					nativeCell.SetBinding(NativeCell.NameProperty, "Name");
					nativeCell.SetBinding(NativeCell.CategoryProperty, "Category");
					nativeCell.SetBinding(NativeCell.ImageFilenameProperty, "ImageFilename");

					return nativeCell;
				})
			};

			Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Xamarin.Forms native cell", HorizontalTextAlignment = TextAlignment.Center },
					listView
				}
			};

			listView.ItemSelected += OnItemSelected;
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return;
			}

			// Deselect row
			listView.SelectedItem = null;

			await Navigation.PushModalAsync(new DetailPage(e.SelectedItem));
		}
	}
}

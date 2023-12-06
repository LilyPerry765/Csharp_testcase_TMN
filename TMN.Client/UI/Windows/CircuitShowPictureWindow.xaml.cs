using System;
using System.Windows;
using Enterprise;

namespace TMN
{
	public partial class CircuitShowPictureWindow : Window
	{
		public CircuitShowPictureWindow()
		{
			InitializeComponent();
		}

		public CircuitShowPictureWindow(Center center, string title)
		{
			InitializeComponent();

			this.Center = center;
			this.CircuitTitle = title;

			ShowPicture(ResourceName, ImageName);
			ShowDetails();
		}


		private void ShowPicture(string resource, string image)
		{
			try
			{
				imageBox.Source = ImageSourceHelper.GetImageCircuit(resource, image);
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		private void ShowDetails()
		{
			lblAddress.Content = CircuitAddress;
			lblCenter.Content = string.Format("مرکز {0}", Center.Name);
			lblModuleNumber.Content = string.Format("کابل شماره {0}", ModulNumber);
		}

		public Center Center
		{
			get;
			set;
		}

		private  string CircuitTitle
		{
			get;
			set;
		}

		private  string ImageName
		{
			get
			{
				return string.Format("circuit_{0}_{1}.jpg", Center.PointCode, ModulNumber);
			}
		}

		private  string ResourceName
		{
			get
			{
				return string.Format("TMN.Resource{0}", Center.PointCode);
			}
		}

		public string ModulNumber
		{
			get
			{
				int spaceIndex = CircuitTitle.IndexOf(' ');
				if (spaceIndex != -1)
					return CircuitTitle.Substring(0, spaceIndex);
				return "";
			}
		}

		public string CircuitAddress
		{
			get
			{
				int spaceIndex = CircuitTitle.IndexOf(' ');
				if (spaceIndex != -1)
					return CircuitTitle.Substring(spaceIndex);
				return CircuitTitle;
			}
		}
	}
}


/////////////////////////////////////////////////////////////////////////////
//
// (c) 2007 BinaryComponents Ltd.  All Rights Reserved.
//
// http://www.binarycomponents.com/
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace TMN
{
	public partial class WorldClock : TMN.PresentationUtility.Controls.FadeInOutWindow
	{
        public WorldClock()
		{
			InitializeComponent();

			_stack.Margin = new Thickness( _expandSide, _expandTop, _expandSide - _spacing, 4 );

			UpdateForTimeInfos();

            Left = SystemParameters.WorkArea.Right - Width;
            Top = SystemParameters.WorkArea.Bottom - Height - 30;

            //Left = SystemParameters.WorkArea.Left - 10;
            //Top = SystemParameters.WorkArea.Bottom - Height - 30;
		}

		private TMN.WorldClocks.Data.TimeInfo[] TimeInfos
		{
			get
			{
				//App app = (App) App.Current;
                TMN.WorldClocks.Data.Settings setting = new TMN.WorldClocks.Data.Settings();


                return setting.TimeInfos.ToArray();
			}
		}

		private void UpdateForTimeInfos()
		{
			Size thumbnailSize = GetThumbnailSize();

            TMN.WorldClocks.Data.TimeInfo[] timeInfos = TimeInfos;

			Width = thumbnailSize.Width * timeInfos.Length + _expandSide * 2;
			Height = thumbnailSize.Height + _expandTop;

			_stack.Children.Clear();

			for( int i = 0; i < timeInfos.Length; ++i )
			{
                TMN.WorldClocks.Data.TimeInfo ti = timeInfos[i];
                TMN.WorldClocks.Controls.ClockDisplay display = new TMN.WorldClocks.Controls.ClockDisplay();

				display.TimeInfo = ti;
				display.Margin = new Thickness( 0, 0, _spacing, 0 );

                //display.IsMouseOver = false;
                display.IsEnabled = false;
                display.Background = Brushes.Transparent;
                display.TextZoom = 1.5;
				_stack.Children.Add( display );
			}
		}

		private Size GetThumbnailSize()
		{
			double screen = SystemParameters.PrimaryScreenWidth * 0.75;
			double size = 280;

			if( size * TimeInfos.Length > screen )
			{
				size = screen / TimeInfos.Length;
			}

			return new Size( size * 0.87, size );
		}

		private const double _spacing = 10;
		private const double _expandSide = 20;
		private const double _expandTop = 100;
	}
}

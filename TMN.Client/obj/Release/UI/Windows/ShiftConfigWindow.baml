   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters &    TMN.UI.Windows.ShiftConfigWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Be4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     ,uclr-namespace:TMN.UserControls     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters       � Title$'  !تنظيم ساعت شيفت ها�� � 
ResizeMode$ NoResize=�$��Tahoma)�$	��211��$	��309��   lightBlueShadow#��[  .��� $��RightToLeft=� � RowDefinitions �� �� $
��Auto �$��31��.+��� $��5q�$%��ساعت شروع شيفت ها��.��� $��0��$��30,10q� �� �� ��  � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��شيفت صبح:��$��0��$��0��$��Right=�$��Center=��� $��شيفت عصر:��$��1��$��0��$��Right=�$��Center=��� $��شيفت شب:��$��2��$��0��$��Right=�$��Center=�#   Enterprise.Wpf.PersianDateBox  -     Name$ 
morningBox��   DisplayMode$
 Time=�$��0��$��1��$��23��$��Center=�  -   $ afterNoonBox��$
 Time=�$��1��$��1��$��23��$��Center=�  -   $ nightBox��$
 Time=�$��2��$��1��$��23��$��Center=��� -    ��Name$ ControlPanel��$��1��$��
-1,0,-1,-1q�$��32��
����ح�$��1q�._�� .+�"  TMN.UserControls.ImageButton  -     Name$ btnOK��"  TMN.UserControls.ImageButton   Click$ btnOK_Click��$��12,0,0,0q�$
��Left=�$��Stretch=�$��75��$��23��$��Center=�	 �� 	IsDefault	 . 
   Text$
 
تاييد��   ImageSource %  TMN.Assets.ImageSourceExtension     Path$ 	check.png��  -   $ 	btnCancel��$��93,3.5,0,3.5q�$��Stretch=� �� IsCancel . $
 انصراف��$
��Left=�$��78��   $ 
cancel.png��
   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null-%clr-namespace:TMN;assembly=TMN.CommonTMN M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters ;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null"   TMN.Views.Details.CenterView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf 	    6sys&clr-namespace:System;assembly=mscorlib 	    7enums%clr-namespace:TMN;assembly=TMN.Common 	    Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters 	    :a,clr-namespace:TMN.Assets;assembly=TMN.Common 	    $��RightToLeft=�$��Tahoma)�$	��370��$	��501��.���   � 	Resources   �� 
TargetType �� Property �� Value��%J   '��       �� " ��.���  � ��$ Right=���  � ��$ Center=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��107* � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��1��.��سوييچ:�� $��2��$��1��.��نوع مركز:�� $��0��$��2��.��منطقه:�� $��0��.��نام مركز:�� $��2��$��2��.��كد:�� $��0��$��5��.��
PointCode:�� $��2��$��5��.��FX:�� $��0��$��6��.��شماره تست:�� $��2��$��6��.��شماره تماس:�� $��0��$��7��.��عرض جغرافيايي:�� $��2��$��7��.��طول جغرافيايي:�� $��9��$��0,-1,0,1q�.��توضيحات:3clr-namespace:TMN.UserControlsTMN.UserControls % TMN.UserControls.EntityComboBox  -ucclr-namespace:TMN.UserControls 	    -     Name$ 	cmbSwitch��$	��150�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��$��0,2,0,2q�$��1��$��1��	 �� SelectedItem	 �� *
SwitchType+�� -   
 ��Name$
 
cmbRegions��$	��150��$��0,2,0,2q�$��2��$��1��$
 Name��	 �� *Region+�� -   $
 cmbCenterType��$	��150��$��0,2,0,2q�$��1��$��3��$ Value����  TMN.Assets.EnumDataSource     EnumType$ enums:CenterTypesL� �� SelectedValue �� *
CenterType+�� -    ��Name$ txtName��$	��150��$��0,2,0,2q�$��1������ *Name+�� -   $ txtCode��$	��150��$��0,2,0,2q�$��2��$��3������ *Code+�� $��2��$��3��.��
User Name:�� -   $ userNameTextBox��$	��150��$��0,2,0,2q�$��3��$��3������ *
UserName+�� $��3��.��IP:�� $��4��.��کلمه عبور:G� -    G�Name$ password��$��1��$��4���� $��4��$��2��.��تکرار کلمه عبور:G� -   $ passwordRepeat��$��3��$��4���� -	   $ 	ipTextBox��$	��150��$��0,2,0,2q�$��1��$��3������ *	IPAddress+�� -
   $ txtPointCode��$	��150��$��0,2,0,2q�$��1��$��5������ *	PointCode+�� -   $ txtFX��$	��150��$��0,2,0,2q� �� 	MaxLength$ 10��$��3��$��5������ *FX+�� -   $ txtTest��$	��150��$��0,2,0,2q�$ 15��$��1��$��6������ *TestNo+�� -   $ 
txtContact��$	��150��$��0,2,0,2q�$ 30��$��3��$��6������ *	ContactNo+�� -   $ txtLatitude��$	��150��$��0,2,0,2q�$��1��$��7������ *
Latitude+�� -   $ txtLongitude��$	��150��$��0,2,0,2q�$��3��$��7������ *	Longitude+�� $��0��$��8��.��Dest:&   System.Windows.Controls.DataGrid  -     Name$ destDataGrid��$��8��$��0,5,0,5q�   SelectionMode$ Single=�   RowHeaderWidth$ 0Q�   SelectionUnit$ FullRow=�   HeadersVisibility$ Column=�$��1��$��3��$��80������ *Dests+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 300    Header$ عنوان Dest��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+�� -   $ txtDescription��" �� VerticalScrollBarVisibility$
 Auto=� �� AcceptsReturn . $��3��$��0,2,0,2q�$��50��$��1��$��9������ *Description+�� -    ��Name$ DefaultCenterCheckBox�� d� 	IsChecked$
 TrueY�$��1��$��11��$	��Top=�.��مرکز جاری